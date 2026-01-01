# Export Import

## Scenario and goals
This tutorial shows how to export a workflow and its dependencies and import them into another environment. It is useful for backups, sharing, or moving from local to hosted servers.

## Export flow
1) Open **Transfer** and select the **Export** tab.
2) Choose what to include:
   - Workflows (select the workflow you want to share).
   - Connectors and models if the workflow depends on them.
   - Assets if the workflow references library assets.
3) Decide whether to include secrets.
4) Click **Export** and save the generated `.zip` file.

## Import and validate
1) On the target environment, open **Transfer** and select the **Import** tab.
2) Upload the exported `.zip`.
3) Review the import summary for the number of items imported.
4) Open the workflow and run it to confirm expected behavior.

If you excluded secrets during export, you may need to re-enter connector credentials before the workflow can run successfully.

