using Core.WebDriver.Configuration;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace Core.WebDriver
{
    public class WebDriverManager
    {
        private static IWebDriver? _driver;

        private static AppConfiguration? _appConfiguration { get; set; }
        public static string BaseUrl => _appConfiguration.BaseUrl;

        public static IWebDriver CurrentDriver => _driver ??= GetDriver();

        public WebDriverManager()
        {
            _appConfiguration = GetConfiguration();
        }

        public static IWebDriver GetDriver()
        {
            return WebDriverFactory.CreateDriver(_appConfiguration.BrowserType, _appConfiguration.HeadlessMode);
        }

        public AppConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var appConfiguration = new AppConfiguration();
            configuration.Bind(appConfiguration);

            return appConfiguration;
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
