namespace Masa.Blazor.Extensions.Languages.Razor;

public class CompileRazorCodeGenerationOptionsFeature : IConfigureRazorCodeGenerationOptionsFeature, ITagHelperFeature
{
    public int Order => 1;

    public RazorEngine Engine { get; set; }

    public IReadOnlyList<TagHelperDescriptor> TagHelpers { get; set; }
    
    public void Configure(RazorCodeGenerationOptionsBuilder options)
    {
        options.RootNamespace = Constant.ROOT_NAMESPACE;
    }
    
    public IReadOnlyList<TagHelperDescriptor> GetDescriptors()
    {
        return TagHelpers;
    }
}