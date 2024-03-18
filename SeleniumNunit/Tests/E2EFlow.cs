using OpenQA.Selenium;
using SeleniumNunit.PageObjects;
using SeleniumNunit.Utility;

namespace SeleniumNunit.Tests
{
    [TestFixture]
    public class E2EFlow : BaseTest
    {
        private const string UserName = "rahulshettyacademy";
        private const string Password = "learning";
        private static readonly LoginPage loginPage = new LoginPage(GetDriver());
        private static readonly DeliveryPage deliveryPage = new DeliveryPage(GetDriver());
        private static readonly CheckoutPage checkoutPage = new CheckoutPage(GetDriver());
        private const string Country = "Ind";
        private const string DropdownCountryValue = "India";
        private const string ExpectedMessage = " Thank you! Your order will be delivered in next few weeks :-).";
        private List<string> ExpectedProducts = new() { "iphone X", "Blackberry" };
        private List<string> ActualProducts = new List<string>();


        [Test, Order(1)]
        public void VerifyTheProductAreAddedToCart()
        {
            var _productPage = loginPage.Login(UserName, Password);
            _productPage.WaitUntilPageIsLoaded();
            IList<IWebElement> products = _productPage.GetCards();
            _productPage.AddProduct(products, ExpectedProducts);
            IList<IWebElement> elements = checkoutPage.GetProductHeaders();
            ActualProducts = checkoutPage.GetProductTitle(elements);
            Assert.That(ActualProducts, Is.EqualTo(ExpectedProducts));
        }

        [Test, Order(2)]
        public void VerifyTheOrderCompletedSuccessfully()
        {
            checkoutPage.Checkout();
            deliveryPage.EnterCountry(Country);
            deliveryPage.SelectInDropdown(DropdownCountryValue);
            deliveryPage.ClickPurchaseButton();
            Assert.IsTrue(deliveryPage.GetAllertMessage().Contains(ExpectedMessage));
        }
    }
}