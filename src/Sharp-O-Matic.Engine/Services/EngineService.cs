namespace SharpOMatic.Engine.Services;

public class EngineService(IRepository Repository,
                           INodeQueue Queue,
                           INotification Notifications,
                           IEnumerable<JsonConverter> JsonConverters) : IEngine
{


    public async Task<Guid> RunWorkflow(Guid workflowId, ContextObject? context = null, ContextEntryListEntity? inputEntries = null)
    {
        context ??= [];

        string? inputJson = null;
        if (inputEntries is not null)
        {
            inputJson = JsonSerializer.Serialize(inputEntries);

            foreach (var entry in inputEntries!.Entries)
            {
                var entryValue = await ContextHelpers.EvaluateContextEntryValue(context, entry);
                if (!context.TrySet(entry.InputPath, entryValue))
                    throw new SharpOMaticException($"Input entry '{entry.InputPath}' could not be assigned the value.");
            }
        }

        var run = new Run()
        {
            WorkflowId = workflowId,
            RunId = Guid.NewGuid(),
            RunStatus = RunStatus.Created,
            Message = "Created",
            Created = DateTime.Now,
            InputEntries = inputJson,
        };

        await RunUpdated(run);

        var workflow = await Repository.GetWorkflow(workflowId) ?? throw new SharpOMaticException($"Could not load workflow {workflowId}.");
        var currentNodes = workflow.Nodes.Where(n => n.NodeType == NodeType.Start).ToList();
        if (currentNodes.Count != 1)
            throw new SharpOMaticException("Must have exactly one start node.");

        context.Add("WorkflowId", workflowId);
        context.Add("RunId", run.RunId);

        var runContext = new RunContext(Repository, Notifications, JsonConverters, workflow, run.RunId, context);

        run.RunStatus = RunStatus.Running;
        run.Message = "Running";
        run.Started = DateTime.Now;
        run.InputContext = runContext.TypedSerialization(runContext.NodeContext);
        await RunUpdated(run);

        Queue.Enqueue(currentNodes[0], runContext);

        return run.RunId;
    }



    public static Task<IEnumerable<NodeEntity>> RunNode(RunContext runContext, NodeEntity node)
    {
        return node switch
        {
            StartNodeEntity startNode => new StartNode(runContext, startNode).Run(),
            EndNodeEntity endNode => new EndNode(runContext, endNode).Run(),
            EditNodeEntity editNode => new EditNode(runContext, editNode).Run(),
            CodeNodeEntity codeNode => new CodeNode(runContext, codeNode).Run(),
            SwitchNodeEntity switchNode => new SwitchNode(runContext, switchNode).Run(),
            FanInNodeEntity fanInNode => new FanInNode(runContext, fanInNode).Run(),
            FanOutNodeEntity fanOutNode => new FanOutNode(runContext, fanOutNode).Run(),
            _ => throw new SharpOMaticException($"Unrecognized node type' {node.NodeType}'")
        };
    }

    private async Task RunUpdated(Run run)
    {
        await Repository.UpsertRun(run);
        await Notifications.RunProgress(run);
    }
}
