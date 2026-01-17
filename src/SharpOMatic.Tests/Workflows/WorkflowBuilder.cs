using SharpOMatic.Engine.Entities.Definitions;
using SharpOMatic.Engine.Entities.Enumerations;

namespace SharpOMatic.Tests.Workflows;

public sealed class WorkflowBuilder
{
    private Guid _workflowId = Guid.NewGuid();
    private string _name = "Test Workflow";
    private string _description = "Generated for tests.";
    private readonly List<NodeEntity> _nodes = [];
    private readonly List<ConnectionEntity> _connections = [];

    public WorkflowBuilder WithId(Guid id)
    {
        _workflowId = id;
        return this;
    }

    public WorkflowBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public WorkflowBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public WorkflowBuilder AddStart(string title = "start")
    {
        var node = new StartNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.Start,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [],
            Outputs = [CreateConnector()],
            ApplyInitialization = false,
            Initializing = CreateContextEntryList()
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddEnd(string title = "end")
    {
        var node = new EndNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.End,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = [],
            ApplyMappings = false,
            Mappings = CreateContextEntryList()
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddCode(string title = "code", string code = "")
    {
        var node = new CodeNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.Code,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = [CreateConnector()],
            Code = code
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddEdit(string title = "edit")
    {
        var node = new EditNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.Edit,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = [CreateConnector()],
            Edits = CreateContextEntryList()
        };

        _nodes.Add(node);
        return this;
    }

    public record class SwitchChoice(string Name, string Code);

    public WorkflowBuilder AddSwitch(string title = "switch", IEnumerable<SwitchChoice>? switchChoices = null)
    {
        var choices = switchChoices is null ? [] : switchChoices.ToArray();
        if (choices.Length < 2)
            throw new ArgumentException("AddSwitch must have at least one switch choice");

        var switches = new SwitchEntryEntity[choices.Length];
        foreach(var choice in choices)
        {
            switches.Append(new SwitchEntryEntity
            {
                Id = Guid.NewGuid(),
                Version = 1,
                Name = choice.Name ?? string.Empty,
                Code = choice.Code ?? string.Empty
            }
        }

        var node = new SwitchNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.Switch,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = CreateConnectors([.. choices.Select(c => c.Name)]),
            Switches = switches
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddFanIn(string title = "fanin")
    {
        var node = new FanInNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.FanIn,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = [CreateConnector()],
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddFanOut(string title = "fanout", IEnumerable<string>? outputNames = null)
    {
        var names = outputNames is null ? [] : outputNames.ToArray();
        if (names.Length < 2)
            throw new ArgumentException("AddFanOut must have at least one output name");

        var node = new FanOutNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.FanOut,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = CreateConnectors(names),
            Names = names
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddModelCall(string title = "modelcall")
    {
        var node = new ModelCallNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.ModelCall,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = [CreateConnector()],
            ModelId = null,
            Instructions = string.Empty,
            Prompt = string.Empty,
            ChatInputPath = string.Empty,
            ChatOutputPath = string.Empty,
            TextOutputPath = "output.text",
            ImageInputPath = string.Empty,
            ImageOutputPath = "output.image",
            ParameterValues = []
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddBatch(string title = "batch", int batchSize = 10, int parallelBatches = 3)
    {
        var node = new BatchNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.Batch,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = CreateConnectors("continue", "process"),
            InputArrayPath = string.Empty,
            BatchSize = batchSize,
            ParallelBatches = parallelBatches
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddGosub(string title = "gosub", Guid? workflowId = null)
    {
        var node = new GosubNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.Gosub,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = [CreateConnector()],
            WorkflowId = workflowId
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowBuilder AddInput(string title = "input")
    {
        var node = new InputNodeEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            NodeType = NodeType.Input,
            Title = title,
            Top = 0f,
            Left = 0f,
            Width = 80f,
            Height = 80f,
            Inputs = [CreateConnector()],
            Outputs = [CreateConnector()],
        };

        _nodes.Add(node);
        return this;
    }

    public WorkflowEntity Build()
    {
        return new WorkflowEntity
        {
            Id = _workflowId,
            Version = 1,
            Name = _name,
            Description = _description,
            Nodes = _nodes.ToArray(),
            Connections = _connections.ToArray()
        };
    }

    public static ContextEntryListEntity CreateContextEntryList(params ContextEntryEntity[] entries)
    {
        return new ContextEntryListEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            Entries = entries ?? Array.Empty<ContextEntryEntity>()
        };
    }

    private static ConnectorEntity CreateConnector(string name = "")
    {
        return new ConnectorEntity
        {
            Id = Guid.NewGuid(),
            Version = 1,
            Name = name ?? string.Empty
        };
    }

    private static ConnectorEntity[] CreateConnectors(params string[] names)
    {
        if (names is null || names.Length == 0)
        {
            return Array.Empty<ConnectorEntity>();
        }

        var connectors = new ConnectorEntity[names.Length];
        for (var i = 0; i < names.Length; i++)
        {
            connectors[i] = CreateConnector(names[i] ?? string.Empty);
        }

        return connectors;
    }

}
