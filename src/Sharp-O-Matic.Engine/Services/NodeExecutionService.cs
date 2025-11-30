namespace SharpOMatic.Engine.Services;

public class NodeExecutionService(INodeQueue queue) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var (node, context) = await queue.DequeueAsync(stoppingToken);
                await ProcessNode(node, context);
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown
                break;
            }
            catch (Exception)
            {
            }
        }
    }

    private async Task ProcessNode(NodeEntity node, RunContext context)
    {
        try
        {
            var nextNodes = await EngineService.RunNode(context, node);
            foreach (var nextNode in nextNodes)
            {
                queue.Enqueue(nextNode, context);
            }
        }
        catch
        {
        }
    }
}
