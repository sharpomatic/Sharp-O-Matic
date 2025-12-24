# Repository Project

SharpoMatic is an open source project on GitHub that allows a user to build and execute workflows with an emphasis on AI related tasks.
The frontend is an Angular and Typescript browser based application contained in the `SharpOMatic.Editor/` directory.
The backend is .NET C# based and consists of an Engine, Server and Test projects.

## Project Structure & Module Organization
- `SharpOMatic.sln` is the root .NET solution file for the backend.
    It point to the project files for the Engine, Server and Test projects. 
    It does not include the Editor, that is built and run separately.

- `SharpOMatic.Engine/` is a .NET 10 class library with the core implementation for the backend.
- `SharpOMatic.Server/` is a .NET 10 ASP.NET Core API + SignalR hub; static files are served here.
- `SharpOMatic.Editor/` is a Angular and Typescript frontend that communicates with a Server backend.
- `SharpOMatic.Test/` is the xUnit test project targeting the Engine.

### Project Structure Engine

- `SharpOMatic.Engine/Contexts`
    ContextObject allows name/value pairs of data to be accessed during execution of nodes in a workflow and is implemented like a C# dictionary type.
    ContextList acts as a list of values. Both of these can hold scalar values as well as references to objects.
    Combining use of these two context types allows complex hierarchies of data to be read/added/removed/updated from the workflow nodes.
    To allow a workflow to be suspended and saved to a database, all the values in this hierarchy must be serializable to JSON.
    This directory contains some built-in converters but any other types must be registered via the JsonConverterService.

    RunContext and ThreadContext are used to group data together when executing a workflow.
    There is a single RunContext for a workflow execution but one ThreadContext for each thread of execution, because nodes can be run in parallel.

- `SharpOMatic.Engine/DataTransferObjects`
    Classes that end in Request are incoming sets of data from the frontend and those ending in Result are answers.
    These are only needed for special purpose calls as most REST calls use Entities or Metadata, which are defined in their specific directories.

- `SharpOMatic.Engine/Entities`
    Contains definitions of the workflow nodes along with helper entities that the nodes use.
    All entities end with the word Entity in the name. They store all the user defined settings for operation of that node.
    Each entity has an instance id so they can be referenced from elsewhere by id. 
    They have a version number so that they can be upgraded in the future automatically as new releases of the project are made.

- `SharpOMatic.Engine/Enumerations`
    Shared enumerations that are used across more than one directory.
    Enumerations needed in only a single directory will usually be placed in that directory.

- `SharpOMatic.Engine/Exceptions`
    Domain-specific exception types for the engine, including syntax errors and base engine exceptions, so failures can be reported with meaningful detail.

- `SharpOMatic.Engine/FastSerializer`
    Custom JSON tokenizer/deserializer used by the engine to parse serialized data efficiently while keeping accurate line and column error locations.
    Is a fast serializer that goes from JSON text to the ContextObject and ContextList instances. 
    This makes it appropriate to use when putting data into the workflows context and saving the workflows context to text.

- `SharpOMatic.Engine/Helpers`
    Small utility types and helpers used across the engine, including identifier validation, context helpers, and shared location metadata.
    This is the place to put extensions methods for base types or helper classes/extensiosn that cut across multiple directories.

- `SharpOMatic.Engine/Interfaces`
    Service contracts and engine abstractions (queueing, execution, repository, converters, schema types) that define DI boundaries and make testing easier.
    Any new service should have an interface that is placed in here.

- `SharpOMatic.Engine/Metadata`
    Metadata is used to define connectors and models. 
    A connector config represents a possible connection target for operations.
    For example, OpenAI services, Azure services and potentially any other 3rd party API. 
    The connector configs can be presented to the user for selection from. 
    The connector instance references a connector config and stores the actual user enter details about the connection, such as url and authentication details.
    Model config is used to define an LLM model and defines the capabilities of the model such as tooling, image generation.
    A model instance comes from a selected model config and allows the user to specify details like structure output

- `SharpOMatic.Engine/Migrations`
    Entity Framework Core migrations and snapshots that define the persisted schema for workflows, runs, and other database tables.

- `SharpOMatic.Engine/Nodes`
    Runtime implementations of workflow nodes (start, end, fan-in/out, switch, code, model call, etc.) plus shared node helpers and attributes.
    Each node implemenation has a class name ending in the word Node. It implements validation checking as well as run time operation.

- `SharpOMatic.Engine/Repository`
    EF Core DbContext and persistence models for workflows, runs, and other database tables.

