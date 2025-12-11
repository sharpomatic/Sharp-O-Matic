namespace SharpOMatic.Engine.Entities.Definitions;

[NodeEntity(NodeType.ModelCall)]
public class ModelCallNodeEntity : NodeEntity
{
    public required string ModelName { get; set; }
    public required string Instructions { get; set; }
    public required string Prompt { get; set; }
}
