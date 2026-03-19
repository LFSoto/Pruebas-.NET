using AutomationPracticeDemoTests.Pages;
using AutomationPracticeDemoTest.Pages.SingUpLogin;
using OpenQA.Selenium;

namespace AutomationPracticeDemoTest.Pages.MainComponents;

public class TopMenuPage : BasePage
{
    private By HomeOptionLocator = By.XPath("//a[text()=' Home']");
    private By ProductsOptionLocator = By.XPath("//a[text()=' Products']");
    private By CartOptionLocator = By.XPath("//a[text()=' Cart']");
    private By SingUpLoginOptionLocator = By.XPath("//a[text()=' Signup / Login']");
    private By LoggedUserLabelLocator = By.XPath("//a[contains(text(), 'Logged in as')]");
    private By ContactUsOptionLocator = By.XPath("//a[text()=' Contact us']");

    public TopMenuPage(IWebDriver driver) : base(driver)
    {

    }

    public SingUpLoginPage GoToSingUpPage()
    {
        ClickElement(SingUpLoginOptionLocator);
        return new SingUpLoginPage(Driver);
    }

    public string GetLoggedUser()
    {
        return GetElementText(LoggedUserLabelLocator);
    }

    public ContactUsPage GoToContactUsPage()
    {
        ClickElement(ContactUsOptionLocator);
        return new ContactUsPage(Driver);
    }

    public ProductsPage GoToProductsPage()
    {
        ClickElement(ProductsOptionLocator);
        return new ProductsPage(Driver);
    }

}
