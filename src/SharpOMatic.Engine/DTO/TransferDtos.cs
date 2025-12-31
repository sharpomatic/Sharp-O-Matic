namespace SharpOMatic.Engine.DTO;

public sealed class TransferExportRequest
{
    public bool IncludeSecrets { get; set; }
    public TransferSelection? Workflows { get; set; }
    public TransferSelection? Connectors { get; set; }
    public TransferSelection? Models { get; set; }
    public TransferSelection? Assets { get; set; }
}

public sealed class TransferSelection
{
    public bool All { get; set; }
    public List<Guid> Ids { get; set; } = [];
}

public sealed class TransferManifest
{
    public const int CurrentSchemaVersion = 1;

    public int SchemaVersion { get; set; } = CurrentSchemaVersion;
    public DateTime CreatedUtc { get; set; }
    public bool IncludeSecrets { get; set; }
    public TransferCounts Counts { get; set; } = new();
    public List<TransferAssetEntry> Assets { get; set; } = [];
}

public sealed class TransferCounts
{
    public int Workflows { get; set; }
    public int Connectors { get; set; }
    public int Models { get; set; }
    public int Assets { get; set; }
}

public sealed class TransferAssetEntry
{
    public Guid AssetId { get; set; }
    public string Name { get; set; } = "";
    public string MediaType { get; set; } = "";
    public long SizeBytes { get; set; }
    public DateTime Created { get; set; }
}

public sealed class TransferImportResult
{
    public int WorkflowsImported { get; set; }
    public int ConnectorsImported { get; set; }
    public int ModelsImported { get; set; }
    public int AssetsImported { get; set; }
}
