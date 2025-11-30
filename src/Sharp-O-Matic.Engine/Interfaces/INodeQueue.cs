namespace SharpOMatic.Engine.Interfaces;

public interface INodeQueue
{
    void Enqueue(NodeEntity node, RunContext context);

    Task<(NodeEntity Node, RunContext Context)> DequeueAsync(CancellationToken cancellationToken);
}
