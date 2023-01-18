namespace Masa.Blazor.Extensions.Languages.Razor;

public class CompileRazorProjectFileSystem : RazorProjectFileSystem
{
    private static List<string> _globalUsing = new()
    {
        "@using Microsoft.AspNetCore.Components.Web"
    };

    public static string GlobalUsing =>
        string.Join(Environment.NewLine, _globalUsing);

    public static void AddGlobalUsing(params string[] args)
    {
        lock (_globalUsing)
        {
            _globalUsing.AddRange(args);
        }
    }
    
    public static void RemoveGlobalUsing(params string[] args)
    {
        lock (_globalUsing)
        {
            foreach (var s in args)
            {
                _globalUsing.Remove(s);
            }
        }
    }

    public override IEnumerable<RazorProjectItem> EnumerateItems(string basePath)
    {
        throw new NotImplementedException();
    }

    public override RazorProjectItem GetItem(string path)
    {
        throw new NotImplementedException();
    }

    public override RazorProjectItem GetItem(string path, string fileKind)
    {
        if (path == "/_Imports.razor")
            return new CompileRazorProjectItem()
            {
                Name = "_Imports.razor",
                Code = GlobalUsing
            };
        throw new NotImplementedException(fileKind + ":" + path);
    }
}