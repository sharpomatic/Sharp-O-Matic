namespace SharpOMatic.Engine.Interfaces;

public interface IEngine
{
    Task<Guid> RunWorkflow(Guid workflowId, ContextObject? context = null, ContextEntryListEntity? inputEntries = null);
}
