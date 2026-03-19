using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages.MainComponents
{
    public class menuPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public menuPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(25));
        }

        // Declaración de los elementos de la página

        //Elementos del menu
        private static By SignupLoginBy => By.CssSelector("li a[href=\"/login\"]");
        private static By ProductsBy => By.CssSelector("li a[href=\"/products\"]");
        private static By ContactUsBy => By.CssSelector("li a[href=\"/contact_us\"]");

        // Logout can vary depending on page state; try a few selectors.
        private static readonly By[] LogoutCandidates =
        [
            By.CssSelector("li a[href=\"/logout\"]"),
            By.CssSelector("a[href=\"/logout\"]"),
            By.PartialLinkText("Logout"),
            By.XPath("//a[contains(@href,'/logout')]")
        ];

        private IWebElement WaitVisible(By by) => _wait.Until(d =>
        {
            try
            {
                var el = d.FindElement(by);
                return el.Displayed ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }
        });

        // Declaración de metodos para interactuar con los elementos del menu de navegación
        public void ClickSignUpLogin() => WaitVisible(SignupLoginBy).Click();

        public void ClickProductOption() => WaitVisible(ProductsBy).Click();

        public void ClickContactUsOption() => WaitVisible(ContactUsBy).Click();

        public string validatedUserLogout()
        {
            // First, wait for the page to reflect a logged-in state.
            _wait.Until(d => d.PageSource.Contains("Logged in as") || d.Url.Contains("/account") || d.Url.Contains("/"));

            foreach (var by in LogoutCandidates)
            {
                try
                {
                    var el = _driver.FindElement(by);
                    if (el.Displayed)
                        return el.Text;
                }
                catch (NoSuchElementException)
                {
                    // try next
                }
                catch (StaleElementReferenceException)
                {
                    // try next
                }
            }

            throw new NoSuchElementException($"No se encontró el enlace de Logout después de iniciar sesión. URL actual: '{_driver.Url}'.");
        }
    }
}
