# Prerequisites

## Required tooling
SharpOMatic is a .NET 10 backend with an Angular editor. To build and run the project locally you need:

- .NET SDK 10.0 (the SDK, not just the runtime)
- Node.js and npm (for the editor build)

Verify that the tools are installed and on your PATH:

```bash
dotnet --version
node --version
npm --version
```

If you only plan to consume the NuGet packages in your own app, the .NET SDK is still required. If you plan to build or run the editor UI from this repo, Node.js and npm are also required because the editor build runs during the .NET build.

## Optional tools
You can work with SharpOMatic using any editor, but a full IDE is convenient for mixed C# and TypeScript work. Common choices include Visual Studio, VS Code, and JetBrains Rider. A modern browser with dev tools is helpful when debugging the Angular UI. If you want to inspect the local SQLite database, a SQLite browser is useful but not required.

## Repository layout overview
These folders are the ones you will touch most during local development:

- `src/SharpOMatic.sln` loads the .NET solution.
- `src/SharpOMatic.Server` is the sample host you run locally.
- `src/SharpOMatic.Editor` packages the editor UI and its HTTP and SignalR endpoints.
- `src/SharpOMatic.Engine` is the workflow runtime, persistence, and execution engine.
- `src/SharpOMatic.FrontEnd` is the Angular SPA source for the editor UI.

