using Core.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Business
{
    public class InsightsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By _buttonToSwipeTheCarousel = By.XPath("//div[contains(@class, 'active')]//div[contains(@class, 'content-container')]");
        private readonly By _activeSlideText = By.XPath("//div[@class='owl-item active']//span[@class='museo-sans-light']");
        private readonly By _activeSlideReadMoreButton = By.XPath("//div[@class='owl-item active']//a[@tabindex='0']");
        private readonly By _articleText = By.XPath("//span[@class='font-size-80-33']/span[@class='museo-sans-light']");

        private string? CarouselArticleTitle;
        private string? ArticleHeader;

        public InsightsPage(IWebDriver driver)
        {
            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void SwipeCarouselTwice()
        {
            var actions = new Actions(driver);

            for (int i = 0; i < 2; i++)
            {
                actions.ClickAndHold(wait.Until(ExpectedConditions.ElementToBeClickable(_buttonToSwipeTheCarousel)))
                    .DragAndDropToOffset(wait.Until(ExpectedConditions.ElementToBeClickable(_buttonToSwipeTheCarousel)), -100, 0)
                    .Release()
                    .Pause(TimeSpan.FromSeconds(2))
                    .Perform();
            }

            LoggerManager.Logger.Info("Swiped carousel twice");

            CarouselArticleTitle = driver.FindElement(_activeSlideText).Text;
        }

        public void ClickReadMoreButton()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(_activeSlideReadMoreButton)).Click();

            LoggerManager.Logger.Info("Clicked 'Read More' button");
        }

        public bool IsActiveSliderTextPresentInTheArticleText()
        {
            ArticleHeader = wait.Until(ExpectedConditions.ElementIsVisible(_articleText)).Text;

            return ArticleHeader.Contains(CarouselArticleTitle);
        }
    }
}
