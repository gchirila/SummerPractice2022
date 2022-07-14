using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;

namespace SummerPractice2022
{
    [TestClass]
    public class RegisterTests
    {
        IWebDriver driver;
        string baseUrl = "http://imdb.com";

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseUrl);
            driver.FindElement(By.XPath("//div[text()='Sign In']")).Click();
            driver.FindElement(By.CssSelector(".create-account")).Click();
        }

        [TestMethod]
        public void SuccessfulyRegister()
        {
            var stringGenerator = TestUtils.RandomString(10);
            driver.FindElement(By.Id("ap_customer_name")).SendKeys("Popa Ionut");
            driver.FindElement(By.Id("ap_email")).SendKeys(stringGenerator + "@gmail.com");
            driver.FindElement(By.Id("ap_password")).SendKeys(stringGenerator);
            driver.FindElement(By.Id("ap_password_check")).SendKeys(stringGenerator);
            driver.FindElement(By.Id("continue")).Click();
            var puzzleMessage = driver.FindElement(By.XPath("//span[@class='a-size-large']")).Text;
            Assert.AreEqual("Solve this puzzle to protect your account", puzzleMessage);
        }

        [TestMethod]
        public void FailedRegister_WithInvalidEmail()
        {
            var stringGenerator = TestUtils.RandomString(10);
            driver.FindElement(By.Id("ap_customer_name")).SendKeys("Popa Ionut");
            driver.FindElement(By.Id("ap_email")).SendKeys(stringGenerator);
            driver.FindElement(By.Id("ap_password")).SendKeys(stringGenerator);
            driver.FindElement(By.Id("ap_password_check")).SendKeys(stringGenerator);
            driver.FindElement(By.Id("continue")).Click();
            var errorMessage = driver.FindElement(By.XPath("//span[@class='a-list-item']")).Text;
            Assert.AreEqual("Enter a valid email address", errorMessage);
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
