# Templates Sharing

## Templates overview
SharpOMatic does not ship with a built-in template gallery yet. Instead, treat a workflow as a reusable template by exporting it and importing it into another environment. This approach preserves the node graph and configuration while keeping the workflow editable once it is imported.

## Import and export flows
Use the **Transfer** page to package workflows for sharing. Export can include workflows, connectors, models, and assets, and can optionally include secrets. Import applies the package as an upsert, so items with matching IDs are updated in place.

If you are using a workflow as a template, export only the items needed by that workflow and omit secrets unless the destination environment should receive them.

## Collaboration tips
When sharing workflows across a team:

- Give workflows descriptive names and keep their descriptions up to date.
- Export only what you need to avoid overwriting unrelated connectors, models, or assets.
- Use a simple versioning convention in export filenames (for example, date + environment).
- Keep secrets out of exports unless you explicitly want them propagated.

