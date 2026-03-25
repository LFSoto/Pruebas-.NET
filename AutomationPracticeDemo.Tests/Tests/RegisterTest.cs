using AutomationPracticeDemo.Tests.Models; // Para AccountInformation y AddressInformation
using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Tests
{
    [TestFixture]
    public class RegisterTest
    {
        private IWebDriver _driver;

            [SetUp]
            public void Setup()
            {
                var options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArgument("--disable-notifications");
                options.AddArgument("--disable-infobars");
                options.AddArgument("--disable-popup-blocking");

                _driver = new ChromeDriver(options);
                _driver.Manage().Cookies.DeleteAllCookies();
            }

            [Test]
            public void RegistroConJson()
            {
                var users = JsonHelper.LoadTestData("Data/InformationData.json");

                foreach (var user in users)
                {
                    // Paso 1: ir a login/signup
                    _driver.Navigate().GoToUrl("https://automationexercise.com");
                    _driver.FindElement(By.CssSelector("a[href='/login']")).Click();

                    // Completar formulario inicial de signup
                    _driver.FindElement(By.CssSelector("input[data-qa='signup-name']")).SendKeys(user.AccountInformation.Name);
                    _driver.FindElement(By.CssSelector("input[data-qa='signup-email']")).SendKeys(user.AccountInformation.Email);
                    _driver.FindElement(By.CssSelector("button[data-qa='signup-button']")).Click();

                    // Paso 2: ahora sí existe el formulario con radios id_gender1 / id_gender2
                    var registerPage = new RegisterPage(_driver);

                    registerPage.CloseAdsIfPresent();
                    registerPage.FillAccountInfo(user.AccountInformation);
                    registerPage.FillAddressInfo(user.AddressInformation);
                    registerPage.Submit();

                // Validación: que aparezca mensaje de cuenta creada
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                var confirmation = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h2[data-qa='account-created']")));
                Assert.That(confirmation.Text, Does.Contain("ACCOUNT CREATED!"));

            }
        }

            [TearDown]
            public void TearDown()
            {
                if (_driver != null)
                {
                    _driver.Quit();
                    _driver.Dispose();
                }
            }
        }
    }

    
