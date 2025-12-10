namespace SharpOMatic.Engine.Nodes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class RunNodeAttribute(NodeType nodeType) : Attribute
{
    public NodeType NodeType { get; } = nodeType;
}
