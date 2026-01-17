namespace SharpOMatic.Engine.Interfaces;

public interface IJsonConverterService
{
    IEnumerable<JsonConverter> GetConverters();
}
