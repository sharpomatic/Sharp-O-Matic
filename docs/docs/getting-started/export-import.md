# Export and Import

## Export a workflow
SharpOMatic uses the **Transfer** page to export workflows and related assets. Open **Transfer** from the left navigation, then select the **Export** tab.

Exports can include:
- Workflows
- Connectors
- Models
- Assets (library assets)

For each category you can choose **All**, **None**, or **Custom**. Custom mode enables search, sorting, and pagination so you can pick specific items. Use the **Include secrets** toggle if you want connector and model secrets embedded in the export file. When you are ready, click **Export** to download a `.zip` package (the server names it `sharpomatic-export-YYYYMMDD-HHmmss.zip`).

## Import a workflow
Open the **Transfer** page and select the **Import** tab. Click **Choose Zip** and select a transfer package created by SharpOMatic. The server validates the archive and then imports workflows, connectors, models, and assets.

Import is an upsert operation. Items in the package overwrite or update existing items with the same IDs. If the package was exported without secrets, the import process preserves existing secrets for matching connectors and models when possible.

After import completes, a summary dialog shows how many items were imported. Return to **Workflows**, **Connectors**, **Models**, and **Assets** to confirm the expected items appear.

## Sharing best practices
Treat transfer packages like deployable artifacts:

- Export the smallest set of items needed for the target environment to avoid accidental overwrites.
- Avoid exporting secrets unless you explicitly want them in the destination.
- Store transfer packages securely, especially if they include secrets or proprietary workflows.
- Keep a simple versioning convention for exports (for example, include a date and environment in the filename).
