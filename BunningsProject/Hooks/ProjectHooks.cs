using BunningsProject.Bunnings.Pages;
using BunningsProject.ProjectUtilityClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Configuration;
using TechTalk.SpecFlow;

namespace BunningsProject.Hooks
{

    [Binding]
    public class ProjectHooks
    {
        private readonly ScenarioContext scenarioContext;
        public ProjectHooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        static IWebDriver driver;
        string TestCaseResult = "";
        public static void InitiateDriver()
        {
            string Env = ConfigurationManager.AppSettings["Env"];
            string browser = ConfigurationManager.AppSettings["Browser"];
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    driver = new FirefoxDriver();
                    break;
                case "IE":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }

        }


        [BeforeScenario]
        public static void DriverWakeUp()
        {
            InitiateDriver();
            string BaseUrl = ConfigurationManager.AppSettings["baseurl"];
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(BaseUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Cookies.DeleteAllCookies();
            AllPageObjects.LoadDesktop(driver);
        }

        [AfterScenario]
        public void CleanupScenario()
        {
            var Screenshot = true;

            try
            {
                Screenshot = (bool)scenarioContext["Screenshot"];
            }
            catch { }

            if (Screenshot == false)
            {
                // Do nothing, do not take screen shot for this feature, as browser window is alreday closed
            }
            else
            {
                if (scenarioContext.TestError == null)
                {
                    TestCaseResult = "TestCasePass";
                    new ScreenShotUtil(driver).TakeScreenshot(TestCaseResult);

                }

                else
                {
                    TestCaseResult = "TestCaseFail";
                    new ScreenShotUtil(driver).TakeScreenshot(TestCaseResult);
                }

            }

            QuitDriver();
        }

        private void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
        public void TakeScreenshot() // take interim screenshots
        {
            TestCaseResult = "TestCaseRunning";
            new ScreenShotUtil(driver).TakeScreenshot(TestCaseResult);
        }
    }
}