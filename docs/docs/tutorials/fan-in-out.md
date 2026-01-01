# Fan In Out

## Scenario and goals
This tutorial demonstrates parallel branches with Fan Out and synchronization with Fan In. You will split a workflow into two branches, produce partial results, and then merge them.

## Build the pattern
1) Create a workflow with **Start**, **Fan Out**, two **Edit** nodes, **Fan In**, and **End**.
2) Connect Start to Fan Out.
3) In the Fan Out dialog, add two outputs and name them (for example, "A" and "B").
4) Connect each Fan Out output to its own Edit node.
5) Connect both Edit nodes into the Fan In node, then connect Fan In to End.
6) In each Edit node, upsert a value under the `output` object:
   - Branch A: path `output.branchA`, type `string`, value `"A done"`.
   - Branch B: path `output.branchB`, type `string`, value `"B done"`.

## Validate behavior
Save and run the workflow. In the **Output** tab you should see both `output.branchA` and `output.branchB` present. If you place values outside of `output`, they may not be merged by Fan In, so keep branch results under `output.*` when using this pattern.

