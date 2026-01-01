# Editor Endpoints

## Editor API overview
The editor API is the HTTP surface used by the Angular UI. When the editor is hosted under `/editor`, the API base path is:

```
/editor/api
```

Endpoints are grouped by resource: workflows, runs, traces, assets, metadata (connectors and models), settings, tools, schema types, transfer, and Monaco code checks. Most endpoints are JSON and use camelCase property names.

## Key editor endpoints
Common entry points include:

- `GET /api/status` for a health check.
- `GET /api/workflow` and `GET /api/workflow/{id}` for workflow list and details.
- `POST /api/workflow` to upsert a workflow.
- `POST /api/workflow/run/{id}` to start a run.
- `GET /api/run/latestforworkflow/{id}` and `GET /api/trace/forrun/{id}` to inspect runs and traces.
- `GET /api/assets` and `POST /api/assets` for asset listing and upload.
- `GET /api/metadata/connector-configs` and `GET /api/metadata/model-configs` to load metadata configs.
- `GET /api/metadata/connectors` and `GET /api/metadata/models` for connector and model instances.
- `POST /api/transfer/export` and `POST /api/transfer/import` for packaging and importing artifacts.
- `POST /api/monaco/codecheck` for C# diagnostics.
- `GET /api/setting`, `GET /api/tool`, and `GET /api/schematype` for editor support data.

## Response and error format
Success responses are JSON unless the endpoint is explicitly returning a file (transfer export) or an empty response (upserts and deletes). Validation errors typically return `400 Bad Request` with a plain text message. Unhandled exceptions surface through the standard ASP.NET Core error response, so clients should be prepared to read both JSON problem details and plain text error bodies.

