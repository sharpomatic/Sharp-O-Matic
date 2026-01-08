namespace SharpOMatic.Engine.Services;

public class RunContextFactory : IRunContextFactory
{
    public RunContext Create(
        IServiceScope serviceScope,
        WorkflowEntity workflow,
        Run run,
        IEnumerable<JsonConverter> jsonConverters,
        int runNodeLimit,
        TaskCompletionSource<Run>? completionSource)
    {
        return new RunContext(
            serviceScope,
            jsonConverters,
            workflow,
            run,
            runNodeLimit,
            completionSource);
    }
}
