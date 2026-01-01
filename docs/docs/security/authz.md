# Authz

## Authentication options
SharpOMatic does not ship with a built-in auth system. The editor and API endpoints are standard ASP.NET Core controllers and a SignalR hub, so you should apply authentication at the host level (cookies, JWT, OpenID Connect, or an upstream reverse proxy).

## Authorization model
There is no role-based authorization inside the editor itself. Any user who can reach `/editor` and `/editor/api/*` can manage workflows, connectors, runs, and settings. Treat the editor as an admin surface and apply a global or path-based authorization policy in your host.

## Hardening checklist
- Require authentication for `/editor`, `/editor/api/*`, and `/editor/notifications`.
- Limit CORS to trusted origins if you expose the API separately.
- Keep the editor off the public internet or behind VPN when possible.
- Use least-privilege hosting accounts and secure database/asset access.
- Set sensible run limits in Settings to prevent runaway workflows.

