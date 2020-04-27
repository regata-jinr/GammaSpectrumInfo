using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using Xunit;

namespace GSI.UI.Test
{
    public class GSISession
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string AppId = @"D:\GoogleDrive\Job\flnp\dev\regata\GammaSpectrumInfo\UI\bin\Debug\netcoreapp3.1\GammaSpectrumInfo.exe";

        public WindowsDriver<WindowsElement> Session;

        public GSISession()
        {
            TearDown();
            if (Session == null)
            {

                var appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", AppId);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");
                Session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                Session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);
            }
        }

        public void TearDown()
        {
            // Close the application and delete the session
            if (Session != null)
            {
                Session.Quit();
                Session = null;
            }
        }
    }

    public class GSIUITest : IClassFixture<GSISession>
    {

        protected static WindowsDriver<WindowsElement> CurrentSession;

        public GSIUITest(GSISession sess)
        {
            CurrentSession = sess.Session;
        }

        [Fact]
        public void TestInit()
        {
            Assert.NotNull(CurrentSession);
            Assert.NotNull(CurrentSession.SessionId);

        }

        [Fact]
        public void TestLang()
        {
            if (Labels.CurrentLanguage != Languages.English)
            {
                CurrentSession.FindElementByName("Menu").Click();
                CurrentSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Language\")]").Click();
                CurrentSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"English\")]").Click();

            }

            Assert.Equal(Languages.English, Labels.CurrentLanguage);

            CurrentSession.FindElementByName("Menu").Click();
            CurrentSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Language\")]").Click();
            CurrentSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Russian\")]").Click();

            Assert.Equal(Languages.Russian, Labels.CurrentLanguage);
            
            CurrentSession.FindElementByName("Menu").Click();
            CurrentSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Language\")]").Click();
            CurrentSession.FindElementByXPath($"//MenuItem[starts-with(@Name, \"English\")]").Click();

            Assert.Equal(Languages.English, Labels.CurrentLanguage);
           
        }
    }
}
