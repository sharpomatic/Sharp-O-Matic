# Upgrades

## Upgrade planning
Review release notes and breaking changes, then take a full backup of the database and assets. If you have custom connectors, models, or code nodes, validate them in a staging environment first.

## Migration steps
1) Update your package references or pull the latest source.
2) Rebuild the host and restart it so EF Core migrations can run.
3) If you have disabled automatic migrations (`ApplyMigrationsOnStartup`), run migrations manually from the Engine project or enable it temporarily.

## Post-upgrade validation
Load the editor UI, open a workflow, and run it. Confirm runs complete, connectors can authenticate, and assets are accessible. Review logs for migration or startup warnings.

