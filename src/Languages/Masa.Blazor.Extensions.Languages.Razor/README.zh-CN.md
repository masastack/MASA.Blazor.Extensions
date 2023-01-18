# 动态编译Razor组件

[English](./README.md) | 简体中文

## 示例

```csharp

// RazorCompile 在调用编译之前必须先初始化
RazorCompile.Initialized(await GetReference(),await GetRazorExtension());

async Task<List<PortableExecutableReference>?> GetReference()
{   
    #region WebAsembly
    
    // 传入Service
    var httpClient = service.GetService<HttpClient>();

    var portableExecutableReferences = new List<PortableExecutableReference>();
    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
    {
        try
        {
            // web Assembly 需要通过网络获取程序集
            var stream = await httpClient!.GetStreamAsync($"_framework/{assembly.GetName().Name}.dll");
            if(stream.Length > 0)
            {
                portableExecutableReferences?.Add(MetadataReference.CreateFromStream(stream));
            }
        }
        catch (Exception e) // 可能存在404的情况
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion
    
    #region Server
    
    var portableExecutableReferences = new List<PortableExecutableReference>();
    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
    {
        try
        {
            // Server是在服务器运行可以直接获取文件 如果是Maui Wpf这种Hybrid开发的话不需要通过HttpClient获取可以跟Server一样直接读取文件
            portableExecutableReferences?.Add(MetadataReference.CreateFromFile(asm.Location));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    #endregion
   
    // 由于WebAssembly和Server的机制不太一样需要分开处理返回PortableExecutableReference
    return portableExecutableReferences;
}

async Task<List<RazorExtension>> GetRazorExtension()
{
    var exits = new List<RazorExtension>();

    foreach (var asm in typeof(Program).Assembly.GetReferencedAssemblies())
    {
        exits.Add(new AssemblyExtension(asm.FullName, AppDomain.CurrentDomain.Load(asm.FullName)));
    }

    return exits;
}
```

初始化完成以后可以调用工具方法编辑Code了，将以下代码Copy到首页

```csharp

@page "/"

<button class="button" @onclick="Run">刷新</button>

<div class="input-container">
    <textarea @bind="Code" type="text" class="input-box" placeholder="请输入执行代码" >
    </textarea>
</div>

@if (ComponentType != null)
{
    <DynamicComponent Type="ComponentType"></DynamicComponent>
}

@code{

    private string Code = @"<body>
    <div id='app'>
        <header>
            <h1>Doctor Who&trade; Episode Database</h1>
        </header>

        <nav>
            <a href='main-list'>Main Episode List</a>
            <a href='search'>Search</a>
            <a href='new'>Add Episode</a>
        </nav>

        <h2>Episodes</h2>

        <ul>
            <li>...</li>
            <li>...</li>
            <li>...</li>
        </ul>

        <footer>
            Doctor Who is a registered trademark of the BBC. 
            https://www.doctorwho.tv/
        </footer>
    </div>
</body>";

    private Type? ComponentType;

    private void Run()
    {
        ComponentType = RazorCompile.CompileToType(new CompileRazorOptions()
        {
            Code = Code // TODO: 在WebAssembly下保证ConcurrentBuild是false 因为Webassembly不支持多线程
        });

        StateHasChanged();
    }

}

<style>
    .button{
        width: 100%;
        font-size: 22px;
        background-color: cornflowerblue;
        border: 0px;
        margin: 5px;
        border-radius: 5px;
        height: 40px;
    }
    .input-container {
        width: 500px;
        margin: 0 auto;
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
    } 
    .input-box {
        width: 100%;
        height: 100px;
        border: 1px solid #ccc;
        border-radius: 5px;
        font-size: 14px;
    }
</style>
```

## Api说明

### 添加编译时的全局引用

```csharp
CompileRazorProjectFileSystem.AddGlobalUsing("@using Masa.Blazor")
```

### 删除编译时的全局引用

```csharp
CompileRazorProjectFileSystem.RemoveGlobalUsing("@using Masa.Blazor")
```

全局引用在每次编译的时候都会被调用您可以动态添加和删除全局引用
