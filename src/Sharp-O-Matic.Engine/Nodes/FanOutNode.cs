namespace SharpOMatic.Engine.Nodes;

public class FanOutNode(RunContext runContext, FanOutNodeEntity node)
    : RunNode<FanOutNodeEntity>(runContext, node)
{
    public override async Task<IEnumerable<NodeEntity>> Run()
    {
        await base.Run();

        try
        {
            // Resolve all outputs for FanOut
            var nextNodes = RunContext.ResolveMultipleOutputs(Node);

            Trace.Message = "Threads started";
            Trace.NodeStatus = NodeStatus.Success;
            Trace.Finished = DateTime.Now;
            Trace.OutputContext = RunContext.TypedSerialization(RunContext.NodeContext);
            await NodeUpdated();

            return nextNodes;
        }
        catch (Exception ex)
        {
            Trace.NodeStatus = NodeStatus.Failed;
            Trace.Finished = DateTime.Now;
            Trace.Message = "Failed";
            Trace.Error = ex.Message;
            Trace.OutputContext = RunContext.TypedSerialization(RunContext.NodeContext);
            await NodeUpdated();

            throw;
        }
    }
}
