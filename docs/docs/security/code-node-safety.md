# Code Node Safety

## Threat model
Code nodes run user-authored C# inside the host process. By default there is no sandbox, so code nodes can access CPU, memory, filesystem, and network resources that the host process can reach. Treat code nodes as trusted code or restrict access to the editor.

## Mitigations
- Restrict editor access to trusted users.
- Run the host under a least-privilege account.
- Isolate the host network if workflows should not reach external services.
- Use `RunNodeLimit` to prevent runaway loops.
- Limit script assemblies and namespaces with `AddScriptOptions(...)`.

## Safe usage guidance
Prefer Edit nodes or expressions for simple transformations. Keep code node logic small and deterministic, validate inputs, and avoid writing secrets into the workflow context. If you must call external services, handle timeouts and failures explicitly.

