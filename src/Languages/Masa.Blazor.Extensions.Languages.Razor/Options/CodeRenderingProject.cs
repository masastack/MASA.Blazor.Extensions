using Microsoft.AspNetCore.Razor.Language;

namespace Masa.Blazor.Extensions.Languages.Razor;

public class CodeRenderingProject : RazorProjectFileSystem
{
    private static List<string> globalUsing = new()
    {
        "@using Microsoft.AspNetCore.Components.Web"
    };

    /// <summary>
    /// 获取全局Using
    /// </summary>
    public static string GlobalUsing =>
        string.Join(Environment.NewLine, globalUsing);

    /// <summary>
    /// 添加全局引用
    /// </summary>
    /// <param name="args"></param>
    public static void AddGlobalUsing(params string[] args)
    {
        lock (globalUsing)
        {
            globalUsing.AddRange(args);
        }
    }

    /// <summary>
    /// 清楚全局引用
    /// </summary>
    /// <param name="args"></param>
    public static void RemoveGlobalUsing(params string[] args)
    {
        lock (globalUsing)
        {
            foreach (var s in args)
            {
                globalUsing.Remove(s);
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
            return new CodeRenderingProjectItem()
            {
                Name = "_Imports.razor",
                Code = GlobalUsing
            };
        throw new NotImplementedException(fileKind + ":" + path);
    }
}