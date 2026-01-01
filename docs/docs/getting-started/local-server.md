# Local Server

## Run the sample server
SharpOMatic.Server is a local host that wires the Engine and Editor together. Start it from the repo root:

```bash
dotnet run --project src/SharpOMatic.Server/SharpOMatic.Server.csproj
```

The server listens on `http://localhost:9001`. On the first run, the build will also generate the embedded editor assets by running `npm ci` and `npm run build -- --configuration production` inside `src/SharpOMatic.FrontEnd`. That first build can take a few minutes.

If you prefer an IDE workflow, open `src/SharpOMatic.sln` and run the SharpOMatic.Server project.

## Configure storage and database
The sample host uses SQLite and a local file system asset store by default.

Database:
- The SQLite file is created under your LocalApplicationData folder.
- On Windows the default path is `%LOCALAPPDATA%\sharpomatic.db`.

Assets:
- Assets are stored on disk using `FileSystemAssetStore`.
- The default root is `%LOCALAPPDATA%\SharpOMatic\Assets`.

To change the asset location, set `AssetStorage:FileSystem:RootPath` in `src/SharpOMatic.Server/appsettings.json` or another configuration source. To change the database location or provider, update the `UseSqlite` configuration in `src/SharpOMatic.Server/Program.cs`.

## Validate the setup
Once the server is running:

- Open `http://localhost:9001/editor` to load the editor UI.
- Hit `http://localhost:9001/editor/api/status` to confirm the editor API responds with 200 OK.

If the editor page is blank, the embedded UI build may have failed. Check the console output from the `dotnet run` command to confirm the Angular build succeeded.

