using dotenv.net;

namespace Configuration
{
    public static class AppConfiguration
    {
        private static IDictionary<string, string> envVars = null;
        static AppConfiguration()
        {
            DotEnv.Load();
            envVars = DotEnv.Read();
        }

        public static string EventHubNamespaceUrl => 
            envVars["EVENT_HUB_NAMESPACE_URL"] 
            ?? throw new InvalidOperationException("EVENT_HUB_NAMESPACE_URL environment variable is not set");

        public static string EventHubName => 
            envVars["EVENT_HUB_NAME"] 
            ?? throw new InvalidOperationException("EVENT_HUB_NAME environment variable is not set");

        public static void ValidateConfiguration()
        {
            try
            {
                _ = EventHubNamespaceUrl;
                _ = EventHubName;
                Console.WriteLine("✓ Configuration validation passed");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✗ Configuration validation failed: {ex.Message}");
                throw;
            }
        }
    }
}