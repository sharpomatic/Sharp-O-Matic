# Execution Lifecycle

## Execution entry points
The engine exposes run entry points through `IEngineService`. The most common flow is:

1) `CreateWorkflowRun(workflowId)` creates a new `Run` record in `Created` state.
2) `StartWorkflowRunAndNotify(runId, inputEntries)` starts execution asynchronously and streams progress to the editor via SignalR.

If you need a blocking call, `StartWorkflowRunAndWait` returns the completed `Run`. There are also synchronous wrappers for environments that cannot use async.

Execution involves these core services:

- `IRepositoryService` for loading workflows and persisting runs and traces.
- `INodeQueueService` for background scheduling.
- `IRunContextFactory` for per-run scoped services.
- `IRunNodeFactory` to construct node runners.
- `IProgressService` to publish live run and trace updates.

## Runtime pipeline
The pipeline looks like this:

1) **Validate**: `CreateWorkflowRun` loads the workflow and enforces exactly one Start node.
2) **Initialize context**: `StartWorkflowRunAndNotify` applies input entries to a new `ContextObject` using `ContextHelpers.ResolveContextEntryValue`, then serializes inputs for storage.
3) **Create RunContext**: the engine creates a `RunContext` with a scoped service provider, converters, and node run limit.
4) **Enqueue Start**: the Start node is queued for execution.
5) **Execute nodes**: `NodeExecutionService` dequeues work, creates `RunNode` instances via `RunNodeFactory`, and runs them.
6) **Fan-out/fan-in**: Fan Out clones context and spawns parallel threads; Fan In waits for branches and merges their output.
7) **Complete**: when all threads finish, the run is marked `Success` and the output context is stored. If any node throws, the run is marked `Failed`.

Each node writes a `Trace` record with input/output context snapshots and a message. The engine publishes these to the editor so the UI can render progress in real time.

## Cancellation and retries
There is no automatic retry policy today. If a node throws an exception, the run is marked `Failed` and no further nodes are executed.

Cancellation is cooperative: `NodeExecutionService` respects the host shutdown token, and `INodeQueueService` can block all queued nodes for a run via `RemoveRunNodes` (not currently used by the default UI). If you need mid-run cancellation or retries, you can extend the host with custom orchestration and queue logic.

