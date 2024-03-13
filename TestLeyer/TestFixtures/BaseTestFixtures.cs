using Core;
using Microsoft.Extensions.Configuration;
using NLog;
using Business;
using Core.Logger;

namespace TestLayer
{
    public abstract class BaseTestFixtures
    {
        protected IConfiguration _configuration;
        protected ILogger _logger;
        public bool _headlessMode;
        protected HomePage _homePage;

        protected BaseTestFixtures() 
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("NLog.json", optional: false, reloadOnChange: true)
                .Build();

            _logger = LogManager.GetCurrentClassLogger();
            _headlessMode = _configuration.GetValue<bool>("AppSettings:WebDriverConfig:HeadlessMode");
            
        }

        static BaseTestFixtures()
        {
            TestFrameworkSetup.InitializeLogging("NLog.json");
        }

        protected string BaseUrl => _configuration["AppSettings:BaseUrl"];
        
        protected bool HeadlessMode => _headlessMode;

        [SetUp]
        public void SetUp()
        {
            WebDriverManager.Driver(_headlessMode).Navigate().GoToUrl(BaseUrl);

            _homePage = new HomePage(WebDriverManager.Driver(_headlessMode));
        }

        [TearDown]
        public void TearDown()
        {
            WebDriverManager.QuitDriver();
        }

        protected ILogger Logger => _logger;
    }
}
