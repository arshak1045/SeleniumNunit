using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;
using Logger = SeleniumNunit.Utility.Logger;

namespace SeleniumNunit.Utility
{
    public class BaseTest
    {
        private static IWebDriver _webDriver;
        private readonly string _browser = ConfigurationManager.AppSettings["browser"];
        private readonly string _projectUrl = ConfigurationManager.AppSettings["url"];

        [OneTimeSetUp]
        public void SetupBrowser()
        {
            InitializeBrowser(_browser);
            Logger.LogInfo($"[{_browser}] browser is launched.");
            _webDriver.Manage().Window.Maximize();
            Logger.LogInfo($"{_browser} window is maximized.");
            _webDriver.Url = _projectUrl;
            Logger.LogInfo($"Navigated to the {_projectUrl}.");
        }

        [OneTimeTearDown]
        public void CloseBrowser()
        {
            _webDriver.Dispose();
            Logger.LogInfo($"The {_browser} browser is disposed");
        }

        private void InitializeBrowser(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    _webDriver = new ChromeDriver();
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    _webDriver = new FirefoxDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    _webDriver = new EdgeDriver();
                    break;
                default:
                    throw new ArgumentException("Unsupported browser: " + browser);
            }
        }

        public static IWebDriver GetDriver()
        {
            return _webDriver;
        }
    }
}
