using Business;
using Core.Logger;
using Core.WebDriver;
using TestLeyer;

namespace TestLayer
{
    public class Tests : BaseTestFixtures
    {
        
        private readonly AboutPage _aboutPage;
        private readonly CareersPage _careersPage;
        private readonly InsightsPage _insightsPage;
        private readonly HomePage _homePage;

        //public Tests()
        //{
        //    _homePage = new HomePage(WebDriverManager.CurrentDriver);

        //    _aboutPage = new AboutPage(WebDriverManager.CurrentDriver);

        //    _careersPage = new CareersPage(WebDriverManager.CurrentDriver);

        //    _insightsPage = new InsightsPage(WebDriverManager.CurrentDriver);
        //}

        [TestCase("C#", "All Locations")]
        public void Test1_Careers(string programmingLanguage, string location)
        {
            try
            {
                _homePage.ClickAcceptButton();
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.Info("Cookies are already accepted");
            }

            _homePage.ClickCareersLink();                

                _careersPage.EnterKeywords(programmingLanguage);                

                _careersPage.OpenLocationDropDownMenu();                

                _careersPage.SelectAllLocations();                

                _careersPage.SelectRemoteOption();                

                _careersPage.ClickFindButton();                

                _careersPage.ClickSortingLabelByDate();                

                _careersPage.GetLatestResul();                

                Assert.That(_careersPage.IsPresentOnThePage(programmingLanguage),
                    $"Programming language '{programmingLanguage}' not found on the page");
            }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Test2_GlobalSearch(string searchKeyword)
        {
                _homePage.ClickMagnifierIcon();                

                _homePage.SendSearchInputToGlobalSearch(searchKeyword);                

                _homePage.ClickFindButtonForGlobalSearch();                

                Assert.That(_homePage.IsSearchKeywordPresentOnThePage(searchKeyword), $"Search keyword '{searchKeyword}' was not found on the page");
            }

        [Test]
        public void Test3_ValidateFileDownload()
        {
                _homePage.ClickAboutLink();                

                _aboutPage.ClickDownloadButton();                

                Assert.That(_aboutPage.IsDownloaded("EPAM_Corporate_Overview_Q4_EOY.pdf"));
            }

        [Test]
        public void Test4_ValidateArticleTitleInCarousel()
        {
            try
            {
                _homePage.ClickAcceptButton();
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.Info("Cookies are already accepted");
            }

            _homePage.ClickInsightsLink();                

                _insightsPage.SwipeCarouselTwice();                

                _insightsPage.ClickReadMoreButton();                

                Assert.That(_insightsPage.IsActiveSliderTextPresentInTheArticleText());
            }
    }
}