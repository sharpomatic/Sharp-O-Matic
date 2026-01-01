# DTO Reference

## DTO organization
DTOs live in both the Engine and Editor projects:

- `SharpOMatic.Engine.DTO` contains shared request/response models for core services.
- `SharpOMatic.Editor.DTO` contains UI-facing DTOs for editor-specific endpoints.
- Many API responses also use Engine entity and metadata types (for example `WorkflowEntity`, `Run`, `Trace`, `Connector`, `Model`).

## Common payloads
Frequently used types include:

- `WorkflowEntity` and `WorkflowSummary` for workflow CRUD.
- `Run` and `Trace` for execution history and diagnostics.
- `WorkflowRunPageResult` for paged run listings.
- `ContextEntryListEntity` for run inputs.
- `ConnectorConfig` and `ModelConfig` for metadata configs.
- `Connector` and `Model` for saved connector/model instances.
- `AssetSummary` for asset listing and details.
- `AssetUploadRequest` for asset uploads (multipart form data).
- `TransferExportRequest`, `TransferImportResult`, and related transfer DTOs.
- `CodeCheckRequest` and `CodeCheckResult` for Monaco diagnostics.

## Versioning guidelines
Treat the API as contract-driven. Prefer additive changes:

- Add new fields as optional with sensible defaults.
- Do not change enum values or reorder `NodeType` and similar enums.
- Avoid renaming fields that the UI depends on (property names are camelCase in JSON).
- Introduce new endpoints for breaking changes instead of altering existing ones.

Remember that run and trace payloads are stored in the database; changes that affect serialization should be backward compatible.

