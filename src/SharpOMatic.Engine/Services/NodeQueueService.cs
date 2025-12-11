namespace SharpOMatic.Engine.Services;

public class NodeQueueService : INodeQueue
{
    private readonly Channel<(ThreadContext threadContext, NodeEntity node)> _queue;

    public NodeQueueService()
    {
        _queue = Channel.CreateUnbounded<(ThreadContext, NodeEntity)>();
    }

    public void Enqueue(ThreadContext threadContext, NodeEntity node)
    {
        _queue.Writer.TryWrite((threadContext, node));
    }

    public ValueTask<(ThreadContext threadContext, NodeEntity node)> DequeueAsync(CancellationToken cancellationToken)
    {
        return _queue.Reader.ReadAsync(cancellationToken);
    }
}
