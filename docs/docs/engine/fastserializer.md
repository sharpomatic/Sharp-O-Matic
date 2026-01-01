# FastSerializer

## Why FastSerializer exists
FastSerializer is a lightweight JSON tokenizer and parser used by the engine when it needs fast parsing with precise error locations. The primary consumer is `ContextEntryType.JSON`, which expects user-supplied JSON in context entries. The parser produces `ContextObject` and `ContextList` values without relying on full `System.Text.Json` object graphs.

## Error location reporting
The tokenizer tracks line, column, and absolute position as it scans the input. When it encounters invalid JSON, it throws a `FastSerializationException` that includes a `Location` object. This gives the UI and logs a precise location for invalid characters, unexpected tokens, or malformed structures.

## Limitations and constraints
FastSerializer is intentionally minimal:

- It supports JSON objects, arrays, strings, numbers, booleans, and null.
- Numbers are parsed into `int` or `double`.
- It does not support comments, trailing commas, or custom number formats.
- It produces only `ContextObject`, `ContextList`, and scalar values.

If you need richer JSON behavior, use `ContextEntryType.Expression` and generate structured output via C# instead.

