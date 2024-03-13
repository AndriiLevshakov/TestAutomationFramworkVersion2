using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace Core
{
    public static class WebDriverManager
    {
        private static IWebDriver? _driver;

        public static IWebDriver Driver(bool headlessMode)
        {            
                if (_driver == null)
                {
                    var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var browserType = configuration.GetValue<BrowserType>("AppSettings:WebDriverConfig:BrowserType");
                    _driver = WebDriverFactory.CreateDriver(browserType, headlessMode);
                }

                return _driver;            
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();

                _driver = null;
            }
        }
    }
}
