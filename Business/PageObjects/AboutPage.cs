using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static Core.Logger.LoggerManager;
using static Core.WebDriver.WebDriverManager;
using static Core.WebDriver.CustomWaiter;

namespace Business
{
    public class AboutPage
    {
        private readonly By _downloadButton = By.XPath("//a[contains(@href, 'EPAM_Corporate_Overview')]");
        private readonly By _sectionWhichHelpToSeeDownloadButton = By.XPath("//span[contains(text(), 'MEET')]");

        public AboutPage()
        {
            _wait = new WebDriverWait(CurrentDriver, TimeSpan.FromSeconds(10));
        }

        public void ClickDownloadButton()
        {
            var actions = new Actions(CurrentDriver);

            actions.MoveToElement(CurrentDriver.FindElement(_sectionWhichHelpToSeeDownloadButton)).Perform();

            _wait.Until(ExpectedConditions.ElementToBeClickable(_downloadButton)).Click();

            Logger.Info("Clicked 'Download' button");
        }

        public bool IsDownloaded(string fileName)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string downloadPath = Path.Combine(userPath, "Downloads");

            DirectoryInfo dirInfo = new DirectoryInfo(downloadPath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string filePath = Path.Combine(downloadPath, fileName);

            Logger.Info("Test successfully finished");

            return File.Exists(filePath);
        }
    }
}
