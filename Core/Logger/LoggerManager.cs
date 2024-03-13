using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;

namespace Core.Logger
{
    public static class LoggerManager
    {
        private static ILogger? _logger;

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
                    catch (Exception)
                    {
                        throw new Exception("File is missing");
                    }
                }

                return _logger;
            }
        }
    }
}
