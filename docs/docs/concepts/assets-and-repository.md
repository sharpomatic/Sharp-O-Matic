# Assets And Repository

## Assets in workflows
Assets are binary files that workflows can reference, such as images or other media. The engine stores asset metadata in the database and stores the file content in an asset store. Assets are identified by a stable `AssetId`, and the `AssetRef` object (assetId, name, mediaType, sizeBytes) is the canonical way to reference an asset in context data.

There are two scopes:

- **Library** assets are reusable across workflows.
- **Run** assets are tied to a specific run and are deleted when the run is deleted.

In the editor, asset references show up in context entries as `AssetRef` or `AssetRefList`. These entries serialize to JSON and are validated during execution.

## Repository storage
SharpOMatic uses an EF Core repository (`SharpOMaticDbContext`) to persist workflow and runtime data. The key tables are:

- **Workflows**: stores workflow metadata plus JSON blobs for nodes and connections.
- **Runs**: stores run status, timestamps, and serialized input/output context.
- **Traces**: stores per-node execution status, messages, and context snapshots.
- **Assets**: stores asset metadata and storage keys.
- **Connector/Model metadata**: stores serialized connector and model definitions.
- **Settings**: stores runtime configuration such as run history and node limits.

Repository operations are exposed through `IRepositoryService`, which handles upserts, queries, and pruning.

## Persistence boundaries
The database stores the workflow graph, run history, traces, connectors, models, and asset metadata. The asset store holds the actual binary files, keyed by storage keys stored in the database. External services (for example, model providers) are not stored in the repo; only their configuration and credentials are persisted in connector and model records.

Keep context payloads reasonably sized because they are stored as JSON in run and trace records. For large or binary data, use assets and pass references in the context instead of embedding the data directly.

