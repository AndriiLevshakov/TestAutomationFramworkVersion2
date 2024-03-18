using Microsoft.Extensions.Configuration;
using NLog;
using Core.Logger;
using Core.WebDriver;
using Autofac;
using Business;
using OpenQA.Selenium;
using Core.WebDriver.Configuration;

namespace TestLeyer
{
    public abstract class BaseTestFixtures
    {
        protected IConfiguration? _configuration;
        protected ILogger? _logger;
        public bool _headlessMode;
        protected WebDriverManager _webDriverManager;
        protected IContainer Container;
        private HomePage _homePage;

        protected BaseTestFixtures()
        {
            _webDriverManager = new WebDriverManager();

            var builder = new ContainerBuilder();

            builder.Register(c => WebDriverManager.GetDriver()).As<IWebDriver>().InstancePerLifetimeScope();

            builder.RegisterType<HomePage>().As<HomePage>();

            Container = builder.Build();
        }

        [SetUp]
        public void SetUp()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                _homePage = scope.Resolve<HomePage>();
            }
                LoggerManager.Logger.Info($"Starting {TestContext.CurrentContext.Test.MethodName}");

            WebDriverManager.CurrentDriver.Navigate().GoToUrl(WebDriverManager.BaseUrl); 
        }

        [TearDown]
        public void TearDown()
        {
            string? testName = TestContext.CurrentContext.Test.MethodName;

            ScreenShot.CaptureScreenshot(WebDriverManager.CurrentDriver, testName + ".png");

            LoggerManager.Logger.Info("Test successfully completed.");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Container.Dispose();

            WebDriverManager.CurrentDriver.Close();
        }
    }
}
