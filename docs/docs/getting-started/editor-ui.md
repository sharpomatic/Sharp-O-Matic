# Editor UI

## Install frontend dependencies
The editor UI source lives in `src/SharpOMatic.FrontEnd`. Install dependencies from that directory:

```bash
cd src/SharpOMatic.FrontEnd
npm ci
```

Use `npm install` only if you need to update the lockfile. For local builds, `npm ci` is preferred because it is repeatable and matches the committed `package-lock.json`.

## Build and serve the editor UI
There are two supported ways to run the editor UI:

1) Embedded build (used by the .NET server)

When you build or run the .NET server, the `SharpOMatic.Editor` project runs `npm run build -- --configuration production` and embeds the output into the editor assembly. You do not need to run `ng build` yourself. Once the server is running, open:

```
http://localhost:9001/editor
```

2) Angular dev server (best for UI iteration)

Run the backend first:

```bash
dotnet run --project src/SharpOMatic.Server/SharpOMatic.Server.csproj
```

Then start the Angular dev server:

```bash
cd src/SharpOMatic.FrontEnd
npm run start
```

The UI will be available at `http://localhost:4200` and will call the backend at `http://localhost:9001` by default.

## Common build errors
If the editor build fails, these are the most common causes:

- `node` or `npm` not found: confirm Node.js is installed and on your PATH.
- `npm ci` fails: delete `src/SharpOMatic.FrontEnd/node_modules` and rerun `npm ci` to restore a clean install.
- The editor build fails during `dotnet run`: run `npm run build -- --configuration production` inside `src/SharpOMatic.FrontEnd` to surface the full frontend error output.
- The UI loads but API calls fail: confirm the backend is running on `http://localhost:9001`.

