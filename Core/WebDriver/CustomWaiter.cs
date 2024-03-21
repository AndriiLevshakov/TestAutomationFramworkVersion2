using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static Core.WebDriver.WebDriverManager;

namespace Core.WebDriver
{
    public static class CustomWaiter
    {
        public static WebDriverWait? _wait;
    }
}
