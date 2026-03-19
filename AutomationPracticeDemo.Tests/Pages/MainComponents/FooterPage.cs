using AutomationPracticeDemoTests.Pages;
using OpenQA.Selenium;

namespace AutomationPracticeDemoTest.Pages.MainComponents;

public class FooterPage : BasePage
{
    private By newsletterInputLocator = By.Id("susbscribe_email");
    private By suscribeButtonLocator = By.Id("subscribe");
    private By alertSuccessMessageLocator = By.XPath($"//div[@class='alert-success alert' and contains(., 'subscribed!')]");

    public FooterPage(IWebDriver driver) : base(driver)
    {
    }

    public void SubscribeToNewsletter(string email)
    {
        ScrollToElement(newsletterInputLocator);
        TypeText(newsletterInputLocator, email);
        ClickElement(suscribeButtonLocator);
    }

    public string GetSuccessSubscriptionMessage()
    {
        return GetElementText(alertSuccessMessageLocator);
    }
}
