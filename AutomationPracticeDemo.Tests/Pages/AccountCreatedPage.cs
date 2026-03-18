
using AutomationPracticeDemo.Tests.Pages;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Test.Pages;

public class AccountCreatedPage : BasePage
{
    private By createdAccountH2TitleLocator = By.XPath("//*[@data-qa='account-created']");
    private By continueButtonLocator = By.XPath("//*[@data-qa='continue-button']");

    public AccountCreatedPage(IWebDriver driver) : base(driver)
    {
    }

    public string GetCreatedAccountTitle()
    {
        return GetElementText(createdAccountH2TitleLocator);
    }

    public HomePage ClickContinueButton()
    {
        ClickElement(continueButtonLocator);
        return new HomePage(Driver);
    }
}
