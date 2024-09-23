using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;

namespace Core
{
    public static class LoggerManager
    {
        private static ILogger _logger;

        public static ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    try
                    {
                        var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("NLog.json", optional: true, reloadOnChange: true)
                            .Build();

                        LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
                        _logger = LogManager.GetCurrentClassLogger();
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine("NLog configuration file not found: " + ex.Message);
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to initialize logger: " + ex.Message);
                        throw;
                    }
                }

                return _logger;
            }
        }
    }
}
