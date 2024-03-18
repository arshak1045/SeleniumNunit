using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNunit.PageObjects
{
    public class LoginPage
    {
        private static IWebDriver driver;

        public LoginPage(IWebDriver webDriver)
        {
            driver = webDriver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "username")]
        private readonly IWebElement userNameField;

        [FindsBy(How = How.Id, Using = "password")]
        private readonly IWebElement passwordField;

        [FindsBy(How = How.Id, Using = "terms")]
        private readonly IWebElement termsCheckbox;

        [FindsBy(How = How.Id, Using = "signInBtn")]
        private readonly IWebElement signInButton;

        public ProductPage Login(string userName, string password)
        {
            this.userNameField.SendKeys(userName);
            this.passwordField.SendKeys(password);
            termsCheckbox.Click();
            signInButton.Click();
            return new ProductPage(driver);
        }
    }
}
