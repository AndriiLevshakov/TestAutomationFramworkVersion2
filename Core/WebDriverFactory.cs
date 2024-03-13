using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Core
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(BrowserType browserType, bool headlessMode)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return CreateChromeDriver(headlessMode);
                case BrowserType.Edge:
                    return CreateEdgeDriver(headlessMode);
                default:
                    throw new ArgumentException($"Unsupported browser type: {browserType}");
            }
        }

        public static IWebDriver CreateEdgeDriver(bool headlessMode)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadPath = Path.Combine(userPath, "Downloads");

            var options = new EdgeOptions();
            
            options.AddArguments("--window-size=1920,1080");
            options.AddUserProfilePreference("download.default_directory", downloadPath);

            if (headlessMode)
            {
                options.AddArguments("--headless");
            }

            return new EdgeDriver(options);

        }

        public static IWebDriver CreateChromeDriver(bool headlessMode)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadPath = Path.Combine(userPath, "Downloads");

            var options = new ChromeOptions();            

            if (headlessMode)
            {
                options.AddArguments("--headless");
            }

            options.AddArguments("--window-size=1920,1080");
            options.AddUserProfilePreference("download.default_directory", downloadPath);

            return new ChromeDriver(options);

        }
    }
}
