namespace SharpOMatic.Tests.Workflows;

public sealed class StartNodeUnitTest
{
    [Fact]
    public async Task Workflow_must_have_start()
    {
        var workflow = new WorkflowBuilder()
            .Build();

        var exception = await Assert.ThrowsAsync<SharpOMaticException>(() => WorkflowRunner.RunWorkflow(workflow));
        Assert.Equal("Must have exactly one start node.", exception.Message);
    }

    [Fact]
    public async Task Workflow_cannot_have_2_start()
    {
        var workflow = new WorkflowBuilder()
            .AddStart()
            .AddStart()
            .Build();

        var exception = await Assert.ThrowsAsync<SharpOMaticException>(() => WorkflowRunner.RunWorkflow(workflow));
        Assert.Equal("Must have exactly one start node.", exception.Message);
    }

    [Fact]
    public async Task Start_only_node()
    {
        var workflow = new WorkflowBuilder()
            .AddStart()
            .Build();

        var run = await WorkflowRunner.RunWorkflow(workflow);
        Assert.NotNull(run);
        Assert.Equal(RunStatus.Success, run.RunStatus);
    }
}
