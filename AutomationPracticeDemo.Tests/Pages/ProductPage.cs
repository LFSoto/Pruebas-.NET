using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver _driver;
        public ProductPage(IWebDriver driver)
        {
            _driver = driver;
        }
    

     private IWebElement BtnProduct => _driver.FindElement(By.XPath("//a[@href='/products']"));
        private IWebElement addToCartBtn => _driver.FindElement(By.XPath("//p[text()='Blue Top']/following-sibling::a[@class='btn btn-default add-to-cart']"));
        private IWebElement addToCartBtn2 => _driver.FindElement(By.XPath("//p[text()='Winter Top']/following-sibling::a[@class='btn btn-default add-to-cart']"));
        private IWebElement bluetop => _driver.FindElement(By.XPath("//a[contains(text(),'Blue Top')]"));
        private IWebElement WinterTop => _driver.FindElement(By.XPath("//a[contains(text(),'Winter Top')]"));
        private IWebElement articulocarrito1 => _driver.FindElement(By.XPath("//td[@class='cart_description']/h4/a[text()='Blue Top']/ancestor::tr"));
        private IWebElement articulocarrito2 => _driver.FindElement(By.XPath("//td[@class='cart_description']/h4/a[text()='Blue Top']/ancestor::tr"));


        public bool existBluetop()
        {
            if (bluetop.Displayed == false)
            {
                return false;
            }

            return true;

        }
        public bool existWinterTop()
        {
            if (WinterTop.Displayed == false)
            {
                return false;
            }
            return true;
        }

        public void ClickProduct()
        {
            BtnProduct.Click();
        }

        public void AddToCart()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", addToCartBtn);
            addToCartBtn.Click();
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement continueShoppingBtn = wait.Until(drv =>
            {
                try
                {
                    var el = drv.FindElement(By.XPath("//button[text()='Continue Shopping']"));
                    return (el != null && el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            continueShoppingBtn.Click();
        }
        public void AddToCart2()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", addToCartBtn2);
            addToCartBtn2.Click();
            var wait2 = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement Viewcart = wait2.Until(drv =>
            {
                try
                {
                    var el = drv.FindElement(By.XPath("//u[normalize-space(text())='View Cart']"));
                    return (el != null && el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });
            Viewcart.Click();
        }

        public void totalPrice1()
        {
            var articulo1 = articulocarrito1;
            string precioTexto = articulo1.FindElement(By.XPath(".//td[@class='cart_price']/p")).Text;
            string cantidadTexto = articulo1.FindElement(By.XPath(".//td[@class='cart_quantity']/button")).Text;
            string totalTexto = articulo1.FindElement(By.XPath(".//td[@class='cart_total']/p")).Text;
            decimal precio = decimal.Parse(precioTexto.Replace("Rs.", "").Trim());
            int cantidad = int.Parse(cantidadTexto.Trim());
            decimal total = decimal.Parse(totalTexto.Replace("Rs.", "").Trim());
            decimal esperado = precio * cantidad;
            Assert.That(total, Is.EqualTo(esperado), "Precio correcto primer articulo");
        }
        public void totalPrice2()
        {
            var articulo2 = articulocarrito2;
            string precioTexto2 = articulo2.FindElement(By.XPath(".//td[@class='cart_price']/p")).Text;
            string cantidadTexto2 = articulo2.FindElement(By.XPath(".//td[@class='cart_quantity']/button")).Text;
            string totalTexto2 = articulo2.FindElement(By.XPath(".//td[@class='cart_total']/p")).Text;
            decimal precio2 = decimal.Parse(precioTexto2.Replace("Rs.", "").Trim());
            int cantidad2 = int.Parse(cantidadTexto2.Trim());
            decimal total2 = decimal.Parse(totalTexto2.Replace("Rs.", "").Trim());
            decimal esperado2 = precio2 * cantidad2;
            Assert.That(total2, Is.EqualTo(esperado2), "Precio correcto segundo articulo");
        }


    }

    }
