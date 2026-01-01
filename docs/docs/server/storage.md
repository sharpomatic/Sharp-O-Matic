# Storage

## Database options
SharpOMatic stores workflows, runs, traces, connectors, models, and settings via EF Core. The sample host uses SQLite for convenience:

```csharp
builder.Services.AddSharpOMaticEngine()
    .AddRepository(options => options.UseSqlite("Data Source=sharpomatic.db"));
```

For production, you can use any EF Core provider by swapping the `UseSqlite` call for your chosen provider. The engine also runs migrations on startup by default; you can disable that via `SharpOMaticDbOptions.ApplyMigrationsOnStartup` if you want to manage migrations externally.

## Asset storage
Assets are stored through the `IAssetStore` abstraction. The sample host uses `FileSystemAssetStore`, which writes to a directory on disk. If you need cloud storage, `AzureBlobStorageAssetStore` is available:

```csharp
builder.Services.Configure<AzureBlobStorageAssetStoreOptions>(
    builder.Configuration.GetSection("AssetStorage:AzureBlobStorage"));
builder.Services.AddSingleton<IAssetStore, AzureBlobStorageAssetStore>();
```

Azure configuration requires a container name and exactly one of `ConnectionString` or `ServiceUri`. If you implement your own asset store, register it as the singleton `IAssetStore`.

## Data paths
By default, the sample host stores the SQLite database in your LocalApplicationData folder and assets in `%LOCALAPPDATA%\\SharpOMatic\\Assets`. Override these paths explicitly if you are deploying to a container or a locked-down environment, and make sure the process identity has read/write access to those directories.

