namespace SharpOMatic.Engine.Services;

public class RunContextFactory(IServiceScopeFactory scopeFactory) : IRunContextFactory
{
    public RunContext Create(
        WorkflowEntity workflow,
        Run run,
        IEnumerable<JsonConverter> jsonConverters,
        int runNodeLimit,
        TaskCompletionSource<Run>? completionSource)
    {
        var scope = scopeFactory.CreateScope();

        return new RunContext(
            scope,
            jsonConverters,
            workflow,
            run,
            runNodeLimit,
            completionSource);
    }
}
