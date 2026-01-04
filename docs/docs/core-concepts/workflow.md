---
title: Workflow
sidebar_position: 1
---

A workflow defines a set of nodes that perform actions and connections that control execution flow.
Use the browser based editor to create and configure workflows. 
Run them via the editor to assess the results and rapidly iterate your designs.
Once completed you can invoke workflows programmatically in your deployed environment.

## Nodes

This is a summary of the more common nodes.

- **Start** - each workflow must have a single start node as the entry point.
- **End** - there can be multiple end nodes if there are multiple paths to completion.
- **Edit** - add, update or delete entries from the context.
- **Code** - run a C# block of code as glue logic or to call into your backend.
- **ModelCall** - make an LLM call to an external provider.
- **Switch** - choose between multiple output paths.
- **FanOut** / **FanIn** - run multiple paths in parallel.

## Connections

A connection is a link between two nodes and controls the execution flow. 

A node output can only be connected to the input of a single other node.
For example, the **Start** node has a single output to control which node is executed next.
This output can only be connected to the input of a single node.
You could not link the output to multiple nodes in an attempt to run several nodes in parallel.
Use the **FanOut** / **FanIn** nodes to achieve parallel operation.

There can however, but multiple inputs to a node.
For example, the **End** node has a single input but there can be multiple incoming connections.
One scenario where this might happen is use of a **Switch** node, the switch might have two output paths and each path ends by connecting to the **End** node.
Only one of the two paths will actually be executed but both can converge into the same node.

## Inputs and Outputs

Nodes read and write from a context as the mechansim for passing information around the workflow.

An initial context is provided to the **Start** node.
If the workflow needs input parameters then these are provided by the user, or programmatically, in that initial context.
On workflow completion the final context is provided as the workflow result.

The context can be used to store intermediate and temporary values.
It can be modified using configuration via the **Edit** node or for full access use the **Code** node.

## Runs and Traces

Each workflow run creates a record in the database.
This tracks basic information such as the starting time, status, initial context, final context and more.
Use the editor to see a list of the previus runs and track how successful your workflow is performing.

With each run there are trace records that record information about the execution of each individual node.
This is useful for tracking down which node raised an error and allows you to view the context at the time of the failure.
These can be seen in the editor by selecting a run.

## Workflow Completion

A workflow completes when either a node execution error is encountered or when all the execution paths are finished.

It is not mandatory to use **End** nodes in your flow but it is definintely recommended as best practice. 
Without end nodes it is the context from the last node executed that is the workflow result.
If there are multiple paths, particularly if they run in parallel, then the order in which paths are completed can vary.
This means different nodes finishing last and so you get a different context.

The **End** node has the ability to select which context entries are output as the workflow results.
Letting you ignore intermediate and working values that are not relevant to the work outcome.
This gives a smaller and cleaner outcome.


