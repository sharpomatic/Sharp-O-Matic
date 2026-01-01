# Assets

## Scenario and goals
This tutorial shows how to upload assets and reference them in a workflow. You will add a library asset, pass it through the workflow, and verify it appears in outputs.

## Asset configuration
1) Open **Assets** and click **Upload** to add a file to the library.
2) Create a workflow with **Start** and **End** nodes connected.
3) Open the Start node dialog and enable **Initialize the context**.
4) Add an entry:
   - Path: `input.asset`
   - Type: `asset`
   - Optional or mandatory (either is fine for this tutorial)
5) Click **Select** and choose the asset you uploaded.
6) (Optional) Open the End node dialog and enable mappings to copy `input.asset` to `output.asset`.

## Verify outputs
Save and run the workflow. In the **Output** tab, you should see the asset reference object with `assetId`, `name`, `mediaType`, and `sizeBytes`. This confirms the asset reference is flowing through the context.

