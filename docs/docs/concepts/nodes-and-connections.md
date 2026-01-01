# Nodes And Connections

## Nodes as units of work
Nodes are the executable building blocks of a workflow. Each node has a type, a title, a size, a position on the canvas, and a set of input and output connectors. The node type determines its runtime behavior, and the engine uses a `RunNode` implementation to execute it.

SharpOMatic ships with core node types:
- **Start**: enters the workflow and optionally initializes context inputs.
- **End**: exits the workflow and optionally maps outputs.
- **Edit**: upserts or deletes values in the context.
- **Code**: runs C# script against the current context.
- **Switch**: evaluates C# boolean expressions and chooses a branch.
- **Fan Out**: duplicates the context and runs branches in parallel.
- **Fan In**: merges branches created by Fan Out.
- **Model Call**: invokes a model using configured connectors and parameters.

## Connections and data flow
Connections join an output connector on one node to an input connector on another. They define the control flow, while the data flow always happens through the shared context object. A connection does not carry payloads itself; it simply decides which node executes next.

Branching is represented by nodes that produce multiple outputs. A Switch node chooses a single output based on the first matching expression (or defaults to its last switch entry). Fan Out creates one thread per output connector and executes them concurrently with cloned contexts. Fan In waits for all branches from the same Fan Out to arrive and then merges their outputs into a single context before continuing.

## Validation rules
The engine enforces a few invariants at runtime:

- A workflow must have exactly one Start node.
- Nodes that expect a single output require exactly one connected output connector; missing connections raise errors.
- Fan In only accepts branches that originated from the same Fan Out; mixing sources fails the run.
- Switch expressions must compile and return booleans, or the run fails with a compilation or execution error.

The editor UI prevents common mistakes (for example, adding multiple Start nodes), but the backend still validates these rules during execution. Runs also enforce a node execution limit (`RunNodeLimit`, default 500) to prevent infinite loops.

