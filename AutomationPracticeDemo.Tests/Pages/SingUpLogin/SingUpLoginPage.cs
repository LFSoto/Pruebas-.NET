using AutomationPracticeDemoTest.Pages.MainComponents;
using AutomationPracticeDemoTests.Pages;
using OpenQA.Selenium;

namespace AutomationPracticeDemoTest.Pages.SingUpLogin;

public class SingUpLoginPage : BasePage
{
    private By emailLoginInputLocator = By.XPath("//*[@data-qa='login-email']");
    private By passwordInputLocator = By.XPath("//*[@data-qa='login-password']");
    private By loginButtonLocator = By.XPath("//*[@data-qa='login-button']");
    private By nameSingUpInputLocator = By.XPath("//*[@data-qa='signup-name']");
    private By emailSingUpInputLocator = By.XPath("//*[@data-qa='signup-email']");
    private By singUpButtonLocator = By.XPath("//*[@data-qa='signup-button']");
    private By titleLoginAccount = By.CssSelector("div.login-form h2");
    private By messageIncorrectPassword = By.CssSelector("div.login-form p");

    private TopMenuPage topMenuPage;

    public SingUpLoginPage(IWebDriver driver) : base(driver)
    {
       topMenuPage = new TopMenuPage(driver);
    }

    public void LoginUsuario(string email, string password)
    {
        TypeText(emailLoginInputLocator, email);
        TypeText(passwordInputLocator, password);
        MouseHover(loginButtonLocator);
        // Click en el botón de Login
        ClickByJs(loginButtonLocator);
    }

    public SignUpPage SingUpNuevoUsuario(string name, string email)
    {
        // Name
        TypeText(nameSingUpInputLocator, name);
        // Email
        TypeText(emailSingUpInputLocator, email);
        // Click en el botón de SingUp
        ClickElement(singUpButtonLocator);
        return new SignUpPage(Driver);
    }

    public string GetTitleLoginAccount()
    {
        return GetElementText(titleLoginAccount);
    }

    public string GetInvalidLoginErrorMessage()
    {
        return GetElementText(messageIncorrectPassword);
    }

}
