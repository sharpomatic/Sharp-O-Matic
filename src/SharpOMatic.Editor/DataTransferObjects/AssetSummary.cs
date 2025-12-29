namespace SharpOMatic.Editor.DataTransferObjects;

public record class AssetSummary(Guid AssetId, string Name, string MediaType, long SizeBytes, AssetScope Scope, DateTime Created);
