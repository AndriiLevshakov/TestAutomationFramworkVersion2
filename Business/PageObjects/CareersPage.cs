using Core.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static Core.Logger.LoggerManager;

namespace Business
{
    public class CareersPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By _keywordsField = By.XPath("//input[@placeholder='Keyword']");
        private readonly By _allLocationSelector = By.XPath("//li[contains(@id, 'all_locations')]");
        private readonly By _remoteOption = By.XPath("//label[contains(text(), 'Remote')]");
        private readonly By _findButtonForTest1 = By.XPath("//button[@type='submit']");
        private readonly By _sortingLabelDate = By.XPath("//label[contains(text(), 'Date')]");
        private readonly By _latestResult = By.XPath("//div[@class='search-result__item-name-section']//a[contains(@class, 'search-result')]");
        private readonly By _locationField = By.XPath("//span[@class='select2-selection__arrow']");

        public CareersPage(IWebDriver driver)
        {
            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void EnterKeywords(string keywords)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(_keywordsField)).SendKeys(keywords);

            Logger.Info($"Entered keywords: {keywords}");
        }

        public void OpenLocationDropDownMenu()
        {
            driver.FindElement(_locationField).Click();

            Logger.Info("Opened location drop down menu");
        }

        public void SelectAllLocations()
        {
            driver.FindElement(_allLocationSelector).Click();

            Logger.Info("Selected all locations");
        }

        public void SelectRemoteOption()
        {
            driver.FindElement(_remoteOption).Click();

            Logger.Info("Selected remote option");
        }

        public void ClickFindButton()
        {
            driver.FindElement(_findButtonForTest1).Click();

            Logger.Info("Clicked 'Find' button");
        }

        public void ClickSortingLabelByDate()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(_sortingLabelDate)).Click();

            Logger.Info("Clicked sorting label by date");
        }

        public void ClickLatestResultLink()
        {
            var elements = driver.FindElements(_latestResult);

            if (elements.Any())
            {
                elements.First().Click();
            }

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            Logger.Info("Got latest result");
        }

        public bool IsPresentOnThePage(string programmingLanguage)
        {
            Logger.Info("Test successfully finished");

            return driver.PageSource.Contains(programmingLanguage);            
        }
    }
}
