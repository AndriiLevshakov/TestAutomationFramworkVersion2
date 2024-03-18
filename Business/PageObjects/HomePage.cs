using Core.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static Core.Logger.LoggerManager;

namespace Business
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By _acceptButtonLocator = By.Id("onetrust-accept-btn-handler");
        private readonly By _careersLink = By.XPath("//span[@class='top-navigation__item-text']/a[contains(@href, 'careers')]");
        private readonly By _magnifierIcon = By.XPath("//span[contains(@class, 'search-icon')]");
        private readonly By _searchInput = By.XPath("//input[@type='search']");
        private readonly By _findButtonForGlobalSearch = By.XPath("//span[contains(text(), 'Find')]");
        private readonly By _searchResult = By.XPath("//section[contains(@data-config-path, 'content-container')]");
        private readonly By _aboutLink = By.XPath("//a[contains(@class, 'top-navigation__item')][contains(text(), 'About')]");
        private readonly By _insightsLink = By.XPath("//a[contains(@class, 'top-navigation__item')][@href='/insights']");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void ClickAcceptButton()
        {
            try
            { 
            wait.Until(ExpectedConditions.ElementToBeClickable(_acceptButtonLocator)).Click();

            Logger.Info("Clicked Accept Button");
            }
            catch (Exception ex)
            {
                Logger.Info("Cookies are already accepted");
            }
        }

        public void ClickCareersLink()
        {
            driver.FindElement(_careersLink).Click();

            Logger.Info("Clicked Careers Link");
        }

        public void ClickMagnifierIcon()
        {
            driver.FindElement(_magnifierIcon).Click();

            Logger.Info("Clicked magnifier icon");
        }

        public void SendSearchInputToGlobalSearch(string searchWords)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(_searchInput)).SendKeys(searchWords);

            Logger.Info("Entered the search keyword into the global search input field");
        }

        public void ClickFindButtonForGlobalSearch()
        {
            driver.FindElement(_findButtonForGlobalSearch).Click();

            Logger.Info("Clicked 'Find' button for global search");
        }

        public bool IsSearchKeywordPresentOnThePage(string word)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            var searchResults = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(_searchResult));

            return searchResults.Any(result => result.Text.ToLower().Contains(word.ToLower()));
        }

        public void ClickAboutLink()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(_aboutLink)).Click();

            Logger.Info("Clicked 'About' link");
        }

        public void ClickInsightsLink()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(_insightsLink)).Click();

            Logger.Info("Clicked 'Insights' link");
        }
    }
}
