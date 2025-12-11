namespace SharpOMatic.Engine.Repository;

public class ConnectionMetadata
{
    [Key]
    public required Guid ConnectionId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Config { get; set; }
}
