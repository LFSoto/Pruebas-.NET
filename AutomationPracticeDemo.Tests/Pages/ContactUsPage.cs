
using AutomationPracticeDemoTests.Pages;
using OpenQA.Selenium;

namespace AutomationPracticeDemoTest.Pages;

public class ContactUsPage : BasePage
{
    private By nameInputLocator = By.CssSelector($"[data-qa='name']");
    private By emailInputLocator = By.CssSelector($"[data-qa='email']");
    private By subjetInputLocator = By.CssSelector($"[data-qa='subject']");
    private By messageInputLocator = By.CssSelector($"[data-qa='message']");
    private By uploadFileButtonLocator = By.CssSelector($"[name='upload_file']");
    private By submitButtonLocator = By.CssSelector($"[data-qa='submit-button']");
    private By alertSuccessMessageLocator = By.XPath($"//div[@class='status alert alert-success' and contains(., 'Success')]");

    public ContactUsPage(IWebDriver driver) : base(driver)
    {
    }

    public void FillContactUsForm(string name, string email, string subject, string message, string image)
    {
        TypeText(nameInputLocator, name);
        TypeText(emailInputLocator, email);
        TypeText(subjetInputLocator, subject);
        TypeText(messageInputLocator, message);
        UploadImage(uploadFileButtonLocator, image);
    }

    public void ClickSubmitButton()
    {
        ClickElement(submitButtonLocator);
    }

    public string GetAlertSuccessMessage()
    {
        return GetElementText(alertSuccessMessageLocator);
    }

}
