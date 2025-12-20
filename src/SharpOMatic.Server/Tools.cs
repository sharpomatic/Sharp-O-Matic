using System.ComponentModel;

namespace SharpOMatic.Server
{
    public static class Tools
    {
        [Description("Get a friendly greeting.")]
        public static string GetGreeting()
        {
            return "Hello there!";
        }

        [Description("Get current time")]
        public static string GetTime()
        {
            return DateTime.Now.ToString();
        }
    }
}
