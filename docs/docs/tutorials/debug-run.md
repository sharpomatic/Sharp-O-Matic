# Debug Run

## Scenario and goals
This tutorial intentionally creates a failure so you can practice debugging a run. You will configure a mandatory input, run without providing it, and then use traces to find the failure.

## Inspect execution
1) Create a workflow with **Start** and **End** nodes connected.
2) Open the Start node dialog and enable **Initialize the context**.
3) Add a mandatory entry:
   - Path: `input.userId`
   - Type: `string`
4) Save the workflow and click **Run** without setting a value for `input.userId`.

The run will fail. Open the Run panel and read the error message. Then open the **Runs** tab, double-click the failed run, and inspect:

- **Run** for the failure status and error message.
- **Input** to confirm no value was provided.
- **Trace** to see which node threw the error.

## Fix and verify
Open the Run panel, set a value for `input.userId`, and run again. The run should succeed, and the traces should show a successful Start and End node. This pattern applies to most runtime failures: provide missing inputs, correct invalid expressions, and re-run.

