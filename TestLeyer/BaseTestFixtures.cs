using Core.Logger;
using Core.WebDriver;
using Microsoft.Extensions.Configuration;

namespace TestLeyer
{
    public abstract class BaseTestFixtures
    {
        protected IConfiguration? _configuration;
        public bool _headlessMode;
        protected WebDriverManager _webDriverManager;

        protected BaseTestFixtures()
        {
            _webDriverManager = new WebDriverManager();
        }

        [SetUp]
        public void SetUp()
        {
            LoggerManager.Logger.Info($"Starting {TestContext.CurrentContext.Test.MethodName}");

            WebDriverManager.CurrentDriver.Navigate().GoToUrl(WebDriverManager.BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            string? testName = TestContext.CurrentContext.Test.MethodName;

            ScreenShot.CaptureScreenshot(WebDriverManager.CurrentDriver, testName + ".png");

            LoggerManager.Logger.Info("Test successfully finished");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            WebDriverManager.CurrentDriver.Close();
        }
    }
}
