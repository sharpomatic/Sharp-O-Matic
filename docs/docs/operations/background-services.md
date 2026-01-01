# Background Services

## Service overview
The workflow runtime is driven by `NodeExecutionService`, a hosted background service registered by `AddSharpOMaticEngine()`. On startup it:

- Applies EF Core migrations (unless `ApplyMigrationsOnStartup` is disabled).
- Seeds default settings such as Run History Limit and Run Node Limit.
- Loads embedded connector and model metadata into the repository.

Once running, it dequeues nodes from the in-memory queue and executes them until each run completes.

## Queue behavior
The engine uses an in-memory, unbounded channel (`INodeQueueService`). Each run is enqueued on the instance that received the start request. Nodes are executed concurrently (up to 5 at a time) and scheduled through the workflow graph. If a run fails, remaining nodes for that run are skipped and the run is marked failed.

Because the queue is in memory:

- A process restart drops in-flight work.
- There is no built-in retry or distributed coordination.
- Long-running or blocked nodes can create backpressure.

## Configuration knobs
Operational settings live in the repository and are editable from the Settings page:

- `RunHistoryLimit` controls how many completed runs are kept per workflow.
- `RunNodeLimit` caps node executions per run to prevent infinite loops.

Database-related tuning is configured via `SharpOMaticDbOptions` when you call `AddRepository(...)` (table prefix, schema, command timeout, and whether to apply migrations on startup). Concurrency for node execution is currently fixed in code, so adjust it by changing `NodeExecutionService` if you need higher throughput.

