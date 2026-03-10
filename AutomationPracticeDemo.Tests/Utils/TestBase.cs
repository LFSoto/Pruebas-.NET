using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationPracticeDemo.Tests.Utils
{
    public class TestBase
    {
        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-popup-blocking");

            Driver = new ChromeDriver(options);
            //REGISTRO
            //silvia paso#1 Abrir URL
            Driver.Navigate().GoToUrl("https://automationexercise.com/");
            //silvia paso#2 Click en Signup / Login.
            //Driver.FindElement(By.LinkText("Signup / Login")).Click();
            ////silvia paso#3 Completar nombre y correo.
            //Driver.FindElement(By.CssSelector("input[data-qa='signup-name']")).SendKeys("SilviaMV");
            //System.Threading.Thread.Sleep(2000);
            //Driver.FindElement(By.CssSelector("input[data-qa='signup-email']")).SendKeys("silvia88cr16@gmail.com");
            //System.Threading.Thread.Sleep(2000);
            //Driver.FindElement(By.CssSelector("button[data-qa='signup-button']")).Click();
            //System.Threading.Thread.Sleep(3000);
            ////silvia paso#4 Completar formulario.
            //Driver.FindElement(By.Id("id_gender2")).Click();
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("password")).SendKeys("123456");
            //System.Threading.Thread.Sleep(1000);
            //new SelectElement(Driver.FindElement(By.Id("days"))).SelectByValue("30");
            //System.Threading.Thread.Sleep(1000);
            //new SelectElement(Driver.FindElement(By.Id("months"))).SelectByValue("5");
            //System.Threading.Thread.Sleep(1000);
            //new SelectElement(Driver.FindElement(By.Id("years"))).SelectByValue("1988");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("newsletter")).Click();
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("optin")).Click();
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("first_name")).SendKeys("Silvia");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("last_name")).SendKeys("Munoz");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("company")).SendKeys("BCR");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.CssSelector("input[data-qa='address']")).SendKeys("Guanacaste Carrillo");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.CssSelector("input[data-qa='address2']")).SendKeys("Guanacaste Carrillo");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("country")).SendKeys("United States");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("state")).SendKeys("New York");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("city")).SendKeys("New York");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("zipcode")).SendKeys("1234");
            //System.Threading.Thread.Sleep(1000);
            //Driver.FindElement(By.Id("mobile_number")).SendKeys("1234567890");
            //System.Threading.Thread.Sleep(1000);
            ////silvia paso#5 Crear cuenta
            //Driver.FindElement(By.CssSelector("button[data-qa='create-account']")).Click();
            //System.Threading.Thread.Sleep(1000);
            ////silvia paso#6 Validar mensaje: Account Created! 
            //var mensaje = Driver.FindElement(By.CssSelector("h2[data-qa='account-created']")).Text;
            //Assert.AreEqual("ACCOUNT CREATED!", mensaje);
            //System.Threading.Thread.Sleep(1000);
            ////silvia paso#7 Click en Continue y validar login
            //Driver.FindElement(By.CssSelector("[data-qa='continue-button']")).Click();
            //System.Threading.Thread.Sleep(3000);


            ////lOGUEO
            ////silvia paso#1 Ir a Signup / Login
            //Driver.FindElement(By.LinkText("Logout")).Click();
            //System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.LinkText("Signup / Login")).Click();
            System.Threading.Thread.Sleep(2000);
            //silvia paso#2 Ingresar usuario y contraseña válidos. 
            Driver.FindElement(By.CssSelector("input[data-qa='login-email']")).SendKeys("silvia88cr12@gmail.com");
            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.CssSelector("input[data-qa='login-password']")).SendKeys("123456");
            System.Threading.Thread.Sleep(2000);
            //silvia paso#3 Click en login
            Driver.FindElement(By.CssSelector("[data-qa='login-button']")).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            

            //AGREGAR PRODUCTOS AL CARRITO
            // silvia Paso #1 Ir a Products
            Driver.FindElement(By.CssSelector("a[href='/products']")).Click();
            System.Threading.Thread.Sleep(3000);
            // Silvia Paso 2: Agregar dos productos al carrito
            Driver.FindElement(By.CssSelector("a[data-product-id='1']")).Click();
            System.Threading.Thread.Sleep(1000);
            Driver.FindElement(By.CssSelector(".btn.btn-success.close-modal.btn-block")).Click();
            System.Threading.Thread.Sleep(1000);
            Driver.FindElement(By.CssSelector("a[data-product-id='14']")).Click();
            System.Threading.Thread.Sleep(1000);
            Driver.FindElement(By.CssSelector(".btn.btn-success.close-modal.btn-block")).Click();
            System.Threading.Thread.Sleep(5000);
            IJavaScriptExecutor ct = (IJavaScriptExecutor)Driver;
            // Silvia Paso 3: Click en View Cart
            Driver.FindElement(By.CssSelector("a[href='/view_cart']")).Click();
            System.Threading.Thread.Sleep(3000);
            // Silvia Paso 4: Validar que los productos aparecen en la tabla y que el total se muestra correctamente
            var productos = Driver.FindElements(By.CssSelector("#cart_info_table tbody tr"));
            Assert.AreEqual(2, productos.Count, "Deberían aparecer 2 productos en el carrito.");
            System.Threading.Thread.Sleep(3000);
            var montoTotal = Driver.FindElement(By.XPath("//td[@class='total']/following-sibling::td")).Text;
            System.Threading.Thread.Sleep(3000);

             //US FORM
            //Silvia Paso 1: Ir a Contact Us.
            Driver.FindElement(By.CssSelector("a[href='/contact_us']")).Click();
            System.Threading.Thread.Sleep(1000);
            IJavaScriptExecutor cu = (IJavaScriptExecutor)Driver;
            //Silvia Paso 2: Completar nombre, email, asunto y mensaje. 
            Driver.FindElement(By.Name("name")).SendKeys("Silvia");
            System.Threading.Thread.Sleep(1000);
            Driver.FindElement(By.Name("email")).SendKeys("silvia88cr12@gmail.com");
            System.Threading.Thread.Sleep(1000);
            Driver.FindElement(By.Name("subject")).SendKeys("Test Silvia");
            System.Threading.Thread.Sleep(1000);
            Driver.FindElement(By.Id("message")).SendKeys("Prueba paso #4.");
            System.Threading.Thread.Sleep(1000);
            //Silvia Paso 3: Adjuntar un archivo. 
            Driver.FindElement(By.Name("upload_file")).SendKeys(@"C:\Users\ofgm1\source\repos\Test.txt");
            System.Threading.Thread.Sleep(1000);
            // Silvia Paso 5: Click en Submit
            Driver.FindElement(By.Name("submit")).Click();
            System.Threading.Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            alert.Accept(); // Presiona OK en el alert

            //Silvia Paso 5: Mensaje de exito 
            WebDriverWait wait1 = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
            var mensajeExito = wait1.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".status.alert.alert-success"))).Text;
            Assert.AreEqual("Success! Your details have been submitted successfully.", mensajeExito);






        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
               // Driver.Quit();
                Driver.Dispose();
            }
        }
    }
}
