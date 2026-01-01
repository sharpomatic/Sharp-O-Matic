# Overview

## Editor layout
The editor is split into a left navigation rail and a main content area. The left rail is where you move between Workflows, Connectors, Models, Assets, Transfer, and Settings. The right side renders the selected page, such as the workflow list or the workflow editor.

The workflow editor itself has three layers:

- A header with the workflow title and actions (Add, Save, Run).
- Tabs for **Details**, **Design**, and **Runs**.
- The **Design** tab hosts the canvas, plus a collapsible Run panel on the right for inputs, outputs, and traces.

## Navigation patterns
Navigation is route-based. You start on the Workflows list, create or open a workflow, and then move between the Details, Design, and Runs tabs inside the workflow editor. Connectors, Models, Assets, Transfer, and Settings are top-level pages in the left rail.

If you attempt to leave a workflow with unsaved changes, the editor presents an unsaved changes dialog that lets you save, discard, or stay. This is enforced by the route guard on workflow and model/connector detail pages.

## Common workflows
The typical authoring loop looks like this:

1) Create a workflow from the Workflows list.
2) Edit name and description in **Details**.
3) Add nodes and connections in **Design**.
4) Save, then Run to execute.
5) Inspect results in the Run panel and the **Runs** tab.
6) Iterate on the graph, configuration, or inputs and run again.

You can also manage models and connectors for Model Call nodes, upload reusable assets, and export or import workflows via the Transfer page.

