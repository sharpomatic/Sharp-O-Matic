# Logging

## Logging defaults
SharpOMatic relies on the host application's logging pipeline. The sample server uses the standard ASP.NET Core logging configuration in `appsettings.json` and defaults to `Error` level for most categories. Inside the engine, most runtime information is surfaced through run history and trace data rather than verbose logs, with only a few startup warnings written to standard output.

## Structured logging
Because the host is a normal ASP.NET Core app, you can plug in structured logging providers such as Serilog, Seq, or OpenTelemetry. To capture run outcomes in your logs, implement `IEngineNotification` and register it with DI:

```csharp
public class RunLoggingNotification(ILogger<RunLoggingNotification> logger) : IEngineNotification
{
    public Task RunCompleted(Guid runId, Guid workflowId, RunStatus runStatus, string? outputContext, string? error)
    {
        logger.LogInformation("Run {RunId} for workflow {WorkflowId} finished with {Status}. Error={Error}",
            runId, workflowId, runStatus, error ?? "(none)");
        return Task.CompletedTask;
    }

    public void ConnectionOverride(Guid runId, Guid workflowId, Dictionary<string, string?> parameters)
    {
        // No-op. Used when you want to override connector fields.
    }
}
```

Register it:

```csharp
builder.Services.AddSingleton<IEngineNotification, RunLoggingNotification>();
```

## Troubleshooting with logs
Use logs to confirm host startup, database migrations, and editor routing. For run failures, rely on the Run panel traces first, then log the run error and status via `IEngineNotification` so you can correlate UI failures with backend events.

