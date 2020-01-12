using Applitools.Appium;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace ApplitoolsTutorial
{
    [TestFixture]
    public class BasicDemo
    {
        private RemoteWebDriver driver;
        private Eyes eyes;

        [Test]
        public void AndroidTest()
        {
            // Initialize the eyes SDK (IMPORTANT: make sure your API key is set in the APPLITOOLS_API_KEY env variable).
            eyes = new Eyes();

            // Set the desired capabilities.
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Samsung Galaxy S10");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.0");
            options.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            options.AddAdditionalCapability(MobileCapabilityType.BrowserName, "Chrome");
            options.AddAdditionalCapability("deviceOrientation", "portrait");

            // Initialize BrowserStack credentials. (IMPORTANT: make sure you have the below environment variables set).
            Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
            browserstackOptions.Add("userName",  Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
            browserstackOptions.Add("accessKey", Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

            options.AddAdditionalCapability("bstack:options", browserstackOptions);

            driver = new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            driver.Url = $"https://applitools.com/helloworld";

            // Start visual UI testing.
            eyes.Open(driver, "Hello, Applitools!", "My first Appium Web C# test!");

            // Visual UI testing.
            eyes.CheckWindow("Hello, World!");

            // End the test.
            eyes.Close();
        }

        [TearDown]
        public void AfterEach()
        {
            // Close the browser if driver isn't null.
            driver?.Quit();

            // If the test was aborted before eyes.close was called, ends the test as aborted.
            eyes.AbortIfNotClosed();
        }

    }
}
