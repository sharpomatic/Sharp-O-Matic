# First Workflow

## Scenario and goals
In this tutorial you will build a small workflow that takes a name as input and produces a greeting in the output context. The goal is to practice the full edit-save-run cycle in the editor and to see how inputs and outputs flow through the runtime.

## Step-by-step build
1) Create a workflow from the **Workflows** page and give it a name like "Hello workflow".
2) Add a **Start**, **Edit**, and **End** node, then connect them in that order.
3) Configure the Start node to initialize an input value:
   - Open the Start node dialog.
   - Enable **Initialize the context**.
   - Add an entry with path `input.name`, type `string`, and mark it **mandatory**.
4) Configure the Edit node to upsert the greeting:
   - Open the Edit node dialog.
   - Under **Upserts**, add an entry with path `output.message`.
   - Set the type to **Expression**.
   - Use a C# expression such as:

```csharp
$"Hello {Context.Get<string>(\"input.name\")}"
```

5) Leave the End node defaults (it will output the full context).

## Validation and next steps
Click **Save** and then **Run**. In the Run panel, set a value for `input.name` and run again if needed. You should see `output.message` in the **Output** tab.

Next, try:

- Adding a second output field in the Edit node.
- Turning on End node mappings to output a smaller result shape.
- Replacing the Edit node with a Code node to generate the greeting in script.

