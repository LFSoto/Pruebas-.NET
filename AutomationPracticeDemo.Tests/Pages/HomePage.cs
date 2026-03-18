using AutomationPracticeDemo.Test.Pages.MainComponents;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Tests.Pages;

public class HomePage : BasePage
{
    private By sliderCarouselLocator = By.Id("slider-carousel");
    public TopMenuPage TopMenu;
    public FooterPage Footer;

    public HomePage(IWebDriver driver) : base(driver)
    {
        TopMenu = new (driver);
        Footer = new (driver);
        WaitUntilElementIsVisible(sliderCarouselLocator);
    }
}
