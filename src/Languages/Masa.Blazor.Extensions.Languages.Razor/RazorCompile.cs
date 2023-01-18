namespace Masa.Blazor.Extensions.Languages.Razor;

public class RazorCompile
{
    private static List<PortableExecutableReference>? _references;

    private static TagHelperDescriptor[]? _tagHelpers;

    private static List<RazorExtension>? _extensions;

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
    }

    public static Type? CompileToType(CompileRazorOptions razorOptions)
    {
        var assembly = CompileToAssembly(razorOptions);
        return assembly.GetType(Constant.ROOT_NAMESPACE.EndsWith(".")
            ? Constant.ROOT_NAMESPACE
            : Constant.ROOT_NAMESPACE + "." + razorOptions.ComponentName);
    }

    public static Assembly CompileToAssembly(CompileRazorOptions razorOptions)
        => Assembly.Load(CompileToByte(razorOptions));

    public static byte[]? CompileToByte(CompileRazorOptions razorOptions)
    {
        // Null code will cause compilation failure
        if (string.IsNullOrWhiteSpace(razorOptions.Code))
        {
            return null;
        }

        ArgumentNullException.ThrowIfNull(_references);
        
        ArgumentNullException.ThrowIfNull(_extensions);

        var config = RazorConfiguration.Create(RazorLanguageVersion.Version_3_0, razorOptions.ConfigurationName,
            _extensions);

        var proj = new CompileRazorProjectFileSystem();

        if (_tagHelpers == null)
        {
            var razorProjectEngine = RazorProjectEngine.Create(config, proj, builder =>
            {
                builder.Features.Add(new DefaultMetadataReferenceFeature
                {
                    References = _references
                });
                builder.Features.Add(new CompilationTagHelperFeature());
                builder.Features.Add(new DefaultTagHelperDescriptorProvider());
                CompilerFeatures.Register(builder);
            });

            _tagHelpers = razorProjectEngine.Engine.Features.OfType<ITagHelperFeature>().Single().GetDescriptors().ToArray();
        }

        var engine = RazorProjectEngine.Create(config, proj,
            builder => { builder.Features.Add(new CompileRazorCodeGenerationOptionsFeature() { TagHelpers = _tagHelpers }); });

        var codeRenderingProjectItem = new CompileRazorProjectItem()
        {
            Name = razorOptions.ComponentName.EndsWith(".razor")
                ? razorOptions.ComponentName
                : $"{razorOptions.ComponentName}.razor",
            Code = razorOptions.Code
        };

        var razorDoc = engine.Process(codeRenderingProjectItem);
        var cSharpDocument = razorDoc.GetCSharpDocument();

        var targetCode = cSharpDocument.GeneratedCode;

        if (string.IsNullOrWhiteSpace(targetCode) && cSharpDocument.Diagnostics.Count != 0)
            return null;

        var syntaxTree = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(targetCode);
        var cSharpCompilation = Microsoft.CodeAnalysis.CSharp.CSharpCompilation.Create($"new{Guid.NewGuid():N}")
            .WithOptions(new Microsoft.CodeAnalysis.CSharp.CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                concurrentBuild: razorOptions.ConcurrentBuild)) // TODO: If it is WebAssembly you need to cancel the concurrency otherwise it will not work
            .AddSyntaxTrees(syntaxTree)
            .AddReferences(_references);

        using MemoryStream ms = new();
        var result = cSharpCompilation.Emit(ms);
        if (!result.Success)
            throw new Exception(result.Diagnostics.First(v => v.Severity == DiagnosticSeverity.Error).ToString());

        return ms.ToArray();
    }
}