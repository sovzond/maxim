using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Drawing.Imaging;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestLogin
    {
        FirefoxDriver firefox;
        static String baseUrl = "http://91.143.44.249/sovzond_test/portal/login.aspx";
        static String screenShotFileName = "C:/temp/Screenshot.png";

        [TestInitialize]
        public void Setup()
        {
            firefox = new FirefoxDriver();
        }


        [TestMethod]
        public void TestGuestLogin()
        {
            Login("guest1", "guest");
            CheckIfExist("exit");
        }

        [TestMethod]
        public void TestIncorrectLogin()
        {
            Login("guest123", "guest");
            CheckIfExist("lbLoginError");
        }

        private void Login(String login, String passwd)
        {
            firefox.Navigate().GoToUrl(baseUrl);
            Thread.Sleep(2000);
            firefox.FindElement(By.Id("txtUser")).SendKeys(login);
            firefox.FindElement(By.Id("txtPsw")).SendKeys(passwd);
            firefox.FindElement(By.Id("txtPsw")).SendKeys(Keys.Enter);
        }

        private void CheckIfExist(String id)
        {
            WebDriverWait wait = new WebDriverWait(firefox, TimeSpan.FromSeconds(3));
            try
            {
                wait.Until(driver => driver.FindElement(By.Id(id)));
            }
            catch (WebDriverTimeoutException)
            {
                Screenshot image = ((ITakesScreenshot)firefox).GetScreenshot();
                image.SaveAsFile(screenShotFileName, ImageFormat.Png);
                Assert.Fail("Идентификатор не найден: " + id);
            }
        }


        [TestCleanup]
        public void TearDown()
        {
            firefox.Quit();
        }
    }
}
