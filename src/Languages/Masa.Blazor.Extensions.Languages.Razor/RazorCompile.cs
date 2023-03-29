using Microsoft.CodeAnalysis.CSharp;

namespace Masa.Blazor.Extensions.Languages.Razor;

public class RazorCompile
{
    private static List<PortableExecutableReference>? _references;

    private static List<RazorExtension>? _extensions;

    private static RazorProjectEngine _engine;

    /// <summary>
    /// Component initialization Initializes components before compilation
    /// </summary>
    /// <param name="refs"></param>
    /// <param name="extensions"></param>
    /// <returns></returns>
    public static void Initialized(List<PortableExecutableReference>? refs, List<RazorExtension> extensions)
    {
        _references = refs;
        _extensions = extensions;

        var config = RazorConfiguration.Create(RazorLanguageVersion.Version_6_0, Constant.ROOT_NAMESPACE,
            _extensions);

        var proj = new CompileRazorProjectFileSystem();

        _engine = RazorProjectEngine.Create(config, proj, builder =>
        {
            builder.SetRootNamespace(Constant.ROOT_NAMESPACE);
            builder.AddDefaultImports(CompileRazorProjectFileSystem.GlobalUsing);

            CompilerFeatures.Register(builder);
            builder.Features.Add(new CompilationTagHelperFeature());
            builder.Features.Add(new DefaultMetadataReferenceFeature
            {
                References = _references
            });
        });
    }

    public static Type? CompileToType(CompileRazorOptions razorOptions)
    {
        var assembly = CompileToAssembly(razorOptions);

        if (assembly == null)
        {
            return null;
        }

        return assembly.GetType(Constant.ROOT_NAMESPACE.EndsWith(".")
            ? Constant.ROOT_NAMESPACE
            : Constant.ROOT_NAMESPACE + "." + razorOptions.ComponentName);
    }

    public static Assembly? CompileToAssembly(CompileRazorOptions razorOptions)
    {
        var compileToByte = CompileToByte(razorOptions);

        if (compileToByte == null)
        {
            return null;
        }

        return Assembly.Load(compileToByte);
    }

    public static byte[]? CompileToByte(CompileRazorOptions razorOptions)
    {
        // Null code will cause compilation failure
        if (string.IsNullOrWhiteSpace(razorOptions.Code))
        {
            return null;
        }

        ArgumentNullException.ThrowIfNull(_references);

        ArgumentNullException.ThrowIfNull(_extensions);

        ArgumentNullException.ThrowIfNull(_engine);

        var codeRenderingProjectItem = new CompileRazorProjectItem()
        {
            Name = razorOptions.ComponentName.EndsWith(".razor")
                ? razorOptions.ComponentName
                : $"{razorOptions.ComponentName}.razor",
            Code = razorOptions.Code
        };

        var razorDoc = _engine.Process(codeRenderingProjectItem);
        var cSharpDocument = razorDoc.GetCSharpDocument();

        var targetCode = cSharpDocument.GeneratedCode;

        if (string.IsNullOrWhiteSpace(targetCode))
            return null;

        if (cSharpDocument.Diagnostics.Count != 0)
        {
            throw new Exception(cSharpDocument.Diagnostics.First(v => v.Severity == RazorDiagnosticSeverity.Error)
                .ToString());
        }

        var syntaxTree = CSharpSyntaxTree.ParseText(targetCode);

        var cSharpCompilation =
            CreateCSharpCompilation($"new{Guid.NewGuid():N}", razorOptions.OptimizationLevel,
                razorOptions.ConcurrentBuild, syntaxTree);

        using MemoryStream ms = new();
        var result = cSharpCompilation.Emit(ms);
        if (!result.Success)
            throw new Exception(result.Diagnostics.First(v => v.Severity == DiagnosticSeverity.Error).ToString());

        return ms.ToArray();
    }

    public static byte[] CompileMultipleToAssembly(CompileMultipleRazorOptions multipleRazorOptions)
    {
        ArgumentNullException.ThrowIfNull(_references);

        ArgumentNullException.ThrowIfNull(_extensions);

        ArgumentNullException.ThrowIfNull(_engine);

        var trees = new List<SyntaxTree>();
        foreach (var fileRazorOption in multipleRazorOptions.FileRazorOptions)
        {
            var codeRenderingProjectItem = new CompileRazorProjectItem()
            {
                Name = fileRazorOption.FileName,
                Code = fileRazorOption.Code
            };

            var razorDoc = _engine.Process(codeRenderingProjectItem);
            var cSharpDocument = razorDoc.GetCSharpDocument();

            var targetCode = cSharpDocument.GeneratedCode;

            if (string.IsNullOrWhiteSpace(targetCode))
                continue;

            if (cSharpDocument.Diagnostics.Count != 0)
            {
                throw new Exception(cSharpDocument.Diagnostics.First(v => v.Severity == RazorDiagnosticSeverity.Error)
                    .ToString());
            }

            trees.Add(CSharpSyntaxTree.ParseText(targetCode));
        }

        var cSharpCompilation =
            CreateCSharpCompilation(multipleRazorOptions.AssemblyName, multipleRazorOptions.OptimizationLevel,
                multipleRazorOptions.ConcurrentBuild, trees.ToArray());

        using MemoryStream ms = new();
        var result = cSharpCompilation.Emit(ms);
        if (!result.Success)
            throw new Exception(result.Diagnostics.First(v => v.Severity == DiagnosticSeverity.Error).ToString());

        return ms.ToArray();
    }

    public static CSharpCompilation CreateCSharpCompilation(string assemblyName, OptimizationLevel optimizationLevel,
        bool concurrentBuild, params SyntaxTree[] trees)
    {
        ArgumentNullException.ThrowIfNull(_references);

        return CSharpCompilation.Create(assemblyName)
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: optimizationLevel,
                specificDiagnosticOptions: new[]
                {
                    new KeyValuePair<string, ReportDiagnostic>("CS1701", ReportDiagnostic.Suppress),
                    new KeyValuePair<string, ReportDiagnostic>("CS1702", ReportDiagnostic.Suppress),
                },
                concurrentBuild: concurrentBuild)) // TODO: If it is WebAssembly you need to cancel the concurrency otherwise it will not work
            .AddSyntaxTrees(trees)
            .AddReferences(_references);
    }
}