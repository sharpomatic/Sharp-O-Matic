# Build Install

## Setup failures
- `dotnet` errors about unsupported framework usually mean the .NET 10 SDK is missing. Install the SDK and confirm with `dotnet --version`.
- The editor build requires Node.js and npm. If `npm` is missing, install Node and ensure it is on PATH.

## Build failures
The `SharpOMatic.Editor` project runs `npm ci` and `npm run build -- --configuration production` automatically. If the build fails:

1) Run `npm ci` in `src/SharpOMatic.FrontEnd` to restore packages.
2) Run `npm run build -- --configuration production` from the same folder to see detailed output.
3) Rebuild the .NET solution.

## Environment mismatches
If the frontend builds but the editor does not load, verify that the frontend and backend are built from the same commit. Mismatched builds can result in DTO or route mismatches.

