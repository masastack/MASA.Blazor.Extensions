# Dynamically compile Razor components

English| [简体中文](./README.zh-CN.md)

## sample

```csharp

// Initialize the RazorCompile must be initialized before calling compilation
RazorCompile.Initialized(await GetReference(),await GetRazorExtension());

async Task<List<PortableExecutableReference>?> GetReference()
{
    #region WebAsembly
    
    // need to add Service
    var httpClient = service.GetService<HttpClient>();

    var portableExecutableReferences = new List<PortableExecutableReference>();
    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
    {
        try
        {
            // WebAssembly You need to get the assembly over the network
            var stream = await httpClient!.GetStreamAsync($"_framework/{assembly.GetName().Name}.dll");
            if(stream.Length > 0)
            {
                portableExecutableReferences?.Add(MetadataReference.CreateFromStream(stream));
            }
        }
        catch (Exception e) // There may be a 404
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
            // Server is running on the Server and you can get the file directly if you're a Hybrid like Maui Wpf you don't need to get the file through HttpClient and you can get the file directly like server
            portableExecutableReferences?.Add(MetadataReference.CreateFromFile(v.Location));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    #endregion
   
    // As a result of WebAssembly and Server return to PortableExecutableReference mechanism are different need to separate processing
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

After the initialization is complete, you can call the tool method to edit the Code and Copy the following code to the home page

```csharp

@page "/"

<button class="button" @onclick="Run">Refresh</button>

<div class="input-container">
    <textarea @bind="Code" type="text" class="input-box" placeholder="Please enter the execution code" >
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
            Code = Code // TODO: ConcurrentBuild is guaranteed to be false under WebAssembly because Webassembly does not support multithreading
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

## Runtime API Reference

### Add a global reference at compile time

```csharp
CompileRazorProjectFileSystem.AddGlobalUsing("@using Masa.Blazor")
```

### Remove global references at compile time

```csharp
CompileRazorProjectFileSystem.RemoveGlobalUsing("@using Masa.Blazor")
```

Global references are called every time you compile and you can dynamically add and remove global references
