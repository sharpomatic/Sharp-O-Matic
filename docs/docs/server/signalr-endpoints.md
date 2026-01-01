# SignalR Endpoints

## SignalR hubs
The editor host exposes a single SignalR hub: `NotificationHub`. It is mapped under the editor base path, so the default URL is:

```
http://localhost:9001/editor/notifications
```

The hub streams run and trace updates to connected clients.

## Message flow
The engine reports progress through `IProgressService`, which the editor host implements using SignalR. Two event names are emitted:

- `RunProgress` with a `Run` payload.
- `TraceProgress` with a `Trace` payload.

The front end subscribes to both and updates the canvas and run panel in real time. If a run fails, the error message is part of the `Run` payload and appears in the UI.

## Debugging connectivity
If live updates are not showing:

- Confirm the editor base URL is correct in **Settings** (the hub URL is `${apiUrl}/notifications`).
- Verify the server is reachable and that WebSockets are enabled (the client uses WebSockets only).
- Check your reverse proxy configuration to ensure `/editor/notifications` is forwarded and supports WebSockets.
- Inspect the browser console for SignalR connection errors.

