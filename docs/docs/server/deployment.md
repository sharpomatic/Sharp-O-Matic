# Deployment

## Deployment models
SharpOMatic is hosted as an ASP.NET Core app, so deployment options follow standard .NET patterns. Common models include:

- Hosting the sample `SharpOMatic.Server` directly (local, VM, or container).
- Embedding the editor and engine into your existing ASP.NET Core app.
- Hosting behind a reverse proxy (IIS, Nginx, Traefik) with a stable base path for the editor UI.

## Build and publish
Use `dotnet publish` to build the server. The editor UI is embedded during the build, so the publish step will run the frontend build as needed.

```bash
dotnet publish src/SharpOMatic.Server/SharpOMatic.Server.csproj -c Release -o out
```

Deploy the contents of the output folder and run the server using your standard ASP.NET Core hosting strategy.

## Operational checklist
Before going live, verify:

- The editor loads at your configured base path.
- API calls succeed under `/editor/api/*`.
- SignalR connects and emits run/trace updates.
- Database migrations are applied or managed explicitly.
- Asset storage is writable and properly secured.
- Secrets are injected via a secure mechanism (not source control).
- CORS and HTTPS settings match your deployment environment.

