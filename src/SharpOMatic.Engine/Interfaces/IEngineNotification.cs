namespace SharpOMatic.Engine.Interfaces;

public interface IEngineNotification
{
    public Task RunCompleted(Guid runId, Guid workflowId, RunStatus runStatus, string? outputContext, string? error);
    public void ConnectionOverride(Guid runId, Guid workflowId, Dictionary<string, string?> parameters);
}
