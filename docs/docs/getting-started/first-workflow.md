# First Workflow

## Create a workflow
Start the server and open the editor at `http://localhost:9001/editor`. The left navigation highlights Workflows by default. Click **New** to create a workflow. The editor immediately navigates to the workflow detail screen with tabs for **Details**, **Design**, and **Runs**.

Use the **Details** tab to set a meaningful title and description. These values are shown in the workflow list and help you identify workflows later.

## Connect and configure nodes
Switch to the **Design** tab and use the **Add** menu to place a **Start** node and an **End** node. The engine requires exactly one Start node; the UI disables the Start option after you add one.

Arrange the nodes by dragging them on the canvas. To connect them, drag from the output connector on the right side of the Start node to the input connector on the left side of the End node.

To configure a node, double-click it. The Start node dialog lets you optionally initialize the run context. If you enable **Initialize the context**, you can declare input paths and default values. These inputs will appear in the Run panel when you execute the workflow. For a minimal first run, you can leave initialization off.

## Run and inspect results
Click **Save** (the **Run** button is disabled while there are unsaved changes). After saving, click **Run** to execute the workflow.

The Run panel appears on the right side of the editor. Use the panel tabs to inspect each phase:

- **Input** shows the run inputs derived from Start node initialization.
- **Output** shows the final context captured by the End node.
- **Trace** shows per-node execution messages and status.

Open the **Runs** tab to view history. Double-click a run to open its detailed viewer and inspect inputs, outputs, and traces for that specific execution.

