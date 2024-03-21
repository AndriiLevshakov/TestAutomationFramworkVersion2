using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static Core.Logger.LoggerManager;
using static Core.WebDriver.WebDriverManager;
using static Core.WebDriver.CustomWaiter;

namespace Business
{
    public class HomePage
    {
        private readonly By _acceptButtonLocator = By.Id("onetrust-accept-btn-handler");
        private readonly By _careersLink = By.XPath("//span[@class='top-navigation__item-text']/a[contains(@href, 'careers')]");
        private readonly By _magnifierIcon = By.XPath("//span[contains(@class, 'search-icon')]");
        private readonly By _searchInput = By.XPath("//input[@type='search']");
        private readonly By _findButtonForGlobalSearch = By.XPath("//span[contains(text(), 'Find')]");
        private readonly By _searchResult = By.XPath("//section[contains(@data-config-path, 'content-container')]");
        private readonly By _aboutLink = By.XPath("//a[contains(@class, 'top-navigation__item')][contains(text(), 'About')]");
        private readonly By _insightsLink = By.XPath("//a[contains(@class, 'top-navigation__item')][@href='/insights']");

        public void ClickAcceptButton()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementToBeClickable(_acceptButtonLocator)).Click();

                Logger.Info("Clicked Accept Button");
            }
            catch (Exception ex)
            {
                Logger.Info("Cookies are already accepted");
            }
        }

        public void ClickCareersLink()
        {
            CurrentDriver.FindElement(_careersLink).Click();

            Logger.Info("Clicked Careers Link");
        }

        public void ClickMagnifierIcon()
        {
            CurrentDriver.FindElement(_magnifierIcon).Click();

            Logger.Info("Clicked magnifier icon");
        }

        public void SendSearchInputToGlobalSearch(string searchWords)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_searchInput)).SendKeys(searchWords);

            Logger.Info("Entered the search keyword into the global search input field");
        }

        public void ClickFindButtonForGlobalSearch()
        {
            CurrentDriver.FindElement(_findButtonForGlobalSearch).Click();

            Logger.Info("Clicked 'Find' button for global search");
        }

        public bool IsSearchKeywordPresentOnThePage(string word)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)CurrentDriver;

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            var searchResults = _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(_searchResult));

            Logger.Info("Test successfully finished");

            return searchResults.Any(result => result.Text.ToLower().Contains(word.ToLower()));
        }

        public void ClickAboutLink()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_aboutLink)).Click();

            Logger.Info("Clicked 'About' link");
        }

        public void ClickInsightsLink()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_insightsLink)).Click();

            Logger.Info("Clicked 'Insights' link");
        }
    }
}
