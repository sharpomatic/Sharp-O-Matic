---
title: Image
sidebar_position: 3
---

If the model supports image input or image output, this tab is available.

**Image Input Path** must point to an **AssetRef** or a list that contains nothing but **AssetRef** values.
You can insert these **AssetRef** values using the editor for interactive development purposes.
Programmatically, you can add them when creating the initial context to insert into the workflow or inside a **Code** node.
Note that these assets must be image types; they cannot be text, PDF documents, or binary data.

See the [Assets](/docs/core-concepts/assets) page in Core Concepts for programmatic examples.

<img src="/img/modelcall-image.png" alt="Asset Substitution" width="600" style={{ maxWidth: '100%', height: 'auto' }} />
