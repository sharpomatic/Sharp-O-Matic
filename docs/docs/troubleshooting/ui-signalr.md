# UI SignalR

## Editor not loading
If `/editor` loads a blank page or 404:

- Confirm `app.MapSharpOMaticEditor("/editor")` is called in the host.
- Verify the base path matches the URL you are using.
- Ensure the editor assets were built and embedded (the Editor project runs `npm run build` during build).

## SignalR disconnects
The editor connects to the hub at `/editor/notifications`. If it repeatedly disconnects:

- Make sure WebSockets are enabled on your host or reverse proxy.
- Use sticky sessions when load balancing so the UI sticks to one instance.
- Check CORS settings if the editor UI is hosted on a different origin.

## UI data mismatch
If the UI throws DTO or schema errors, make sure the frontend and backend are built from the same commit and clear the browser cache. Schema or DTO changes between versions can cause rendering issues until the UI is rebuilt.

