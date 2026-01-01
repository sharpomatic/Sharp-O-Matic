# Monaco Editors

## JSON editor usage
SharpOMatic uses Monaco for JSON fields that need structure, such as context entries or structured output schemas. The JSON editor runs with a fixed-width font, automatic layout, and no minimap, making it comfortable for inline configuration. Always enter valid JSON; invalid JSON will cause runtime validation errors when the engine tries to parse the value.

## C# editor usage
The C# editor is used for Code nodes and expression fields. As you type, the editor sends your code to the backend code-check endpoint and displays compilation diagnostics inline. This helps catch syntax and type errors early.

Code executes on the server with access to the current context via a `Context` variable, so treat it as part of the workflow runtime. Save and run the workflow to see the effect of your script on the context.

## Error diagnostics
Monaco displays compiler errors as red markers in the editor gutter. If the code check API fails (for example, the API URL is incorrect), the editor shows a toast error and stops updating diagnostics.

Runtime errors still surface during workflow execution. Use the Run panel or Run Viewer to see the failing node, its input context, and the exception message.

