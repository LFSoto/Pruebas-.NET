using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CartPage(IWebDriver driver, TimeSpan timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, timeout);
        }

        public IList<IWebElement> GetCartRows()
        {
            _wait.Until(d => d.FindElements(By.XPath("//table//tbody//tr | //div[@class='cart_info']//tr")).Count > 0);
            return _driver.FindElements(By.XPath("//table//tbody//tr | //div[@class='cart_info']//tr")).ToList();
        }

        public string GetTotalText()
        {
            var el = _driver.FindElements(By.XPath("//*[contains(., 'Total') or contains(., 'TOTAL') or contains(@class,'total')]")).FirstOrDefault();
            return el?.Text;
        }
    }
}
