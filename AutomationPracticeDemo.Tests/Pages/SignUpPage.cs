using AutomationPracticeDemo.Test.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages;

public class SignUpPage : BasePage
{
    private By passwordInputLocator = By.XPath("//*[@data-qa='password']");
    private By firstNameInputLocator = By.XPath("//*[@data-qa='first_name']");
    private By lastNameInputLocator = By.XPath("//*[@data-qa='last_name']");
    private By addressInputLocator = By.XPath("//*[@data-qa='address']");
    private By countrySelectLocator = By.XPath("//*[@data-qa='country']");
    private By stateInputLocator = By.XPath("//*[@data-qa='state']");
    private By cityInputLocator = By.XPath("//*[@data-qa='city']");
    private By zipCodeInputLocator = By.XPath("//*[@data-qa='zipcode']");
    private By mobileNumberInputLocator = By.XPath("//*[@data-qa='mobile_number']");
    private By createAccountButtonLocator = By.XPath("//*[@data-qa='create-account']");

    public SignUpPage(IWebDriver driver) : base(driver)
    {
       
    }

    public void FillSignUpForm(string password, string firstName, string lastName, string address,
        string country, string state, string city, string zipCode, string mobileNumber)
    {
        TypeText(passwordInputLocator, password);
        TypeText(firstNameInputLocator, firstName);
        TypeText(lastNameInputLocator, lastName);
        TypeText(addressInputLocator, address);
        SelectOptionByText(countrySelectLocator, country);
        TypeText(stateInputLocator, state);
        TypeText(cityInputLocator, city);
        TypeText(zipCodeInputLocator, zipCode);
        TypeText(mobileNumberInputLocator, mobileNumber);
    }

    public void ClickCreateAccount()
    {
        ClickElement(createAccountButtonLocator);
    }

    public AccountCreatedPage GoToAccountCreatedPage()
    {
        ClickCreateAccount();
        return new AccountCreatedPage(Driver);
    }


}
