using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using System.Text.Json;

namespace Core.Logger
{
    public static class TestFrameworkSetup
    {
        public static void InitializeLogging(string configFilePath)
        {
            NLogConfiguration config = NLogConfigLoader.LoadConfig(configFilePath);

            IConfigurationSection nlogSection = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string> {
                    {
                        "NLog", JsonSerializer.Serialize(config.NLog)
                    } })
                .Build().GetSection("NLog");

            LogManager.Configuration = new NLogLoggingConfiguration(nlogSection);
        }
    }
}
