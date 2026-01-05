---
title: Start with NuGet packages
sidebar_position: 2
---

Use these steps if you want to add SharpOMatic to an existing ASP.NET Core project.

## Install packages

```powershell
dotnet add package SharpOMatic.Engine
dotnet add package SharpOMatic.Editor
```

## Register Services

Update `Program.cs` to add the required services.
The following example stores library assets and a SQLite database in your user profile.
This gets you up and running quickly and isolates the data to the current user.

For example, if your username is JohnDoe, then the files will be at:<br />
`C:\Users\JohnDoe\AppData\Local\SharpOMatic`

```csharp
// Assets are stored in the current user's profile
builder.Services.Configure<FileSystemAssetStoreOptions>(
    builder.Configuration.GetSection("AssetStorage:FileSystem"));
builder.Services.AddSingleton<IAssetStore, FileSystemAssetStore>();

builder.Services.AddSharpOMaticEditor();
builder.Services.AddSharpOMaticTransfer();
builder.Services.AddSharpOMaticEngine()
    .AddRepository((optionBuilder) =>
    {
        // SQLite database in current user's profile
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "SharpOMatic", "sharpomatic.db");
        optionBuilder.UseSqlite($"Data Source={dbPath}");
    });
```

## Map the editor UI

```csharp
app.MapSharpOMaticEditor("/editor");
```

## Open Visual Editor

Use your favorite browser to open http://localhost:9001/editor
