# Monaco编辑器

English| [简体中文](./README.zh-CN.md)

## 示例

添加js引用

```html
<script>
        var require = { paths: { 'vs': 'https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs' } };
</script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/loader.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.nls.js"></script>
<script src="https://cdn.masastack.com/npm/monaco-editor/0.34.1/min/vs/editor/editor.main.js"></script>
```

注入MasaBlazorMonacoEditor服务

```
builder.Services.AddMasaBlazorMonacoEditor();
```

基本示例代码

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
            value = """{"value":"masa"}""", // 初始代码
            language = "json", // 语法支持语言
            automaticLayout = true, //自动适应父容器大小
            theme = "vs-dark" // monaco主题 
        };
        base.OnInitialized();
    }

    private async Task GetValue()
    {
        Code = await _monacoEditor.GetValue();
    }
}
```
