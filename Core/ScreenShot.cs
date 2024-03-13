﻿using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.TestFixtures
{
    public static class ScreenShot
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static void CaptureScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                string directoryPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string fileName = $"screenshot_{testName}_{DateTime.Now:yyyyMMddHHmmss}.png";

                string filePath = Path.Combine(directoryPath, fileName);

                screenshot.SaveAsFile(filePath);

                logger.Info($"Screenshot captured: {filePath}");
            }
            catch (Exception ex)
            {
                logger.Error($"Failed to capture screenshot: {ex.Message}");
            }
        }
    }
}
