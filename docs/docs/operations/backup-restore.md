# Backup Restore

## Backup strategy
Back up both the repository database and the asset store. The database holds workflows, runs, connectors, models, and settings; the asset store holds binary files referenced by runs and nodes.

Defaults for the sample host are:

- Database: `%LOCALAPPDATA%\\sharpomatic.db`
- Assets: `%LOCALAPPDATA%\\SharpOMatic\\Assets`

If you use Azure Blob Storage or a different database provider, back up those systems using their native tools.

## Restore procedure
1) Stop the host application.
2) Restore the database file or database instance.
3) Restore the asset store contents to the configured root or container.
4) Start the host and allow migrations to run if needed.

## Validation after restore
Open the editor and confirm workflows, connectors, and models are present. Run a small workflow and verify that run history and asset access work as expected.

