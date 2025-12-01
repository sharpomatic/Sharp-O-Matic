namespace SharpOMatic.Engine.Interfaces;

public interface INodeQueue
{
    void Enqueue(ThreadContext threadContext, NodeEntity node);
    ValueTask<(ThreadContext threadContext, NodeEntity node)> DequeueAsync(CancellationToken cancellationToken);
}
