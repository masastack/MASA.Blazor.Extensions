namespace Microsoft.Extensions.DependencyInjection;

public static class MasaBlazorMonacoEditorExtension
{
    public static IServiceCollection AddMasaBlazorMonacoEditor(this IServiceCollection services)
    {
        services.AddScoped<MonacoEditorJSModule>();
        return services;
    }
}
