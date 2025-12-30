using SharpOMatic.Engine.Repository;

namespace SharpOMatic.Engine.DTO;

public record class WorkflowRunPageResult(List<Run> Runs, int TotalCount);
