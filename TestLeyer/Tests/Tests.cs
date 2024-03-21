using Business;
using Business.PageObjects;
using TestLeyer;
using static Core.WebDriver.WebDriverManager;

namespace TestLayer
{
    public class Tests : BaseTestFixtures
    {
        private readonly AboutPage _aboutPage;
        private readonly CareersPage _careersPage;
        private readonly InsightsPage _insightsPage;
        private readonly HomePage _homePage;
        private readonly Navigation _navigation;

        public Tests()
        {
            _homePage = new HomePage();

            _aboutPage = new AboutPage();

            _careersPage = new CareersPage();

            _insightsPage = new InsightsPage();

            _navigation = new Navigation();
        }

        [TestCase("C#", "All Locations")]
        public void Test1_Careers(string programmingLanguage, string location)
        {
            _navigation.NavigateToStartPage();

            _homePage.ClickAcceptButton();

            _homePage.ClickCareersLink();

            _careersPage.EnterKeywords(programmingLanguage);

            _careersPage.OpenLocationDropDownMenu();

            _careersPage.SelectAllLocations();

            _careersPage.SelectRemoteOption();

            _careersPage.ClickFindButton();

            _careersPage.ClickSortingLabelByDate();

            _careersPage.ClickLatestResultLink();

            var isLaguegePresent = _careersPage.IsPresentOnThePage(programmingLanguage);

            Assert.That(isLaguegePresent);
        }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Test2_GlobalSearch(string searchKeyword)
        {
            _navigation.NavigateToStartPage();

            _homePage.ClickMagnifierIcon();

            _homePage.SendSearchInputToGlobalSearch(searchKeyword);

            _homePage.ClickFindButtonForGlobalSearch();

            var isSearchKeywordPresent = _homePage.IsSearchKeywordPresentOnThePage(searchKeyword);

            Assert.That(isSearchKeywordPresent);
        }

        [Test]
        public void Test3_ValidateFileDownload()
        {
            _navigation.NavigateToStartPage();

            _homePage.ClickAboutLink();

            _aboutPage.ClickDownloadButton();

            var isDownloaded = _aboutPage.IsDownloaded("EPAM_Corporate_Overview_Q4_EOY.pdf");

            Assert.That(isDownloaded);
        }

        [Test]
        public void Test4_ValidateArticleTitleInCarousel()
        {
            _navigation.NavigateToStartPage();

            _homePage.ClickAcceptButton();

            _homePage.ClickInsightsLink();

            _insightsPage.SwipeCarouselTwice();

            _insightsPage.ClickReadMoreButton();

            var isTextPresent = _insightsPage.IsActiveSliderTextPresentInTheArticleText();

            Assert.That(isTextPresent);
        }
    }
}