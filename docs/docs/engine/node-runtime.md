# Node Runtime

## Node contracts
Each node type has two parts:

- A **node entity** that defines its serialized shape (for example, `StartNodeEntity`).
- A **runtime handler** that executes the node (for example, `StartNode`).

At runtime, the engine executes nodes through the `IRunNode` interface. The base class `RunNode<T>` handles trace creation, input/output capture, and status updates. Your node handler implements `RunInternal` and returns a list of `NextNodeData` objects that tell the engine which node(s) to run next and which `ThreadContext` to use.

Node handler discovery is attribute-driven. `RunNodeFactory` scans for classes decorated with `RunNodeAttribute(NodeType.X)` and creates them via DI when a node of that type executes.

## Validation and preflight
Validation happens at two levels:

- **Engine-level invariants**: the engine validates the workflow has exactly one Start node and enforces the `RunNodeLimit` to prevent infinite loops.
- **Node-level validation**: each node throws `SharpOMaticException` when configuration or inputs are invalid (for example, missing paths, invalid switch expressions, or incorrect fan-in wiring).

This means most invalid configurations fail fast at runtime, and the error message is captured in the run and trace records.

## Extensibility
To add a new node type:

1) Add a new `NodeType` enum value.
2) Create a `NodeEntity` subclass and annotate it with `NodeEntityAttribute(NodeType.X)` so the JSON converter can deserialize it.
3) Implement a `RunNode<T>` handler and annotate it with `RunNodeAttribute(NodeType.X)`.
4) Update the editor front end to render, edit, and serialize the new node.

Because both the engine and editor discover node types from attributes, keeping the enum and entity/runtime pair aligned is essential.

