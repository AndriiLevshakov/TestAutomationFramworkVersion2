using Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;
using System.Configuration;
using NUnit.Framework;
using System.IO;
using Business;

namespace TestLayer
{
    public abstract class BaseTestFixtures
    {
        protected IConfiguration _configuration;
        protected ILogger _logger;
        public bool _headlessMode;
        protected HomePage _homePage;

        public BaseTestFixtures() 
        {
            // you should move configuration initialization to a class with a SetUpFixture attribute
            // so that you wouldn't read the same configuration before every single test executing
            // it can be written there into a static field and referenced here
            // https://docs.nunit.org/articles/nunit/writing-tests/attributes/setupfixture.html

            // alternatively, you can create a ConfigurationManager class
            // that would be responsible for reading configuration once
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("NLog.json", optional: false, reloadOnChange: true)
                .Build();

            _logger = LogManager.GetCurrentClassLogger();
            _headlessMode = _configuration.GetValue<bool>("AppSettings:WebDriverConfig:HeadlessMode");
            
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
