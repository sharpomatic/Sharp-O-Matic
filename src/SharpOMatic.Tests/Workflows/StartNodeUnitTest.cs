using SharpOMatic.Engine.Contexts;

namespace SharpOMatic.Tests.Workflows;

public sealed class StartNodeUnitTest
{
    [Fact]
    public async Task Workflow_must_have_start()
    {
        var workflow = new WorkflowBuilder()
            .Build();

        var exception = await Assert.ThrowsAsync<SharpOMaticException>(() => WorkflowRunner.RunWorkflow([], workflow));
        Assert.Equal("Must have exactly one start node.", exception.Message);
    }

    [Fact]
    public async Task Workflow_cannot_have_2_start()
    {
        var workflow = new WorkflowBuilder()
            .AddStart()
            .AddStart()
            .Build();

        var exception = await Assert.ThrowsAsync<SharpOMaticException>(() => WorkflowRunner.RunWorkflow([], workflow));
        Assert.Equal("Must have exactly one start node.", exception.Message);
    }

    [Fact]
    public async Task Start_only_node()
    {
        var workflow = new WorkflowBuilder()
            .AddStart()
            .Build();

        var run = await WorkflowRunner.RunWorkflow([], workflow);
        Assert.NotNull(run);
        Assert.Equal(RunStatus.Success, run.RunStatus);
    }

    [Fact]
    public async Task Start_no_initializing()
    {
        var workflow = new WorkflowBuilder()
            .AddStart("start", false)
            .Build();

        ContextObject ctx = [];
        ctx.Set<bool>("input.boolean", true);

        var run = await WorkflowRunner.RunWorkflow(ctx, workflow);

        Assert.NotNull(run);
        Assert.Equal(RunStatus.Success, run.RunStatus);
        Assert.NotNull(run.OutputContext);

        var outCtx = ContextObject.Deserialize(run.OutputContext);
        Assert.NotNull(outCtx);
        Assert.True(outCtx.Get<bool>("input.boolean"));
    }

    [Fact]
    public async Task Start_init_mandatory_present()
    {
        var workflow = new WorkflowBuilder()
            .AddStart("start", true, WorkflowBuilder.CreateBoolInput("input.boolean", false))
            .Build();

        ContextObject ctx = [];
        ctx.Set<bool>("input.boolean", true);
        ctx.Set<int>("input.integer", 42);

        var run = await WorkflowRunner.RunWorkflow(ctx, workflow);

        Assert.NotNull(run);
        Assert.Equal(RunStatus.Success, run.RunStatus);
        Assert.NotNull(run.OutputContext);

        var outCtx = ContextObject.Deserialize(run.OutputContext);
        Assert.NotNull(outCtx);
        Assert.True(outCtx.Get<bool>("input.boolean"));
        var hasInteger = outCtx.TryGet<int>("input.integer", out var _);
        Assert.False(hasInteger);
    }

    [Fact]
    public async Task Start_init_mandatory_missing_path()
    {
        var workflow = new WorkflowBuilder()
            .AddStart("start", true, WorkflowBuilder.CreateBoolInput("", false))
            .Build();

        ContextObject ctx = [];
        ctx.Set<int>("input.integer", 42);

        var run = await WorkflowRunner.RunWorkflow(ctx, workflow);
        Assert.NotNull(run);
        Assert.Equal(RunStatus.Failed, run.RunStatus);
        Assert.Equal("Start node path cannot be empty.", run.Error);
    }

    [Fact]
    public async Task Start_init_mandatory_missing_value()
    {
        var workflow = new WorkflowBuilder()
            .AddStart("start", true, WorkflowBuilder.CreateBoolInput("input.boolean", false))
            .Build();

        ContextObject ctx = [];
        ctx.Set<int>("input.integer", 42);

        var run = await WorkflowRunner.RunWorkflow(ctx, workflow);
        Assert.NotNull(run);
        Assert.Equal(RunStatus.Failed, run.RunStatus);
        Assert.Equal("Start node mandatory path 'input.boolean' cannot be resolved.", run.Error);
    }

    [Fact]
    public async Task Start_init_optional()
    {
        var workflow = new WorkflowBuilder()
            .AddStart("start", true, WorkflowBuilder.CreateBoolInput("input.boolean", true, true))
            .Build();

        var run = await WorkflowRunner.RunWorkflow([], workflow);
        Assert.NotNull(run);
        Assert.Equal(RunStatus.Success, run.RunStatus);
        var outCtx = ContextObject.Deserialize(run.OutputContext);
        Assert.NotNull(outCtx);
        Assert.True(outCtx.Get<bool>("input.boolean"));
    }
}
