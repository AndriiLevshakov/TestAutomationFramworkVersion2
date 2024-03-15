using Business;
using Core.Logger;
using Core.WebDriver;
using TestLeyer;

namespace TestLayer
{
    public class Tests : BaseTestFixtures
    {
        private readonly HomePage _homePage;
        private readonly AboutPage _aboutPage;
        private readonly CareersPage _careersPage;
        private readonly InsightsPage _insightsPage;

        public Tests()
        {
            _homePage = new HomePage(WebDriverManager.CurrentDriver);

            _aboutPage = new AboutPage(WebDriverManager.CurrentDriver);

            _careersPage = new CareersPage(WebDriverManager.CurrentDriver);

            _insightsPage = new InsightsPage(WebDriverManager.CurrentDriver);
        }

        [TestCase("C#", "All Locations")]
        public void Test1_Careers(string programmingLanguage, string location)
        {
            try
            {
                LoggerManager.Logger.Info($"Starting Test1_Careers with programming language: {programmingLanguage}, location: {location}");

                _homePage.ClickAcceptButton();
                LoggerManager.Logger.Info("Clicked Accept Button");

                _homePage.ClickCareersLink();
                LoggerManager.Logger.Info("Clicked Careers Link");

                _careersPage.EnterKeywords(programmingLanguage);
                LoggerManager.Logger.Info($"Entered keywords: {programmingLanguage}");

                _careersPage.OpenLocationDropDownMenu();
                LoggerManager.Logger.Info("Opened location drop down menu");

                _careersPage.SelectAllLocations();
                LoggerManager.Logger.Info("Selected all locations");

                _careersPage.SelectRemoteOption();
                LoggerManager.Logger.Info("Selected remote option");

                _careersPage.ClickFindButton();
                LoggerManager.Logger.Info("Clicked 'Find' button");

                _careersPage.ClickSortingLabelByDate();
                LoggerManager.Logger.Info("Clicked sorting label by date");

                _careersPage.GetLatestResul();
                LoggerManager.Logger.Info("Got latest result");

                Assert.That(_careersPage.IsPresentOnThePage(programmingLanguage),
                    $"Programming language '{programmingLanguage}' not found on the page");
                LoggerManager.Logger.Info("Test successfully completed.");
            }
            catch (Exception ex)
            {
                ScreenShot.CaptureScreenshot(WebDriverManager.CurrentDriver, nameof(Test1_Careers));

                LoggerManager.Logger.Error($"An error occurred in Test1_Careers: {ex.Message}");
                throw;
            }
        }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Test2_GlobalSearch(string searchKeyword)
        {
            try
            {
                LoggerManager.Logger.Info($"Starting Test2_GlobalSearch with search keyword: {searchKeyword}");

                _homePage.ClickMagnifierIcon();
                LoggerManager.Logger.Info("Clicked magnifier icon");

                _homePage.SendSearchInputToGlobalSearch(searchKeyword);
                LoggerManager.Logger.Info("Entered the search keyword into the global search input field");

                _homePage.ClickFindButtonForGlobalSearch();
                LoggerManager.Logger.Info("Clicked 'Find' button for global search");

                Assert.That(_homePage.IsSearchKeywordPresentOnThePage(searchKeyword), $"Search keyword '{searchKeyword}' was not found on the page");
                LoggerManager.Logger.Info("Test successfully completed.");
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.Error($"An error occurred in Test2_GlobalSearch: {ex.Message}");
                throw;
            }
        }

        [Test]
        public void Test3_ValidateFileDownload()
        {
            try
            {
                LoggerManager.Logger.Info("Starting the Test3_ValidatiFileDownload");

                _homePage.ClickAcceptButton();
                LoggerManager.Logger.Info("Clicked 'Accept' button");

                _homePage.ClickAboutLink();
                LoggerManager.Logger.Info("Clicked 'About' link");

                _aboutPage.ClickDownloadButton();
                LoggerManager.Logger.Info("Clicked 'Download' button");

                Assert.That(_aboutPage.IsDownloaded("EPAM_Corporate_Overview_Q4_EOY.pdf"));
                LoggerManager.Logger.Info("Test successfully completed.");
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.Error($"An error occurred in Test3_ValidateFileDownload: {ex.Message}");
                throw;
            }
        }

        [Test]
        public void Test4_ValidateArticleTitleInCarousel()
        {
            try
            {
                LoggerManager.Logger.Info("Starting the Test4_ValidateArticleTitleInCarousel");

                _homePage.ClickAcceptButton();
                LoggerManager.Logger.Info("Clicked 'Accept' button");

                _homePage.ClickInsightsLink();
                LoggerManager.Logger.Info("Clicked 'Insights' link");

                _insightsPage.SwipeCarouselTwice();
                LoggerManager.Logger.Info("Swiped carousel twice");

                _insightsPage.ClickReadMoreButton();
                LoggerManager.Logger.Info("Clicked 'Read More' button");

                Assert.That(_insightsPage.IsActiveSliderTextPresentInTheArticleText());
                LoggerManager.Logger.Info("Test successfully completed.");
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.Error($"An error occurred in Test4_ValidateArticleTitleInCarousel: {ex.Message}");
                throw;
            }
        }
    }
}