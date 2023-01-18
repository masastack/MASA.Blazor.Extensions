using System.Reflection;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Razor;

namespace Masa.Blazor.Extensions.Languages.Razor;

public class CodeRenderingHelper
{
    private static List<PortableExecutableReference>? _refs;

    private static TagHelperDescriptor[]? tagHelpers;

    private static List<RazorExtension>? exts;

    public static Type? RenderingToType(RenderingToTypeOptions options)
    {
        // code为空会导致编译失败
        if (string.IsNullOrEmpty(options.Code))
        {
            return null;
        }

        var config = RazorConfiguration.Create(RazorLanguageVersion.Version_3_0, options.ConfigurationName, exts);

        var proj = new CodeRenderingProject();

        if (tagHelpers == null)
        {
            var rpe1 = RazorProjectEngine.Create(config, proj, b =>
            {
                b.Features.Add(new DefaultMetadataReferenceFeature
                {
                    References = _refs
                });
                b.Features.Add(new CompilationTagHelperFeature());
                b.Features.Add(new DefaultTagHelperDescriptorProvider());
                CompilerFeatures.Register(b);
            });

            tagHelpers = rpe1.Engine.Features.OfType<ITagHelperFeature>().Single().GetDescriptors().ToArray();
        }

        var engine = RazorProjectEngine.Create(config, proj,
            b => { b.Features.Add(new CodeRenderingFeatureV2() { TagHelpers = tagHelpers }); });

        var codeRenderingProjectItem = new CodeRenderingProjectItem()
        {
            Name = options.ComponentName.EndsWith(".razor") ? options.ComponentName : options.ComponentName + ".razor",
            Code = options.Code
        };

        var razorDoc = engine.Process(codeRenderingProjectItem);
        var cSharpDocument = razorDoc.GetCSharpDocument();

        var targetCode = cSharpDocument.GeneratedCode;

        if (string.IsNullOrEmpty(targetCode) && cSharpDocument.Diagnostics.Count != 0)
            return null;

        var st = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(targetCode);
        var csc = Microsoft.CodeAnalysis.CSharp.CSharpCompilation.Create("new" + Guid.NewGuid().ToString("N"))
            .WithOptions(new Microsoft.CodeAnalysis.CSharp.CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                concurrentBuild: options.ConcurrentBuild)) // 如果是web Assembly需要取消并发否则会异常
            .AddSyntaxTrees(st)
            .AddReferences(_refs);

        MemoryStream ms = new();
        var result = csc.Emit(ms);
        if (!result.Success)
            throw new Exception(result.Diagnostics.First(v => v.Severity == DiagnosticSeverity.Error).ToString());

        var assembly = Assembly.Load(ms.ToArray());

        return assembly.GetType(Constant.RootNamespace.EndsWith(".")
            ? Constant.RootNamespace
            : Constant.RootNamespace + "." + options.ComponentName);
    }

    /// <summary>
    /// 组件初始化 在编译前优先执行初始化组件
    /// </summary>
    /// <param name="refs"></param>
    /// <param name="extensions">忽略加载的程序集名称</param>
    /// <returns></returns>
    public static void Initialized(List<PortableExecutableReference>? refs, List<RazorExtension> extensions)
    {
        _refs = refs;
        exts = extensions;
    }
}