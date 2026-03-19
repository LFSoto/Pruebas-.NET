using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Drawing;
using System.Runtime.InteropServices;

namespace AutomationPracticeDemo.Tests.Utils
{
    public static class EsperasHelper
    {
        public static void Esperar(IWebDriver driver, IWebElement elemento, int tiempo)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(tiempo));
            wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
        }
    }
}
