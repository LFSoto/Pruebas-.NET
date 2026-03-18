
using AutomationPracticeDemo.Tests.Pages;
using OpenQA.Selenium;

namespace AutomationPracticeDemo.Test.Pages;

public class CartPage : BasePage
{
    private By GetPriceByProductLocator(string nombreProducto) 
    {
        return By.XPath($"//tr[contains(., '{nombreProducto}')]//td[@class='cart_price']/p");
    }

    private By GetQuantityByProductLocator(string nombreProducto)
    {
        return By.XPath($"//tr[contains(., '{nombreProducto}')]//td[@class='cart_quantity']/button");
    }

    private By GetTotalByProductLocator(string nombreProducto)
    {
        return By.XPath($"//tr[contains(., '{nombreProducto}')]//td[@class='cart_total']/p");
    }

    public CartPage(IWebDriver driver) : base(driver)
    {
    }

    public int GetPriceByProduct(string nombreProducto)
    {
        return int.Parse(GetElementText(GetPriceByProductLocator(nombreProducto)).Replace("Rs. ", ""));
    }

    public int GetQuantityByProduct(string nombreProducto)
    {
        return int.Parse(GetElementText(GetQuantityByProductLocator(nombreProducto)));
    }

    public int GetTotalByProduct(string nombreProducto)
    {
        return int.Parse(GetElementText(GetTotalByProductLocator(nombreProducto)).Replace("Rs. ", ""));
    }
}
