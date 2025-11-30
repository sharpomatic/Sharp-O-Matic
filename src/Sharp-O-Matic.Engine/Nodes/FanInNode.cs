namespace SharpOMatic.Engine.Nodes;

public class FanInNode(RunContext runContext, FanInNodeEntity node)
    : RunNode<FanInNodeEntity>(runContext, node)
{
    public override async Task<IEnumerable<NodeEntity>> Run()
    {
        await base.Run();

        try
        {
            var nextNode = RunContext.ResolveSingleOutput(Node);

            Trace.Message = "Threads synchronized";
            Trace.NodeStatus = NodeStatus.Success;
            Trace.Finished = DateTime.Now;
            Trace.OutputContext = RunContext.TypedSerialization(RunContext.NodeContext);
            await NodeUpdated();

            return nextNode != null ? new[] { nextNode } : Enumerable.Empty<NodeEntity>();
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
