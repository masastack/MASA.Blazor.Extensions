# Monaco editor

[English](./README.md) | 简体中文

## sample

Add js reference

```html
<script>
        var require = { paths: { 'vs': 'https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs' } };
</script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/loader.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.nls.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.js"></script>
```

Inject the MasaBlazorMonacoEditor service

```
builder.Services.AddMasaBlazorMonacoEditor();
```

Basic sample code

```csharp
@using Masa.Blazor

<div style="height:95vh;width:50%;float:left">
    <MMonacoEditor EditorOptions="Options" @ref="_monacoEditor" />
</div>
<button @onclick="GetValue" style="margin:5px;height:25px;font-size:18px;background-color:cornflowerblue;">Get Code</button>
<code>
    @Code
</code>

@code{
    private MMonacoEditor? _monacoEditor;
    private string Code;
    private object Options;
    protected override void OnInitialized()
    {
        Options = new
        {
            value = """{"value":"masa"}""", // Initial code
            language = "json", // Syntactic support language
            automaticLayout = true, // Automatically ADAPTS to parent container size
            theme = "vs-dark" // monaco theme 
        };
        base.OnInitialized();
    }

    private async Task GetValue()
    {
        Code = await _monacoEditor.GetValue();
    }
}
```
