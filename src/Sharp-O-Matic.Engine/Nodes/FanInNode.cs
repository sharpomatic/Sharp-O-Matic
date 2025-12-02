using SharpOMatic.Engine.Contexts;

namespace SharpOMatic.Engine.Nodes;

public class FanInNode(ThreadContext threadContext, FanInNodeEntity node) : RunNode<FanInNodeEntity>(threadContext, node)
{
    protected override async Task<(string, List<NextNodeData>)> RunInternal()
    {
        if (ThreadContext.Parent is null)
            throw new SharpOMaticException($"Arriving thread did not originate from a FanOut.");

        // If multiple threads arrive at this node at the same time, serialize so they merge correctly
        lock(ThreadContext.Parent)
        {
            if (ThreadContext.Parent.FanInMergedContext is null)
                ThreadContext.Parent.FanInMergedContext = ThreadContext.NodeContext;
            else
                ThreadContext.RunContext.MergeContexts(ThreadContext.Parent.FanInMergedContext, ThreadContext.NodeContext);

            ThreadContext.Parent.FanInArrived++;
            if (ThreadContext.Parent.FanInArrived < ThreadContext.Parent.FanOutCount)
            {               
                // Exit thread, exited wait for other threads to arrive
                return ("Thread arrived", []);
            }
            else
            {
                ThreadContext.Parent.FanInArrived = 0;
                ThreadContext.Parent.FanOutCount = 0;

                // Must set the merged context into this thread, so tracing progresses merges
                ThreadContext.NodeContext = ThreadContext.Parent.FanInMergedContext;
                ThreadContext.Parent.NodeContext = ThreadContext.NodeContext;
                return ("Threads synchronized", [new NextNodeData(ThreadContext.Parent, RunContext.ResolveSingleOutput(Node))]);
            }
        }
    }
}
