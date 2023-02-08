namespace Masa.Blazor;

public class EditorParameterHintOptions
{
    /// <summary>
    ///启用参数提示循环。
    /// 默认为false。
    /// </summary>
    public bool Cycle { get; set; }
    
    /// <summary>
    /// Enable parameter hints.
    /// Defaults to true.
    /// </summary>
    public bool Enabled { get; set; } = true;
}