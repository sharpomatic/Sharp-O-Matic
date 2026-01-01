# Run Endpoints

## Run creation
Runs are started from the workflow controller:

- `POST /api/workflow/run/{id}` creates a new run and starts execution.

The request body is a `ContextEntryListEntity`, which supplies the run inputs derived from the Start node. The response is the new `runId` as a GUID.

## Run monitoring
Use these endpoints to inspect run status:

- `GET /api/run/latestforworkflow/{id}` returns the most recent `Run` for the workflow.
- `GET /api/run/latestforworkflow/{id}/{page}/{count}` returns a page of runs as `WorkflowRunPageResult`.

The paged endpoint supports `sortBy` (Created or Status) and `sortDirection` (Ascending or Descending). Both endpoints normalize the run input entries to camelCase JSON for the UI.

## Run history and logs
To retrieve trace data for a specific run:

- `GET /api/trace/forrun/{id}` returns the ordered list of `Trace` records.

Each `Trace` includes the node ID, status, message, input/output context snapshots, and timestamps. The editor uses these records to render execution history and to populate the Run Viewer.

