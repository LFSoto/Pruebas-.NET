using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public ProductsPage(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, timeout);
        }

        public void GoTo()
        {
            _driver.Navigate().GoToUrl("https://automationexercise.com/products");
            _wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Add to cart')] | //button[contains(., 'Add to cart')]")).Count > 0);
        }

        public void AddToCartByIndex(int index)
        {
            var addBtns = _driver.FindElements(By.XPath("//a[contains(., 'Add to cart')] | //button[contains(., 'Add to cart')]")).ToList();
            if (addBtns.Count > index)
            {
                var el = addBtns[index];
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", el);
                try { el.Click(); } catch { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", el); }
                // wait for modal
                _wait.Until(d => d.FindElements(By.XPath("//a[contains(., 'Continue Shopping')] | //a[contains(., 'View Cart')] | //button[contains(., 'View Cart')]")).Count > 0);
            }
        }

        public void ClickViewCartFromModal()
        {
            var view = _driver.FindElements(By.XPath("//a[contains(., 'View Cart')] | //button[contains(., 'View Cart')]")).FirstOrDefault();
            if (view != null) { try { view.Click(); } catch { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", view); } }
            _wait.Until(d => d.FindElements(By.XPath("//table//tbody//tr | //div[@class='cart_info']//tr")).Count > 0);
        }
    }
}
