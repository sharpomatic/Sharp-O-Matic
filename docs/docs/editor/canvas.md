# Canvas

## Creating nodes
Use the **Add** menu in the workflow header to insert nodes. The menu contains the built-in node types (Start, End, Edit, Code, Model Call, Switch, Fan In, Fan Out). The Start node can only be added once; after it exists, the UI disables that option.

New nodes are placed near the top-left of the canvas and snap to the grid. You can reposition them by dragging; positions snap to 16-pixel increments to keep layouts tidy.

## Connecting nodes
Connections are created by dragging from a node’s output connector (right side) to another node’s input connector (left side). A connection represents execution flow; data always moves through the shared context, not the connection itself.

Each output connector supports a single outgoing connection. If you need multiple branches, use nodes that provide multiple outputs (Switch or Fan Out). To remove a connection, select it and press **Delete**.

## Organizing the canvas
The canvas supports a few core layout tools:

- **Selection rectangle**: click and drag on empty space to box-select nodes and their connections.
- **Multi-select**: hold **Ctrl** and click to add or remove items from the selection.
- **Pan**: right-click drag to move the canvas.
- **Zoom**: scroll the mouse wheel or use the zoom buttons; **Reset view** returns to 100%.

There is no auto-layout or grouping feature yet, so keep graphs readable by spacing nodes evenly and naming them clearly.

