using Core;
using Microsoft.Extensions.Configuration;
using Business;
using NUnit.Framework.Interfaces;

namespace TestLayer.TestFixtures
{
    public abstract class BaseTestFixtures
    {
        public bool _headlessMode;
        protected HomePage _homePage;

        protected string BaseUrl => GlobalSetup.Configuration["AppSettings:BaseUrl"];
        protected bool HeadlessMode => GlobalSetup.Configuration.GetValue<bool>("AppSettings:WebDriverConfig:HeadlessMode");

        [SetUp]
        public void SetUp()
        {
            WebDriverManager.Driver(_headlessMode).Navigate().GoToUrl(BaseUrl);

            _homePage = new HomePage(WebDriverManager.Driver(_headlessMode));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                ScreenShot.CaptureScreenshot(WebDriverManager.Driver(HeadlessMode), TestContext.CurrentContext.Test.Name);
            }

            WebDriverManager.QuitDriver();
        }
    }
}
