using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumNunit.PageObjects
{
    public class ProductPage
    {
        private static IWebDriver driver;
        private WebDriverWait WebDriverWait;
        By cardTitle = By.CssSelector(".card-title a");
        By addToCart = By.CssSelector(".card-footer button");

        public ProductPage(IWebDriver webDriver)
        {
            driver = webDriver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.TagName, Using = "app-card")]
        private readonly IList<IWebElement> ProductCards;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private readonly IWebElement CheckouButton;

        public void WaitUntilPageIsLoaded()
        {
            WebDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            WebDriverWait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }

        public By GetCardTitle()
        {
            return cardTitle;
        }

        public IList<IWebElement> GetCards()
        {
            return ProductCards;
        }

        public void Checkout()
        {
            CheckouButton.Click();
        }

        public By AddToCartButton()
        {
            return addToCart;
        }

        public void AddProduct(IList<IWebElement> products, List<string> expectedProduct)
        {
            foreach (var product in products)
            {
                if (expectedProduct.Contains(product.FindElement(GetCardTitle()).Text))
                {
                    product.FindElement(AddToCartButton()).Click();
                }
            }
            Checkout();
        }
    }
}
