namespace SharpOMatic.Engine.Services;

public class ToolMethodRegistry(IEnumerable<MethodInfo> methods) : IToolMethodRegistry
{
    private readonly List<MethodInfo> _methods = methods.ToList();

    public IReadOnlyList<MethodInfo> GetMethods() => _methods.AsReadOnly();

    public IReadOnlyList<string> GetMethodDisplayNames()
    {
        return [.. _methods.Select(m => m.Name).OrderBy(m => m)];
    }

    public MethodInfo? GetMethodFromDisplayName(string displayName)
    {
        return _methods.FirstOrDefault(m => m.Name == displayName);
    }
}
