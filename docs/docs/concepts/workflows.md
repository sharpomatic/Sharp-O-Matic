# Workflows

## Workflow definition
A workflow is the persistent definition of a job you want to run. It is made up of nodes and the connections between them. In code, this maps to a `WorkflowEntity` with a name, description, a set of node entities, and a set of connection entities. Each node records its type, title, size, position, and its input/output connectors. Each connection maps a single output connector to a single input connector.

When you save a workflow, the repository stores it as a `Workflow` record. Nodes and connections are serialized to JSON and stored in the database alongside the workflow metadata (name, description, version). The saved workflow can be run many times without changing the definition, which allows you to compare runs or reproduce results.

## Execution model
Running a workflow creates a `Run` record with status `Created`. The engine then enqueues the Start node and moves the run to `Running`. Each node is executed by its runtime handler (a `RunNode` implementation), and the next node(s) are discovered by walking the outgoing connectors. Nodes that expect a single output use `ResolveSingleOutput`, which means missing or unconnected outputs cause the run to fail immediately.

The engine processes nodes through a background queue. Fan-out nodes clone the current context and execute branches in parallel. Fan-in nodes synchronize those branches and merge their results before continuing. Each node execution creates a `Trace` record that captures input context, output context, status, timing, and a message. When all threads complete, the engine marks the run as `Success`. If any node throws, the run is marked `Failed` and the error message is captured. If no End node sets the run output, the engine falls back to the last node context.

## Workflow lifecycle
Workflows are created in the editor and saved into the repository. Edits are tracked in memory until you save, and the UI disables Run while unsaved changes exist. When you run a workflow, a new `Run` record and a series of `Trace` records are created and stored.

Run history is kept per workflow and pruned automatically based on the `RunHistoryLimit` setting (default 50). Deleting a workflow removes its runs and traces via cascade delete. If you want to preserve a workflow for reuse or sharing, export it through the Transfer page before deleting it.

