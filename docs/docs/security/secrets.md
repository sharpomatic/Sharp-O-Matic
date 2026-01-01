# Secrets

## Secrets storage
Connector and model secret fields are stored in the repository database. The API obfuscates secret values when returned to the UI and export bundles strip secret fields, but the stored values are not encrypted. Treat the database and its backups as sensitive.

Application-level secrets (database connection strings, storage credentials, etc.) should live in environment variables or a secret manager, not in source control.

## Configuration hygiene
Use environment-specific config files or environment variables for settings such as `AssetStorage` and database connection strings. Rotate secrets by updating connector/model fields in the editor and redeploying your host with updated app settings.

## Audit guidance
Regularly review configuration files and exported transfer bundles to ensure secrets are not checked into source control. Validate that your runtime environment has restricted access to the database and asset store, and rotate any credentials that were exposed.

