# Repository Migrations

## Repository design
The repository uses EF Core to persist workflows and runtime data. The key tables are:

- **Workflows**: stores workflow metadata plus JSON for nodes and connections.
- **Runs**: stores run status, timestamps, and serialized input/output context.
- **Traces**: stores per-node execution status and context snapshots.
- **Assets**: stores asset metadata and storage keys.
- **Connector/Model metadata**: stores serialized connector and model configuration.
- **Settings**: stores runtime settings such as run history and node limits.

The `SharpOMaticDbContext` configures cascade deletes so removing a workflow deletes its runs, traces, and run-scoped assets.

## Migrations workflow
Migrations live under `src/SharpOMatic.Engine/Migrations`. The engine applies migrations on startup by default in `NodeExecutionService`. If you want to manage migrations yourself, set `SharpOMaticDbOptions.ApplyMigrationsOnStartup` to `false`.

To create a new migration, install the EF Core tool and run:

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add <MigrationName>
```

Run `dotnet ef database update` or let the engine apply migrations at startup during development.

## Backward compatibility
Database migrations do not rewrite JSON payloads stored in runs and traces. Keep your workflow and context schemas compatible across versions, especially if you add new fields to node entities or rename properties.

Recommended practices:

- Add fields with defaults rather than removing fields.
- Avoid reordering or renumbering enum values (such as `NodeType`).
- Provide compatibility logic when deserializing older node JSON.

