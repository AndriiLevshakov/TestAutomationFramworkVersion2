using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
            wait.Until(ExpectedConditions.ElementToBeClickable(_acceptButtonLocator)).Click();
        }

        public void ClickCareersLink()
        {
            driver.FindElement(_careersLink).Click();
        }

        public void ClickMagnifierIcon()
        {
            driver.FindElement(_magnifierIcon).Click();
        }

        public void SendSearchInputToGlobalSearch(string searchWords)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(_searchInput)).SendKeys(searchWords);
        }

        public void ClickFindButtonForGlobalSearch()
        {
            driver.FindElement(_findButtonForGlobalSearch).Click();
        }

        public bool IsSearchKeywordPresentOnThePage(string word)
        {
            var searchResults = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(_searchResult));

            return searchResults.All(result => result.Text.ToLower().Contains(word.ToLower()));
        }

        public void ClickAboutLink()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(_aboutLink)).Click();
        }

        public void ClickInsightsLink()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(_insightsLink)).Click();
        }
    }
}
