using System.ComponentModel;

namespace SharpOMatic.Server;

public static class Tools
{
    [Description("Get a friendly greeting.")]
    public static string GetGreeting()
    {
        return "Hello there!";
    }

    [Description("Get current time")]
    public static string GetTime(IServiceProvider services)
    {
        var clockService = services.GetRequiredService<IClockService>();
        return DateTimeOffset.Now.ToString();
    }
}


public interface IClockService
{
    public string Now { get; }
}

public class ClockService : IClockService
{
    public string Now
    {
        get
        {
            return DateTimeOffset.Now.ToString();
        }
    }
}
