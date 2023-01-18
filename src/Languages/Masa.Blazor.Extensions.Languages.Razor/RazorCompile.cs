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

    public static byte[] CompileToByte(CompileRazorOptions razorOptions)
    {
        // Null code will cause compilation failure
        if (string.IsNullOrEmpty(razorOptions.Code))
        {
            return null;
        }

        if (_references == null)
        {
            throw new ArgumentNullException(nameof(_references));
        }

        if (_extensions == null)
        {
            throw new ArgumentNullException(nameof(_extensions));
        }

        var config = RazorConfiguration.Create(RazorLanguageVersion.Version_3_0, razorOptions.ConfigurationName,
            _extensions);

        var proj = new CompileRazorProjectFileSystem();

        if (_tagHelpers == null)
        {
            var razorProjectEngine = RazorProjectEngine.Create(config, proj, b =>
            {
                b.Features.Add(new DefaultMetadataReferenceFeature
                {
                    References = _references
                });
                b.Features.Add(new CompilationTagHelperFeature());
                b.Features.Add(new DefaultTagHelperDescriptorProvider());
                CompilerFeatures.Register(b);
            });

            _tagHelpers = razorProjectEngine.Engine.Features.OfType<ITagHelperFeature>().Single().GetDescriptors().ToArray();
        }

        var engine = RazorProjectEngine.Create(config, proj,
            b => { b.Features.Add(new CompileRazorCodeGenerationOptionsFeature() { TagHelpers = _tagHelpers }); });

        var codeRenderingProjectItem = new CompileRazorProjectItem()
        {
            Name = razorOptions.ComponentName.EndsWith(".razor")
                ? razorOptions.ComponentName
                : razorOptions.ComponentName + ".razor",
            Code = razorOptions.Code
        };

        var razorDoc = engine.Process(codeRenderingProjectItem);
        var cSharpDocument = razorDoc.GetCSharpDocument();

        var targetCode = cSharpDocument.GeneratedCode;

        if (string.IsNullOrEmpty(targetCode) && cSharpDocument.Diagnostics.Count != 0)
            return null;

        var st = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(targetCode);
        var csc = Microsoft.CodeAnalysis.CSharp.CSharpCompilation.Create("new" + Guid.NewGuid().ToString("N"))
            .WithOptions(new Microsoft.CodeAnalysis.CSharp.CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                concurrentBuild: razorOptions.ConcurrentBuild)) // TODO: If it is web Assembly you need to cancel the concurrency otherwise it will not work
            .AddSyntaxTrees(st)
            .AddReferences(_references);

        MemoryStream ms = new();
        var result = csc.Emit(ms);
        if (!result.Success)
            throw new Exception(result.Diagnostics.First(v => v.Severity == DiagnosticSeverity.Error).ToString());

        return ms.ToArray();
    }
}