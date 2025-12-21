namespace SharpOMatic.Engine.Repository;

public class ConnectorConfigMetadata
{
    [Key]
    public required string ConfigId { get; set; }
    public required int Version { get; set; }
    public required string Config { get; set; }
}
