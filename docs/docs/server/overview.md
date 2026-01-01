# Overview

## Server responsibilities
The server host is the glue between the Engine and the Editor. It wires up the workflow runtime, persistence, and background execution services from `SharpOMatic.Engine`, and it exposes the editor UI, HTTP APIs, and SignalR hub from `SharpOMatic.Editor`. In other words, the host is responsible for:

- Serving the embedded editor UI at a chosen base path.
- Exposing editor APIs for workflows, runs, assets, connectors, models, and settings.
- Streaming run and trace updates over SignalR.
- Configuring storage, metadata, scripting, tools, and schema types for the engine.

## Default setup
The sample host in `src/SharpOMatic.Server` is configured for local development:

- Binds to `http://localhost:9001`.
- Enables CORS with permissive settings for local UI testing.
- Registers the Editor and Transfer controllers and the Notification hub.
- Uses the file system asset store.
- Uses SQLite for the repository under your LocalApplicationData folder.
- Registers example schema types and tool methods for the engine.
- Hosts the editor at `/editor`.

This setup is intentionally minimal but end-to-end complete, so you can validate workflows, model calls, assets, and run history without building your own host first.

## Local testing workflow
Run the sample server and open the editor UI:

```bash
dotnet run --project src/SharpOMatic.Server/SharpOMatic.Server.csproj
```

Then open `http://localhost:9001/editor` and confirm you can:

- Create a workflow.
- Add nodes and run it.
- See live run and trace updates on the canvas.
- View run history and details.

