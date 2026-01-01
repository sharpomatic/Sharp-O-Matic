# Embed Editor

## Scenario and goals
In this tutorial you will embed the SharpOMatic editor UI inside an existing ASP.NET Core app. The goal is to keep your own hosting, authentication, and configuration while exposing the editor at a predictable route (for example `/editor`) that developers can use to build and run workflows.

## Integration steps
1) Reference the SharpOMatic projects or packages in your host app:
   - `SharpOMatic.Engine` (runtime services).
   - `SharpOMatic.Editor` (editor UI + APIs).
   - Optional: `SharpOMatic.Editor` transfer endpoints if you want export/import.
2) Register the engine, editor, repository, and asset store. The snippet below mirrors the sample server but fits an existing host:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharpOMaticEngine()
    .AddRepository(options => options.UseSqlite("Data Source=sharpomatic.db"))
    .AddScriptOptions([typeof(Program).Assembly], ["MyApp"]);

builder.Services.AddSharpOMaticEditor();
builder.Services.AddSharpOMaticTransfer();

builder.Services.Configure<FileSystemAssetStoreOptions>(
    builder.Configuration.GetSection("AssetStorage:FileSystem"));
builder.Services.AddSingleton<IAssetStore, FileSystemAssetStore>();

var app = builder.Build();

app.MapSharpOMaticEditor("/editor");
app.Run();
```

3) (Optional) Set a file-system asset root if you do not want the default `%LOCALAPPDATA%\SharpOMatic\Assets` location:

```json
{
  "AssetStorage": {
    "FileSystem": {
      "RootPath": "C:\\Data\\SharpOMatic\\Assets"
    }
  }
}
```

4) If you already use auth or middleware, keep it in your normal pipeline. `MapSharpOMaticEditor("/editor")` must be called before `app.Run()` and must be a sub-path (not `/`).

## Verify the embed
Run the host application and navigate to `http://localhost:<port>/editor`. You should see the editor UI load. Create a workflow, click **Save**, then **Run** to confirm the editor can reach the API and SignalR hub. If you enabled transfer, visit the **Transfer** page to verify export/import endpoints are wired up.

