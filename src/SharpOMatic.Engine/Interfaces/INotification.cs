namespace SharpOMatic.Engine.Interfaces;

public interface INotification
{
    Task RunProgress(Run run);
    Task TraceProgress(Trace trace);
}
