using Core.WebDriver.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace Core.WebDriver
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver(BrowserTypes BrowserType, bool HeadlessMode)
        {
            switch (BrowserType)
            {
                case BrowserTypes.Chrome:
                    return CreateChromeDriver(HeadlessMode);
                case BrowserTypes.Edge:
                    return CreateEdgeDriver(HeadlessMode);
                default:
                    throw new ArgumentException($"Unsupported browser type: {BrowserType}");
            }
        }

        public static IWebDriver CreateEdgeDriver(bool HeadlessMode)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadPath = Path.Combine(userPath, "Downloads");

            var options = new EdgeOptions();

            options.AddArguments("--window-size=1920,1080");
            options.AddUserProfilePreference("download.default_directory", downloadPath);

            if (HeadlessMode)
            {
                options.AddArguments("--headless");
            }

            return new EdgeDriver(options);

        }

        public static IWebDriver CreateChromeDriver(bool HeadlessMode)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadPath = Path.Combine(userPath, "Downloads");

            var options = new ChromeOptions();

            if (HeadlessMode)
            {
                options.AddArguments("--headless");
            }

            options.AddArguments("--window-size=1920,1080");
            options.AddUserProfilePreference("download.default_directory", downloadPath);

            return new ChromeDriver(options);

        }
    }
}
