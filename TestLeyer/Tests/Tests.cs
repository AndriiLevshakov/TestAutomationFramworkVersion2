using Business;
using Core;
using Core.Logger;

namespace TestLayer
{
    public class Tests : BaseTestFixtures
    {
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

                var careersPage = new CareersPage(WebDriverManager.Driver(HeadlessMode));

                careersPage.EnterKeywords(programmingLanguage);
                LoggerManager.Logger.Info($"Entered keywords: {programmingLanguage}");

                careersPage.OpenLocationDropDownMenu();
                LoggerManager.Logger.Info("Opened location drop down menu");

                careersPage.SelectAllLocations();
                LoggerManager.Logger.Info("Selected all locations");

                careersPage.SelectRemoteOption();
                LoggerManager.Logger.Info("Selected remote option");

                careersPage.ClickFindButton();
                LoggerManager.Logger.Info("Clicked 'Find' button");

                careersPage.ClickSortingLabelByDate();
                LoggerManager.Logger.Info("Clicked sorting label by date");

                careersPage.GetLatestResul();
                LoggerManager.Logger.Info("Got latest result");

                Assert.That(careersPage.IsPresentOnThePage(programmingLanguage),
                    $"Programming language '{programmingLanguage}' not found on the page");
                LoggerManager.Logger.Info("Test successfully completed.");
            }
            catch (Exception ex)
            {
                ScreenShot.CaptureScreenshot(WebDriverManager.Driver(HeadlessMode), nameof(Test1_Careers));

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

                var aboutPage = new AboutPage(WebDriverManager.Driver(HeadlessMode));

                _homePage.ClickAcceptButton();
                LoggerManager.Logger.Info("Clicked 'Accept' button");

                _homePage.ClickAboutLink();
                LoggerManager.Logger.Info("Clicked 'About' link");

                aboutPage.ClickDownloadButton();
                LoggerManager.Logger.Info("Clicked 'Download' button");

                Assert.That(aboutPage.IsDownloaded("EPAM_Corporate_Overview_Q4_EOY.pdf"));
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

                var insightsPage = new InsightsPage(WebDriverManager.Driver(HeadlessMode));

                _homePage.ClickAcceptButton();
                LoggerManager.Logger.Info("Clicked 'Accept' button");

                _homePage.ClickInsightsLink();
                LoggerManager.Logger.Info("Clicked 'Insights' link");

                insightsPage.SwipeCarouselTwice();
                LoggerManager.Logger.Info("Swiped carousel twice");

                insightsPage.ClickReadMoreButton();
                LoggerManager.Logger.Info("Clicked 'Read More' button");

                Assert.That(insightsPage.IsActiveSliderTextPresentInTheArticleText());
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