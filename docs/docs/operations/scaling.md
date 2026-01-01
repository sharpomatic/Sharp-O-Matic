# Scaling

## Scaling goals
Scale the host when workflow runs and model calls start to compete for CPU, IO, or external API throughput, or when you need higher availability for the editor UI.

## Horizontal and vertical options
Vertical scaling (more CPU/memory) is the simplest option because a single instance owns its in-memory run queue. It also helps with concurrent node execution and model call throughput.

Horizontal scaling is possible, but requires extra care:

- Use a shared database and asset store (for example Azure Blob Storage) so all instances see the same workflows and assets.
- Enable sticky sessions for the `/editor/notifications` SignalR hub so the UI stays connected to the same instance.
- Remember that runs execute on the instance that accepted the start request. There is no distributed queue, so instances do not steal work from each other.

## Performance considerations
Common bottlenecks are external model calls, asset IO, and database latency. Measure by looking at run duration, model call timings, and repository throughput. For production, prefer a production-grade database provider over SQLite and use a shared asset store so IO does not become a single-host limitation.

