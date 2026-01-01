# Runtime

## Execution failures
Most runtime errors surface in the Run panel as a failed status with an error message. Common causes include:

- Missing mandatory inputs on the Start node.
- Invalid expressions in Edit or Switch nodes.
- Missing connector or model configuration for Model Call nodes.
- Exceeding the run node limit (`RunNodeLimit`).

Fix the configuration and rerun the workflow to validate.

## Data and context issues
Context path errors show up when a node reads or writes a path that does not exist or has an unexpected type. Use the Run panel **Input** and **Trace** tabs to inspect the context at each node and verify paths.

## Service availability
If runs fail immediately, confirm the host process is running, the background execution service is active, and the asset store is reachable. Check file permissions for the asset root or storage connection strings for blob storage.

