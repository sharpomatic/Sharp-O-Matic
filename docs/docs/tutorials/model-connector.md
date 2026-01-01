# Model Connector

## Scenario and goals
This tutorial walks you through connecting a model provider, creating a model instance, and calling it from a workflow. You will configure a connector, create a model, and then use a Model Call node to produce text output.

## Connector setup
1) Go to **Connectors** and click **New**.
2) Choose a connector config (for example, OpenAI or Azure OpenAI).
3) Select the authentication mode and fill in the required fields (API key, endpoint, etc.).
4) Save the connector.

Next, create a model instance:

1) Go to **Models** and click **New**.
2) Choose the connector you just created.
3) Pick a model config from the list (these are driven by metadata).
4) Adjust any model parameters and save.

## Run and evaluate
Now add the model call to a workflow:

1) Create or open a workflow and add **Start**, **Model Call**, and **End** nodes.
2) Open the Model Call dialog and select the model you created.
3) In the **Text** tab, set:
   - **Text Output Path** to `output.text`.
   - **Instructions** and **Prompt** with a simple request.
4) Save and run the workflow.

When the run completes, open the **Output** tab and confirm that `output.text` contains the model response.

