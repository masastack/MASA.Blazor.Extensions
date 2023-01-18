using Microsoft.AspNetCore.Razor.Language;

namespace Masa.Blazor.Extensions.Languages.Razor;

public class CodeRenderingFeatureV2 : IConfigureRazorCodeGenerationOptionsFeature, ITagHelperFeature
{
    public int Order => 1;

    public RazorEngine Engine { get; set; }

    public void Configure(RazorCodeGenerationOptionsBuilder options)
    {
        options.RootNamespace = Constant.RootNamespace;
    }

    public IReadOnlyList<TagHelperDescriptor> TagHelpers { get; set; }

    public IReadOnlyList<TagHelperDescriptor> GetDescriptors()
    {
        return TagHelpers;

    }

}