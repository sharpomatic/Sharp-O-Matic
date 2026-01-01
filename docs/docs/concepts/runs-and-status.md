# Runs And Status

## Run records
Each time you run a workflow, the engine creates a `Run` record. A run links back to its workflow and captures timestamps, status, and serialized input/output context. Runs start in `Created`, move to `Running` when the Start node executes, and finish as `Success` or `Failed`.

During execution, each node writes a `Trace` record that includes the node ID, node type, status, message, and the input and output context captured at that point. Runs and traces are stored in the database and can be queried by the editor for history and debugging.

## Status flags and transitions
Run status transitions are straightforward:

- **Created**: the run has been created but not started.
- **Running**: the Start node has begun execution.
- **Success**: all threads completed without error.
- **Failed**: a node threw an exception or the run hit a hard limit.

Node status is tracked separately in traces using `NodeStatus` (Running, Success, Failed). The editor uses these statuses to render live progress on the canvas and to show trace messages in the run panel. If a run fails, subsequent node executions are skipped.

## Run history and diagnostics
Run history shows each execution of a workflow and its final status. You can open a run to inspect its input context, output context, and per-node traces. This makes it easy to identify where a run failed and what data a node received.

The engine prunes history using the `RunHistoryLimit` setting (default 50). If you need longer retention, raise the limit or export workflow artifacts and logs for external storage. Runs also enforce `RunNodeLimit` (default 500) to prevent infinite loops.

