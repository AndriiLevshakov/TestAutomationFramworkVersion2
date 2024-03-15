using Microsoft.Extensions.Configuration;
using NLog;
using Business;
using Core.Logger;
using Core.WebDriver;
using Core.WebDriver.Configuration;

namespace TestLeyer
{
    public abstract class BaseTestFixtures
    {
        protected IConfiguration _configuration;
        protected ILogger _logger;
        public bool _headlessMode;
        protected WebDriverManager _webDriverManager;

        protected BaseTestFixtures()
        {
            _webDriverManager = new WebDriverManager();
        //    //_configuration = new ConfigurationBuilder()
        //    //    .SetBasePath(Directory.GetCurrentDirectory())
        //    //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    //    .AddJsonFile("NLog.json", optional: false, reloadOnChange: true)
        //    //    .Build();

        //    //_logger = LogManager.GetCurrentClassLogger();
        //    //_headlessMode = _configuration.GetValue<bool>("AppSettings:WebDriverConfig:HeadlessMode");

        }

        //static BaseTestFixtures()
        //{
        //    //TestFrameworkSetup.InitializeLogging("NLog.json");
        //}

        //protected string BaseUrl => ;

        //protected bool HeadlessMode => _headlessMode;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            WebDriverManager.GetDriver();
        }

        [SetUp]
        public void SetUp()
        {
            WebDriverManager.CurrentDriver.Navigate().GoToUrl(WebDriverManager.BaseUrl); 
        }

        [TearDown]
        public void TearDown()
        {
            WebDriverManager.QuitDriver();
        }

        protected ILogger Logger => _logger;
    }
}
