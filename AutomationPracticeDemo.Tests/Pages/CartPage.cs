using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class CartPage
    {
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    // --- IWebElement Properties ---
    private ReadOnlyCollection<IWebElement> CartRows =>
        _driver.FindElements(By.CssSelector("table#cart_info_table tbody tr"));

    private IWebElement RowTotalCell(IWebElement row) =>
        row.FindElement(By.ClassName("cart_total"));

    public CartPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    // --- Actions ---

    public ReadOnlyCollection<IWebElement> GetCartRows() => CartRows;

    public void ValidateProductCount(int expectedCount)
    {
        Assert.That(CartRows.Count, Is.EqualTo(expectedCount),
            $"Se esperaban {expectedCount} productos en el carrito, pero hay {CartRows.Count}.");
    }

    public double CalculateTotalFromRows()
    {
        double total = 0;

        foreach (var row in CartRows)
        {
            string rawText = RowTotalCell(row).Text;
            string numericOnly = System.Text.RegularExpressions.Regex
                                    .Replace(rawText, @"[^\d]", "");

            if (!string.IsNullOrEmpty(numericOnly))
                total += double.Parse(numericOnly);
        }

        Console.WriteLine($"Suma total calculada: Rs. {total}");
        return total;
    }

    public void ValidateTotalGreaterThanZero(double total)
    {
        Assert.That(total, Is.GreaterThan(0),
            "El total del carrito debería ser mayor a cero.");
    }

    public void ScrollToFirstRow()
    {
        if (CartRows.Count > 0)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", CartRows[0]);
        }
    }

    }
}