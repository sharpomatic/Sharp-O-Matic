namespace SharpOMatic.Engine.Services;

public class NodeQueueService : INodeQueue
{
    private readonly Channel<(NodeEntity Node, RunContext Context)> _queue;

    public NodeQueueService()
    {
        _queue = Channel.CreateUnbounded<(NodeEntity, RunContext)>();
    }

    public void Enqueue(NodeEntity node, RunContext context)
    {
        _queue.Writer.TryWrite((node, context));
    }

    public ValueTask<(NodeEntity Node, RunContext Context)> DequeueAsync(CancellationToken cancellationToken)
    {
        return _queue.Reader.ReadAsync(cancellationToken);
    }
}
