# Database

## Migration errors
Migrations are applied at startup by `NodeExecutionService` unless `ApplyMigrationsOnStartup` is disabled. If migrations fail:

- Verify the connection string and database permissions.
- For SQLite, confirm the file path is writable and not locked.
- Temporarily enable `ApplyMigrationsOnStartup` to apply pending migrations automatically.

## Corrupt or missing data
Restore from the latest database backup and ensure the asset store matches the same point in time. If connector or model metadata appears missing, restart the host to reload embedded metadata into the repository.

## Connection issues
Connection errors typically indicate invalid provider configuration or unsupported concurrency (for example, multiple instances writing to the same SQLite file). Use a production database provider if you need multi-instance hosting.

