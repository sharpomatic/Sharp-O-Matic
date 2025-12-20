namespace SharpOMatic.Engine.Services;

public class ToolMethodRegistry(IEnumerable<Delegate> methods) : IToolMethodRegistry
{
    private readonly List<Delegate> _methods = methods.ToList();

    public IReadOnlyList<Delegate> GetMethods() => _methods.AsReadOnly();

    public IReadOnlyList<string> GetToolDisplayNames()
    {
        return [.. _methods.Select(m => m.Method.Name).OrderBy(m => m)];
    }

    public Delegate? GetToolFromDisplayName(string displayName)
    {
        return _methods.FirstOrDefault(m => m.Method.Name == displayName);
    }
}
