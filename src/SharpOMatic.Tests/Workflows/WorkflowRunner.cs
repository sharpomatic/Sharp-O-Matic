namespace SharpOMatic.Tests.Workflows;

public class WorkflowRunner
{
    public static async Task<Run> RunWorkflow(params WorkflowEntity[] workflows)
    {
        // Create full set of engine services
        var services = new ServiceCollection();
        services.AddSharpOMaticEngine();

        // Override the repository with a simple in memory test version
        services.AddSingleton<IRepositoryService, TestRepositoryService>();

        await using var provider = services.BuildServiceProvider();

        // Add all provided workflows to the database
        var repositoryService = provider.GetRequiredService<IRepositoryService>();
        foreach (var workflow in workflows)
            await repositoryService.UpsertWorkflow(workflow);

        // Provide cancellation token so we can kill the node execution after the test
        using var cts = new CancellationTokenSource();
        var executionService = provider.GetRequiredService<INodeExecutionService>();
        var queueTask = executionService.RunQueueAsync(cts.Token);

        try
        {
            // Run the first provided workflow
            await using var scope = provider.CreateAsyncScope();
            var engine = scope.ServiceProvider.GetRequiredService<IEngineService>();
            var runId = await engine.CreateWorkflowRun(workflows[0].Id);
            var run = await engine.StartWorkflowRunAndWait(runId);
            return run;

        }
        finally
        {
            // Always kill the background node executor, wait for it to die before exiting
            cts.Cancel();
            await queueTask;
        }
    }
}
