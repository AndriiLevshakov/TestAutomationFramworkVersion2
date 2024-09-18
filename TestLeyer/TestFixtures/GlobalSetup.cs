using Microsoft.Extensions.Configuration;
using NLog;

namespace TestLayer
{
    [SetUpFixture]
    public class GlobalSetup
    {
        public static IConfiguration Configuration { get; private set; }
        public static ILogger Logger { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("NLog.json", optional: false, reloadOnChange: true)
                .Build();

            Logger = LogManager.GetCurrentClassLogger();
        }
    }
}
