# Context Data

## Context objects and lists
Every node reads and writes a single context object. The engine represents this as a `ContextObject` (a dictionary of string keys to values) and a `ContextList` (an ordered list of values). These types are the only containers the runtime understands, and they are serialized to JSON for run inputs, outputs, and traces.

Context entries are defined using `ContextEntryListEntity` and `ContextEntryEntity`. Each entry includes a purpose (Input, Upsert, Delete, Output), a path, an optional flag, a type, and a value. The Start node uses entries to initialize inputs, Edit nodes use entries to upsert or delete values, and the End node can map input paths to output paths.

Serialization uses custom JSON converters so the engine can preserve `ContextObject`, `ContextList`, and known external types (for example, `ChatMessage` and `AssetRef`). You can register additional converters if you need custom types to round-trip.

## Variable access patterns
Paths use dot notation for objects and brackets for list indexes. Each property segment must be a valid C# identifier (no spaces, and keywords must be escaped), because identifiers are validated at runtime. Examples:

```text
customer.name
items[0].price
output.summary.total
```

The engine can create missing object segments when setting values, but it will not auto-create lists. If a path traverses a list, the list must already exist and the index must be in range. When working with a list as the root, the path must start with an index like `[0].name`.

Expression entries run C# script in a context where `Context` is the current `ContextObject`. This lets you compute values dynamically, for example:

```csharp
Context.Get<int>("items[0].count") * 2
```

If an entry is marked optional and the value is missing, the engine can fall back to a default or skip the assignment depending on the node type.

## JSON constraints
Values must be JSON-serializable. `ContextEntryType.JSON` expects valid JSON and is parsed using the fast JSON parser used by the engine. `ContextEntryType.AssetRef` and `AssetRefList` expect JSON that matches the `AssetRef` shape (`assetId`, `name`, `mediaType`, `sizeBytes`), and invalid entries fail the run.

If you need to store complex types, register custom converters via `AddSharpOMaticEngine().AddJsonConverters(...)` in your host app so the engine can serialize and deserialize them. Keep in mind that invalid paths, invalid identifiers, or type mismatches are treated as runtime errors and will fail the run.

