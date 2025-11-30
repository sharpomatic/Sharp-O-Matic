namespace SharpOMatic.Engine.Services;

public class NodeExecutionService(INodeQueue queue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var (runContext, contextObject, node) = await queue.DequeueAsync(stoppingToken);
                await ProcessNode(runContext, contextObject, node);
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown
                break;
            }
            catch
            {
            }
        }
    }

    private async Task ProcessNode(RunContext runContext, ContextObject nodeContext, NodeEntity node)
    {
        try
        {
            var nextNodes = await EngineService.RunNode(runContext, nodeContext, node);

            if (runContext.UpdateRunningThreadCount(nextNodes.Count - 1) == 0)
            {
                runContext.Run.RunStatus = RunStatus.Success;
                runContext.Run.Message = "Success";
                runContext.Run.Stopped = DateTime.Now;
                runContext.Run.OutputContext = runContext.TypedSerialization(nodeContext);
                await runContext.RunUpdated();
            }
            else
            {
                foreach (var nextNode in nextNodes)
                    queue.Enqueue(runContext, nextNode.NodeContext, nextNode.Node);
            }
        }
        catch (Exception ex)
        {
            // TODO, handle exception in the node, decrement count by one and then finish up
        }
    }
}
