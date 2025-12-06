namespace SharpOMatic.Engine.Interfaces;

public interface IRepository
{
    IQueryable<Workflow> GetWorkflows();
    Task<WorkflowEntity> GetWorkflow(Guid workflowId);
    Task UpsertWorkflow(WorkflowEntity workflow);
    Task DeleteWorkflow(Guid workflowId);

    IQueryable<Run> GetRuns();
    IQueryable<Run> GetWorkflowRuns(Guid workflowId);
    Task UpsertRun(Run run);

    IQueryable<Trace> GetTraces();
    IQueryable<Trace> GetRunTraces(Guid runId);
    Task UpsertTrace(Trace trace);

    Task<ConnectionConfig?> GetConnectionConfig(string id);
    Task UpsertConnectionConfig(ConnectionConfig config);
}
