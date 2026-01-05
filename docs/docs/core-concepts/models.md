---
title: Models
sidebar_position: 3
---

Models describe which LLM to call and which parameters to use.
They are the reusable configuration layer that **ModelCall** nodes reference at runtime.

## Model Configs

Model configs are metadata definitions that describe provider-specific capabilities and parameter fields.
They also specify which connector config is required, if any, so the editor can enforce compatibility.

## Model Instances

A model is an instance of a model config.
You choose the connector (when required) and set default parameter values such as temperature or token limits.
These defaults travel with the model and can be applied consistently across workflows.

## Usage

Select a model in a **ModelCall** node to execute it.
If the model requires a connector, the engine resolves the connector when the node runs.
