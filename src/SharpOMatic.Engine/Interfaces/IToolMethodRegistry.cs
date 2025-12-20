namespace SharpOMatic.Engine.Interfaces;

public interface IToolMethodRegistry
{
    public IReadOnlyList<MethodInfo> GetMethods();
    public IReadOnlyList<string> GetMethodDisplayNames();
    public MethodInfo? GetMethodFromDisplayName(string displayName);
}

