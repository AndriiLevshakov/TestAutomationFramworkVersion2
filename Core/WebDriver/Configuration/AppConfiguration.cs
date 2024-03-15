namespace Core.WebDriver.Configuration
{
    public class AppConfiguration
    {
        public string? BaseUrl { get; set; }
        public BrowserTypes BrowserType { get; set; }
        public bool HeadlessMode { get; set; }
    }
}
