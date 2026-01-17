namespace SharpOMatic.Engine.Entities.Definitions;

[NodeEntity(NodeType.Gosub)]
public class GosubNodeEntity : NodeEntity
{
    public required Guid? WorkflowId { get; set; }
}
