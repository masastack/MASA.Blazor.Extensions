namespace Masa.Blazor.Extensions.Languages.Razor;

public class CompileRazorOptions
{
    /// <summary>
    /// 编译代码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Razor组件名称
    /// </summary>
    public string ComponentName { get; set; } = "Rendering";

    /// <summary>
    /// 配置名称
    /// </summary>
    public string ConfigurationName { get; set; } = "Default";

    /// <summary>
    /// 是否Build时并发
    /// （WebAssembly不支持多线程使用并发会报错）
    /// </summary>
    public bool ConcurrentBuild { get; set; } = false;
}