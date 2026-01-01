# Workflow Endpoints

## Workflow CRUD
Workflows are managed under `/api/workflow`:

- `GET /api/workflow` returns a list of `WorkflowSummary` objects.
- `GET /api/workflow/count` returns the total number of workflows matching the optional search.
- `GET /api/workflow/{id}` returns a full `WorkflowEntity`.
- `POST /api/workflow` upserts a workflow (create or update).
- `DELETE /api/workflow/{id}` deletes the workflow and cascades to runs, traces, and run-scoped assets.

`POST /api/workflow` expects a full workflow payload, including `id`, `version`, `name`, `description`, `nodes`, and `connections`. Nodes are polymorphic and use `NodeType` as the discriminator, so the payload must include a `nodeType` value for each node.

## Versioning and publishing
The API does not have a publish state. The editor always runs the latest saved workflow definition. The `version` field is client-owned; the server does not auto-increment or enforce a version policy, so clients should treat it as metadata rather than concurrency control.

## Filtering and pagination
`GET /api/workflow` supports:

- `search`: optional text filter (trimmed server-side).
- `sortBy`: `Name` or `Description`.
- `sortDirection`: `Ascending` or `Descending`.
- `skip`: number of rows to skip (for pagination).
- `take`: number of rows to take.

Use `GET /api/workflow/count` with the same `search` value to build paged UIs.

