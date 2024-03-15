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

        public WebDriverManager()
        {
            _appConfiguration = GetConfiguration();
        }

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
            {              

                _driver = WebDriverFactory.CreateDriver(_appConfiguration.BrowserType, _appConfiguration.HeadlessMode);
            }

            return _driver;
        }

        public AppConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var appConfiguration = new AppConfiguration();
            configuration.Bind("BrowserType", appConfiguration.BrowserType);
            configuration.Bind("HeadlessMode", appConfiguration.HeadlessMode);
            configuration.Bind("BaseUrl", appConfiguration.BaseUrl);

            return appConfiguration;
        }

        public static IWebDriver CurrentDriver
        {
            get
            {
                if ( _driver == null)
                {
                    _driver = GetDriver();
                }

                return _driver;
            }
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
