using SeleniumNunit.Utility;

namespace SeleniumNunit
{
    public class Tests : BaseTest
    {
        [Test]
        public void Test()
        {
            Thread.Sleep(5000);
            Assert.Pass();
        }
    }
}