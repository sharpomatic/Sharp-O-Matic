# Metadata

## Metadata purpose
Metadata describes connectors and models in a structured, versioned way. The editor uses metadata to render forms for authentication and model parameters, and the engine uses it to validate and execute model calls. This keeps the UI dynamic and allows new connectors or models to be added without hardcoding every field.

At startup, the engine loads embedded JSON metadata resources into the database. Connector and model instances created by users reference these metadata configs by `ConfigId`, which makes validation and UI rendering consistent.

## Metadata structure
There are two layers of metadata:

- **Config metadata**: describes available connectors and models.
  - `ConnectorConfig` includes `ConfigId`, display name, description, and one or more authentication modes.
  - `ModelConfig` includes `ConfigId`, display name, connector dependency, capability flags, and parameter fields.
- **Instance metadata**: stores user-specific configurations.
  - `Connector` stores the chosen authentication mode and field values.
  - `Model` stores parameter values and an optional reference to a connector.

Field definitions use `FieldDescriptor` with a type (String, Secret, Boolean, Enum, Integer, Double), required flag, default value, and optional constraints like min/max/step. These definitions drive the editor form layout and validation behavior.

## Validation flow
The editor validates required fields and type constraints before saving connectors or models. The engine performs a second round of validation when a Model Call node executes: it resolves the model and connector configuration, checks for missing configs, and uses the parameter values for the call.

When metadata changes (for example, a new config version or a new required field), existing connectors and models may require updates. Transfer imports respect metadata by merging secrets when they are omitted, which keeps credentials from being overwritten unintentionally.

