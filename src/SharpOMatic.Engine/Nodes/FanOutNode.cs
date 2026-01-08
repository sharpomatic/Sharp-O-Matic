namespace SharpOMatic.Engine.Nodes;

[RunNode(NodeType.FanOut)]
public class FanOutNode(ThreadContext threadContext, FanOutNodeEntity node) : RunNode<FanOutNodeEntity>(threadContext, node)
{
    protected override async Task<(string, List<NextNodeData>)> RunInternal()
    {
        var resolveNodes = RunContext.ResolveMultipleOutputs(Node);
        var json = ThreadContext.NodeContext.Serialize(RunContext.JsonConverters);

        List<NextNodeData> nextNodes = [];
        foreach(var resolveNode in resolveNodes)
        {
            var newContext = ContextObject.Deserialize(json, RunContext.JsonConverters);
            var newThreadContext = new ThreadContext(RunContext, newContext, ThreadContext);
            nextNodes.Add(new NextNodeData(newThreadContext, resolveNode));
        }

        ThreadContext.FanOutCount = nextNodes.Count;
        ThreadContext.FanInArrived = 0;

        return ($"{nextNodes.Count} threads started", nextNodes);
    }
}
