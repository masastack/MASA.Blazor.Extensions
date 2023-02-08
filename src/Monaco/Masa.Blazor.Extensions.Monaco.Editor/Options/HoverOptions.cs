namespace Masa.Blazor;

public class EditorHoverOptions
{
    /// <summary>
    /// Delay for showing the hover.
    /// Defaults to 300.
    /// </summary>
    public int Delay { get; set; } = 300;

    /// <summary>
    /// Enable the hover.
    /// Defaults to true.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Is the hover sticky such that it can be clicked and its contents selected?
    /// Defaults to true.
    /// </summary>
    public bool Sticky { get; set; } = true;

    /// <summary>
    /// 如果可能，悬停是否应该显示在直线上方?
    /// 默认为false。
    /// </summary>
    public bool Above { get; set; }
}