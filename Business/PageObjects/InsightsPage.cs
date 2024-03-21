using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static Core.Logger.LoggerManager;
using static Core.WebDriver.WebDriverManager;
using static Core.WebDriver.CustomWaiter;

namespace Business
{
    public class InsightsPage
    {
        private readonly By _buttonToSwipeTheCarousel = By.XPath("//div[contains(@class, 'active')]//div[contains(@class, 'content-container')]");
        private readonly By _activeSlideText = By.XPath("//div[@class='owl-item active']//span[@class='museo-sans-light']");
        private readonly By _activeSlideReadMoreButton = By.XPath("//div[@class='owl-item active']//a[@tabindex='0']");
        private readonly By _articleText = By.XPath("//span[@class='font-size-80-33']/span[@class='museo-sans-light']");

        private string? CarouselArticleTitle;
        private string? ArticleHeader;

        public void SwipeCarouselTwice()
        {
            var actions = new Actions(CurrentDriver);

            for (int i = 0; i < 2; i++)
            {
                actions.ClickAndHold(_wait.Until(ExpectedConditions.ElementToBeClickable(_buttonToSwipeTheCarousel)))
                    .DragAndDropToOffset(_wait.Until(ExpectedConditions.ElementToBeClickable(_buttonToSwipeTheCarousel)), -100, 0)
                    .Release()
                    .Pause(TimeSpan.FromSeconds(2))
                    .Perform();
            }

            Logger.Info("Swiped carousel twice");

            CarouselArticleTitle = CurrentDriver.FindElement(_activeSlideText).Text;
        }

        public void ClickReadMoreButton()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(_activeSlideReadMoreButton)).Click();

            Logger.Info("Clicked 'Read More' button");
        }

        public bool IsActiveSliderTextPresentInTheArticleText()
        {
            ArticleHeader = _wait.Until(ExpectedConditions.ElementIsVisible(_articleText)).Text;

            Logger.Info("Test successfully finished");

            return ArticleHeader.Contains(CarouselArticleTitle);
        }
    }
}
