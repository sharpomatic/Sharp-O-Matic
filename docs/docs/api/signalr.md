# SignalR

## Connection setup
The editor connects to the Notification hub at `/notifications` under the editor base path. With the default host, the URL is:

```
http://localhost:9001/editor/notifications
```

The current UI uses WebSockets only (`skipNegotiation: true`), so your host and any reverse proxy must support WebSockets on that route.

## Events and payloads
Two events are broadcast to all connected clients:

- `RunProgress` with a `Run` payload.
- `TraceProgress` with a `Trace` payload.

The UI uses these events to update node status, trace messages, and the run panel in real time.

## Reconnect behavior
The default front end does not enable automatic reconnect. If the SignalR connection drops, the UI will stop receiving live updates until the connection is restarted or the page is refreshed. If you embed the editor in your own app, you can opt into automatic reconnect by configuring the SignalR client with `withAutomaticReconnect()`.

