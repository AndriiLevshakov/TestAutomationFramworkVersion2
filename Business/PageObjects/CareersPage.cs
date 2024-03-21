using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using static Core.Logger.LoggerManager;
using static Core.WebDriver.CustomWaiter;
using static Core.WebDriver.WebDriverManager;

namespace Business
{
    public class CareersPage
    {
        private readonly By _keywordsField = By.XPath("//input[@placeholder='Keyword']");
        private readonly By _allLocationSelector = By.XPath("//li[contains(@id, 'all_locations')]");
        private readonly By _remoteOption = By.XPath("//label[contains(text(), 'Remote')]");
        private readonly By _findButtonForTest1 = By.XPath("//button[@type='submit']");
        private readonly By _sortingLabelDate = By.XPath("//label[contains(text(), 'Date')]");
        private readonly By _latestResult = By.XPath("//div[@class='search-result__item-name-section']//a[contains(@class, 'search-result')]");
        private readonly By _locationField = By.XPath("//span[@class='select2-selection__arrow']");

        public void EnterKeywords(string keywords)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_keywordsField)).SendKeys(keywords);

            Logger.Info($"Entered keywords: {keywords}");
        }

        public void OpenLocationDropDownMenu()
        {
            CurrentDriver.FindElement(_locationField).Click();

            Logger.Info("Opened location drop down menu");
        }

        public void SelectAllLocations()
        {
            CurrentDriver.FindElement(_allLocationSelector).Click();

            Logger.Info("Selected all locations");
        }

        public void SelectRemoteOption()
        {
            CurrentDriver.FindElement(_remoteOption).Click();

            Logger.Info("Selected remote option");
        }

        public void ClickFindButton()
        {
            CurrentDriver.FindElement(_findButtonForTest1).Click();

            Logger.Info("Clicked 'Find' button");
        }

        public void ClickSortingLabelByDate()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_sortingLabelDate)).Click();

            Logger.Info("Clicked sorting label by date");
        }

        public void ClickLatestResultLink()
        {
            var elements = CurrentDriver.FindElements(_latestResult);

            if (elements.Any())
            {
                elements.First().Click();
            }

            _wait.Until(CurrentDriver => ((IJavaScriptExecutor)CurrentDriver).ExecuteScript("return document.readyState").Equals("complete"));

            Logger.Info("Got latest result");
        }

        public bool IsPresentOnThePage(string programmingLanguage)
        {
            Logger.Info("Test successfully finished");

            return CurrentDriver.PageSource.Contains(programmingLanguage);
        }
    }
}
