namespace SharpOMatic.Engine.Entities.Definitions;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class NodeEntityAttribute(NodeType nodeType) : Attribute
{
    public NodeType NodeType { get; } = nodeType;
}
