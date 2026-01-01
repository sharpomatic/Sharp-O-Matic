# Add Code Node

## Scenario and goals
This tutorial shows how to use a Code node to transform context data with C#. You will accept a name as input, build a greeting in code, and write it to the output context.

## Configuration steps
1) Start from a workflow with a **Start** and **End** node connected.
2) Insert a **Code** node between Start and End.
3) In the Start node, enable initialization and add an input entry:
   - Path: `input.name`
   - Type: `string`
   - Mandatory
4) Open the Code node dialog and paste the following script:

```csharp
var name = Context.Get<string>("input.name");
Context.Set("output.message", $"Hello {name}");
```

5) Save the workflow.

## Verification
Click **Run** and provide a value for `input.name` in the Run panel. After execution, check the **Output** tab for `output.message`. You should also see the Code node marked as successful on the canvas.

