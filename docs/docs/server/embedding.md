# Embedding

## Integration patterns
SharpOMatic is designed to be embedded in an existing ASP.NET Core application. You bring your own host, then register the Engine and Editor services and map the editor UI to a route. This lets you integrate with your existing auth, logging, and infrastructure while still using the visual editor.

At a minimum you need:

- `AddSharpOMaticEngine()` for runtime services and background execution.
- `AddSharpOMaticEditor()` to expose editor APIs and the SignalR hub.
- An `IAssetStore` implementation (file system by default).
- A repository configuration via `AddRepository(...)`.

## Service registration
Here is a minimal host setup that mirrors the sample server, with room to add your own tooling and schemas:

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

You can add tool methods and schema types on the Engine builder just like the sample host:

```csharp
builder.Services.AddSharpOMaticEngine()
    .AddSchemaTypes(typeof(MySchema))
    .AddToolMethods(MyTools.GetTime);
```

## Routing and static assets
`MapSharpOMaticEditor("/editor")` serves the embedded Angular build under a base path and maps the editor controllers and the Notification hub under the same prefix. It also rewrites the `<base href>` in the embedded index.html so the UI loads correctly from a sub-path.

If you use a different base path, the editor will automatically use that path as its API base URL. Keep the base path stable when deploying behind a reverse proxy so that `/editor/api/*` and `/editor/notifications` stay aligned.

