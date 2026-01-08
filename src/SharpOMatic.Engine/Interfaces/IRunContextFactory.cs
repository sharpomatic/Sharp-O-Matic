using System.Threading.Tasks;

namespace SharpOMatic.Engine.Interfaces;

public interface IRunContextFactory
{
    RunContext Create(
        IServiceScope serviceScope,
        WorkflowEntity workflow,
        Run run,
        IEnumerable<JsonConverter> jsonConverters,
        int runNodeLimit,
        TaskCompletionSource<Run>? completionSource);
}
