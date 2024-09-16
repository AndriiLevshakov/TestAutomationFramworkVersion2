using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    catch (Exception ex)
                    {
                        // try adding some exception details in here or avoid the try catch block altogether
                        throw;
                    }
                    
                    // fix formatting
                    }

                return _logger;
            }
        }
    }
}
