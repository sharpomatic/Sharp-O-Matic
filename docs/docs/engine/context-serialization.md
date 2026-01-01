# Context Serialization

## Serialization model
Context data is serialized with a typed JSON format so the engine can preserve structure and types across runs and traces. `ContextObject` and `ContextList` are encoded with a `$type` and `value` wrapper, and scalar values are encoded with explicit type tokens.

When the engine persists a run or trace, it uses `RunContext.TypedSerialization`, which builds JSON options with:

- `ContextObjectConverter` and `ContextListConverter` for container types.
- `ContextTypedValueConverter` for typed values.
- External type mappings (for example `ChatMessage` and `AssetRef`).

This format allows the engine to deserialize the exact same structures on subsequent reads.

## Custom converters
You can register custom JSON converters via the engine builder:

```csharp
builder.Services.AddSharpOMaticEngine()
    .AddJsonConverters(typeof(MyCustomConverter));
```

The engine collects these converters through `IJsonConverterService` and includes them in every run serialization. If you store custom types in context values, you must register converters on every host that needs to deserialize those runs.

## Compatibility concerns
Run and trace records store serialized context as JSON strings. Database migrations do not rewrite those payloads, so changes to your custom types or converters can break deserialization of older runs. When evolving types:

- Keep type tokens stable.
- Prefer additive changes with defaults.
- Provide backwards-compatible converters if the shape changes.

If you need to migrate historical run data, plan a separate migration tool that reads old JSON payloads and rewrites them using the new schema.