- `SharpOMatic.Engine/Services`
    Core engine services for node execution, queue management, repository access, JSON conversion, schema typing, and engine configuration.
    A service is in here because it can be added to the services collection for dependency injection.
    Also has extension methods to help with correct setup of the services.
    The NodeExecutionService is a hosted background service that pulls nodes from a queue for processing.
    NodeQueueService is a queue of nodes that need to be processed by the NodeExecutionService.
    The RepositoryService is used by other services to get and set information to the backing database.

 - NOTES: The backend uses the Rosalyn compiler services to validate and run C# code snippits. The ability to run C# code entered by the user is
   crucial in giving the user flexibility when building a workflow.

### Project Structure Editor

- `SharpOMatic.Editor/src/app`
    Root Angular application structure, containing the feature folders and app bootstrapping assets that wire routing, configuration, and shared state together.

- `SharpOMatic.Editor/src/app/components`
    Reusable UI components (designer, context viewer, tabs, dynamic fields, etc.).
    These components embedded inside other components such as dialogs and pages.

- `SharpOMatic.Editor/src/app/data-transfer-objects`
    Client-side DTO shapes for API payloads such as code-check requests/results, mirroring the backend's DTO contracts.

- `SharpOMatic.Editor/src/app/dialogs`
    Modal dialog components for editing nodes, confirming actions, and showing informative or blocking messages in the editor.
    Most of the dialog are shown when a user double click a workflow node so the user can edit the properties of that node.

- `SharpOMatic.Editor/src/app/entities`
    Client-side entity models that mirror the engine's workflow entities, plus helpers like factories for creating node instances.

- `SharpOMatic.Editor/src/app/enumerations`
    UI enums for node/run status and other state flags used across components, services, and templates.

- `SharpOMatic.Editor/src/app/guards`
    Route guards that protect navigation (for example, warning about unsaved changes) and enforce editor flow rules.
    When the user navigates away from a page that has been changed, it gives the user a chance to save or cancel navigating.

- `SharpOMatic.Editor/src/app/metadata`
    Metadata definitions and enums for connector/model configuration so the editor can render dynamic fields and validation.
    Using configuration of fields from metadata is essential in making it fast and easy to define the fields presented to a user.

- `SharpOMatic.Editor/src/app/pages`
    Routed pages for core editor experiences like workflows, workflow editing, connectors, models, and settings, with page-scoped services where needed.
    The UI sidebar lists the pages and when clicked they navigate to one of these pages.

- `SharpOMatic.Editor/src/app/services`
    Angular services for API access, SignalR communication, metadata loading, Monaco integration, settings persistence, and toasts.

 - NOTES: The user interface uses the monaco editor that is also used in Visual Studio Code in order to display and allow editing
   of JSON and C# code snippets. This gives a good experience for the user. It calls the backend to validate the C# snippets.

## Build, Test, and Development Commands
- `dotnet build SharpOMatic.sln` builds all .NET projects.
- `dotnet run --project SharpOMatic.Server/SharpOMatic.Server.csproj` runs the API at `http://localhost:9001`.
- `dotnet test SharpOMatic.Test/SharpOMatic.Engine.Test.csproj` runs engine unit tests (coverlet collector).
- `cd SharpOMatic.Editor; npm install` installs UI dependencies when first setting up.
- `cd SharpOMatic.Editor; npm run start` runs the Angular dev server; `npm run build` builds for production; `npm test` runs Karma/Jasmine.
- `dotnet tool install --global dotnet-ef` once, then `dotnet ef migrations add <Name>` for EF migrations (run from `SharpOMatic.Engine/`).

## Coding Style & Naming Conventions
- C#: 4-space indentation; nullable is enabled; follow .NET conventions (PascalCase types/methods/properties, camelCase locals/parameters, `I` prefix for interfaces).
- Angular/TypeScript: 2-space indentation and single quotes for `.ts` per `SharpOMatic.Editor/.editorconfig`; HTML formatted via Prettier.
- File naming: Angular uses kebab-case (`my-widget.component.ts`); tests use `*.spec.ts`; C# tests typically end with `*UnitTest(s).cs`.

## Testing Guidelines
- Backend tests use xUnit in `SharpOMatic.Test/` and target the engine library.
- Frontend tests use Karma/Jasmine and live alongside components as `.spec.ts`.
- Add unit tests for new engine logic and for UI flows that affect editor behavior.

## Commit & Pull Request Guidelines
- Recent commits are short, descriptive, and prefix-free; keep messages to a single line (e.g., "OpenAI parameters").
- PRs should describe intent and scope, link any related issues/TODOs, include screenshots/GIFs for editor UI changes, and state tests run.

## Security & Configuration Tips
- The server stores SQLite data under the user LocalApplicationData path; do not commit local `.db` files.
- `SharpOMatic.Server/appsettings.json` is the place for environment configuration; keep secrets out of the repo.
