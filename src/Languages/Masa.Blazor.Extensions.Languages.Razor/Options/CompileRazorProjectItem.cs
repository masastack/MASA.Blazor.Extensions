namespace Masa.Blazor.Extensions.Languages.Razor;

public class CompileRazorProjectItem : RazorProjectItem
{
    public override string FileKind => FileKinds.Component;

    public override string BasePath => "/";

    public override string FilePath => "/" + Name;

    public override string PhysicalPath => "/" + Name;

    public override bool Exists => true;

    public string Name { get; init; }

    public string Code { get; init; }

    public override Stream Read()
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(Code));
    }
}