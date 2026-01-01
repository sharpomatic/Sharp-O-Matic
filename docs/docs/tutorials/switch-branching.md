# Switch Branching

## Scenario and goals
This tutorial models a simple decision tree using a Switch node. You will route execution based on an input value and set different outputs for each branch.

## Build the switch
1) Create a workflow with **Start**, **Switch**, **Edit**, **Edit**, and **End** nodes.
2) Connect Start to Switch. Create two outputs on the Switch node (for example, "High" and "Low").
3) Connect each Switch output to its own Edit node, then connect both Edit nodes to End.
4) In the Start node, initialize a mandatory input:
   - Path: `input.score`
   - Type: `int`
5) Open the Switch dialog and add expressions for each branch:
   - For the first entry, use: `Context.Get<int>("input.score") >= 80`
   - For the second entry, use: `Context.Get<int>("input.score") >= 50`
   - The last entry is the default and does not take code.
6) In each Edit node, upsert a different value into `output.bucket` (for example, "high", "medium", "low").

## Validate behavior
Run the workflow multiple times with different `input.score` values and confirm:

- The Switch routes to the correct branch.
- `output.bucket` matches the expected label.
- Traces show the Switch message indicating which branch was chosen.

