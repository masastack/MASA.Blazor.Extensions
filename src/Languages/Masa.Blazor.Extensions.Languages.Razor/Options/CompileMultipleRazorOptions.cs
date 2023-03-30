namespace Masa.Blazor.Extensions.Languages.Razor;

public class CompileMultipleRazorOptions
{
    public List<CompileFileRazorOptions> FileRazorOptions { get; set; }

    /// <summary>
    /// Configuration Name 
    /// </summary>
    public string ConfigurationName { get; set; } = "Default";

    public string AssemblyName { get; set; }
    
    /// <summary>
    /// Whether to Build concurrently
    /// （WebAssembly does not support multiple threads using concurrency）
    /// </summary>
    public bool ConcurrentBuild { get; set; } = false;

    /// <summary>
    /// Determines the level of optimization of the generated code.
    /// </summary>
    public OptimizationLevel OptimizationLevel { get; set; } = OptimizationLevel.Release;
}