using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        private static By TitleLoginBy => By.CssSelector("div.login-form h2");
        private static By EmailLoginBy => By.CssSelector("input[data-qa=\"login-email\"]");
        private static By PasswordLoginBy => By.CssSelector("input[data-qa=\"login-password\"]");
        private static By LoginButtonBy => By.CssSelector("button[data-qa=\"login-button\"]");

        // Primary locator from the original code
        private static By IncorrectPasswordMsgBy => By.CssSelector("div.login-form p");

        // Fallbacks (site sometimes renders messages in different containers)
        private static readonly By[] IncorrectMessageCandidates =
        [
            IncorrectPasswordMsgBy,
            By.CssSelector("form[action='/login'] p"),
            By.CssSelector("div.login-form form p"),
            By.XPath("//div[contains(@class,'login-form')]//p"),
            By.XPath("//p[contains(.,'incorrect') or contains(.,'Incorrect')]")
        ];

        private IWebElement? TryFindVisible(By by)
        {
            try
            {
                var el = _driver.FindElement(by);
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
        }

        private IWebElement WaitVisible(By by) => _wait.Until(_ => TryFindVisible(by));

        //Metodo para obtener el título de Login 
        public string GetTitleLoginAccount() => WaitVisible(TitleLoginBy).Text;

        //Metodo para llenar el formulario de Login
        public void FillLogin(string email, string password)
        {
            var emailEl = WaitVisible(EmailLoginBy);
            emailEl.Clear();
            emailEl.SendKeys(email);

            var passEl = WaitVisible(PasswordLoginBy);
            passEl.Clear();
            passEl.SendKeys(password);
        }

        //Metodo para enviar el formulario de Login
        public void SubmitLogin() => WaitVisible(LoginButtonBy).Click();

        //Metodo para obtener el mensaje de contraseña incorrecta
        public string GetMessageIncorrectPassword()
        {
            // Wait for either an error message OR a successful-login indicator (logout).
            _wait.Until(d => d.PageSource.Contains("Your email or password is incorrect") || d.PageSource.Contains("Logout"));

            foreach (var by in IncorrectMessageCandidates)
            {
                var el = TryFindVisible(by);
                if (el != null)
                    return el.Text;
            }

            throw new NoSuchElementException("No se encontró el mensaje de error de login (password incorrecto). Puede que el sitio haya iniciado sesión o haya cambiado el HTML.");
        }
    }
}
