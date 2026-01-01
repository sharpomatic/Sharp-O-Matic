# Configuration

## appsettings.json keys
The sample server keeps configuration minimal. The most common keys are:

- `Logging`: standard ASP.NET Core log levels.
- `AssetStorage:FileSystem:RootPath`: root directory for the file system asset store.
- `AssetStorage:AzureBlobStorage`: options for the Azure Blob asset store (`ContainerName`, and either `ConnectionString` or `ServiceUri`).

Database configuration is typically done in code when you call `AddRepository(...)`, but you can also map options such as `TablePrefix`, `DefaultSchema`, and `ApplyMigrationsOnStartup` via `SharpOMaticDbOptions` if you want to move those settings into config.

## Environment overrides
Use the standard ASP.NET Core configuration stack to override settings per environment. For example:

- `appsettings.Development.json` for local development.
- `appsettings.Production.json` for production.
- Environment variables like `AssetStorage__FileSystem__RootPath`.

When running behind a reverse proxy, keep the editor base path stable and ensure the proxy forwards WebSocket traffic to `/editor/notifications`.

## Secrets hygiene
Avoid storing secrets in source control. For local development, use user secrets or environment variables. In production, use your platform’s secret manager. If you export transfer packages, avoid including secrets unless the destination explicitly requires them.

