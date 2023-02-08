using Microsoft.AspNetCore.Components;

namespace Masa.Blazor;

public partial class MMonacoEditor
{
    [Inject]
    public MonacoEditorJSModule MonacoEditorJsModule { get; set; }

    [Parameter]
    public object EditorOptions { get; set; } = new EditorOptions();

    [Parameter]
    public string Width { get; set; } = "100%";

    [Parameter]
    public string Height { get; set; } = "100%";

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Id { get; set; }

    private ElementReference _ref;

    private ElementReference? _prevRef;

    private bool _elementReferenceChanged;

    /// <summary>
    /// 添加语言智能提示
    /// </summary>
    [Parameter]
    public MonacoRegisterCompletionItemOptions[]? MonacoRegisterCompletionItemOptions { get; set; }

    public virtual ElementReference Ref
    {
        get => _ref;
        set
        {
            if (_prevRef.HasValue)
            {
                if (_prevRef.Value.Id != value.Id)
                {
                    _prevRef = value;
                    _elementReferenceChanged = true;
                }
            }
            else
            {
                _prevRef = value;
            }

            _ref = value;
        }
    }

    /// <summary>
    /// Monaco
    /// </summary>
    public IJSObjectReference Monaco { get; private set; }

    protected override void OnInitialized()
    {
        Id ??= Guid.NewGuid().ToString("N");

        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await InitializeMonaco();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task InitializeMonaco()
    {
        if (MonacoRegisterCompletionItemOptions != null)
        {
            // RegisterCompletionItemProvider need in front of the initialization
            await MonacoEditorJsModule.RegisterCompletionItemProvider(MonacoRegisterCompletionItemOptions!);
        }

        Monaco = await MonacoEditorJsModule.Initialize(Id, EditorOptions);
    }

    public async Task<string> GetValue()
    {
        return await MonacoEditorJsModule.GetValue(Monaco);
    }

    public async Task SetValue(string value)
    {
        await MonacoEditorJsModule.SetValue(Monaco, value);
    }

    public async Task SetTheme(string theme)
    {
        await MonacoEditorJsModule.SetTheme(theme);
    }

    public async Task<TextModelOptions[]> GetModels()
    {
        return await MonacoEditorJsModule.GetModels();
    }

    public async Task<TextModelOptions> GetModel(IJSObjectReference id)
    {
        return await MonacoEditorJsModule.GetModel(Monaco);
    }

    public async Task SetModelLanguage(IJSObjectReference id, string languageId)
    {
        await MonacoEditorJsModule.SetModelLanguage(Monaco, languageId);
    }

    public async Task RemeasureFonts()
    {
        await MonacoEditorJsModule.RemeasureFonts();
    }

    public async Task AddKeybindingRules(KeybindingRule[] rules)
    {
        await MonacoEditorJsModule.AddKeybindingRules(rules);
    }

    public async Task AddKeybindingRule(KeybindingRule rule)
    {
        await MonacoEditorJsModule.AddKeybindingRule(rule);
    }
}