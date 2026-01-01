# Settings

## Editor settings
The Settings page lets you configure the editor’s backend connection and server-side settings. The **Server** field sets the API base URL used by the UI when making requests. Below that, you’ll see user-editable settings pulled from the server (for example, run history or run node limits), rendered with the appropriate input type.

## Persistence of preferences
The API base URL is stored in the browser’s local storage. Clearing the field resets it to the default URL derived from the current page, and clearing local storage resets it entirely.

Server settings are persisted in the backend database. Changes are saved automatically when you blur a field or toggle a checkbox, so you do not need an explicit Save button.

## Workspace management
Use the Workflows list to create, open, and delete workflows. For environment-specific setups, point the **Server** field to the correct host and keep connectors and models scoped to that environment. For sharing or backup, export workflows and dependencies from the Transfer page and import them into the target environment.

