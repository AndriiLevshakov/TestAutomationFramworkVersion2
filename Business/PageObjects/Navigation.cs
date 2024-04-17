﻿using static Core.WebDriver.WebDriverManager;

namespace Business.PageObjects
{
    public class Navigation
    {
        public void NavigateToStartPage()
        {
            CurrentDriver.Navigate().GoToUrl(BaseUrl);
        }
    }
}
