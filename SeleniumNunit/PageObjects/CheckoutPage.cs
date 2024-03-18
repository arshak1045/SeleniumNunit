using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNunit.PageObjects
{
    public class CheckoutPage
    {
        private static IWebDriver driver;

        public CheckoutPage(IWebDriver webDriver)
        {
            driver = webDriver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        IList<IWebElement> ProductHeaders;

        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        IWebElement CheckoutButton;

        public IList<IWebElement> GetProductHeaders()
        {
            return ProductHeaders;
        }

        public void Checkout()
        {
            CheckoutButton.Click();
        }

        public List<string> GetProductTitle(IList<IWebElement> productCards)
        {
            List<string> titles = new List<string>();

            foreach (var item in productCards)
            {
                titles.Add(item.Text);
            }
            return titles;
        }
    }
}
