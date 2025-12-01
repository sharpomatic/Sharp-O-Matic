using SharpOMatic.Engine.Contexts;

namespace SharpOMatic.Engine.Nodes;

public class FanInNode(ThreadContext threadContext, FanInNodeEntity node) : RunNode<FanInNodeEntity>(threadContext, node)
{
    protected override async Task<(string, List<NextNodeData>)> RunInternal()
    {
        if (threadContext.Parent is null)
            throw new SharpOMaticException($"Arriving thread did not originate from a FanOut.");

        // If multiple threads arrive at this node at the same time, serialize so they merge correctly
        lock(threadContext.Parent)
        {
            if (threadContext.Parent.FanInMergedContext is null)
                threadContext.Parent.FanInMergedContext = threadContext.NodeContext;
            else
                threadContext.RunContext.MergeContexts(threadContext.Parent.FanInMergedContext, threadContext.NodeContext);

            threadContext.Parent.FanInArrived++;
            if (threadContext.Parent.FanInArrived < threadContext.Parent.FanOutCount)
            {               
                // Exit thread, exited wait for other threads to arrive
                return ("Thread arrived", []);
            }
            else
            {
                threadContext.Parent.FanInArrived = 0;
                threadContext.Parent.FanOutCount = 0;

                // Must set the merged context into this thread, so tracing progresses merges
                threadContext.NodeContext = threadContext.Parent.FanInMergedContext;
                threadContext.Parent.NodeContext = threadContext.NodeContext;
                return ("Threads synchronized", [new NextNodeData(threadContext.Parent, RunContext.ResolveSingleOutput(Node))]);
            }
        }
    }
}
