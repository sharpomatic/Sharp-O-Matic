# Node Dialogs

## Dialog structure
Double-click a node on the canvas to open its dialog. Each dialog is a full-screen overlay with a header and a **Close** button. Most dialogs share a tabbed layout:

- **Details** for configuration fields.
- **Inputs** and **Outputs** for context snapshots captured during runs.

Dialogs for richer nodes add additional tabs. For example, Model Call nodes expose tabs for chat, text, image, tool calling, and structured output configuration.

## Editing parameters
Fields in the Details tab map directly to node properties. Start, End, and Edit nodes use context entry tables so you can define input, output, or edit paths with explicit types and default values. Code and JSON fields use Monaco editors for a better authoring experience.

Model Call nodes are metadata-driven. The model selection determines which parameter fields are shown, and capability flags control which sections appear. When you change the selected model, parameter fields update to match that model’s config.

## Save and revert behavior
Changes in node dialogs are applied immediately to the in-memory workflow. Closing the dialog does not revert changes; the workflow still has unsaved edits until you click **Save** in the header.

If you navigate away with unsaved changes, the editor prompts you to save, discard, or stay. To revert changes without saving, choose **Discard** in that dialog or reload the workflow.

