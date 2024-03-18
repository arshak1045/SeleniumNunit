using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNunit.PageObjects
{
    public class DeliveryPage
    {
        private static IWebDriver driver;
        private WebDriverWait WebDriverWait;

        public DeliveryPage(IWebDriver webDriver)
        {
            driver = webDriver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement CountryFiled;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit']")]
        private IWebElement PurchaseButton;

        [FindsBy(How = How.CssSelector, Using = ".alert[class]")]
        private IWebElement AlertMessage;

        public void EnterCountry(string country)
        {
            CountryFiled.SendKeys(country);
        }

        public void SelectInDropdown(string option)
        {
            WaitUntilSugestionsToBeLoaded();
            var options = driver.FindElements(By.CssSelector(".suggestions ul li a"));
            foreach (var item in options)
            {
                if (item.Text == option)
                {
                    item.Click();
                    break;
                }
            }
        }

        public void ClickPurchaseButton()
        {
            PurchaseButton.Click();
        }

        public string GetAllertMessage()
        {
            return AlertMessage.Text;
        }

        public void WaitUntilSugestionsToBeLoaded()
        {
            WebDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            WebDriverWait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(".suggestions")));
        }
    }
}
