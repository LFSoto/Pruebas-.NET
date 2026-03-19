using AutomationPracticeDemoTests.Pages;
using AutomationPracticeDemoTests.Utils;
using OpenQA.Selenium;

namespace AutomationPracticeDemoTest.Pages;

public class ProductsPage : BasePage
{
    private By modalLabelProductAdded = By.XPath("//div[@id='cartModal']//h4[contains(., 'Added!')]");
    private By modalButtonContinueShopping = By.XPath("//div[@id='cartModal']//button[contains(., 'Continue Shopping')]");
    private By modalButtonViewCart = By.XPath("//div[@id='cartModal']//u[contains(., 'View Cart')]");

    private By GetProductoButtonAddCart(string productName)
    {
        return By.XPath($"(//div[@class='single-products' and contains(., '{productName}')]//i)[1]");
    }

    private By GetProductoTitleH2Animation(string productName)
    {
        return By.XPath($"//div[contains(@class, 'product-overlay') and .//h2[contains(., '{productName}')]]");
    }

    private By GetProductoButtonAddCartAfterAnimation(string productName)
    {
        return By.XPath($"(//div[@class='single-products' and contains(., '{productName}')]//i)[2]");
    }


    public ProductsPage(IWebDriver driver) : base(driver)
    {
    }

    public void AgregarProductoAlCarrito(string productName)
    {
        // Se hace scroll hasta el producto para asegurar que esté visible y se pueda interactuar con él
        ScrollToElement(GetProductoButtonAddCart(productName));
        MouseHover(GetProductoButtonAddCart(productName));
        // Se espera a que el botón de agregar al carrito esté visible antes de hacer click
        WaitUntilCssValueIs(GetProductoTitleH2Animation(productName), "display", "block");
        MouseHover(GetProductoButtonAddCartAfterAnimation(productName));
        // Se hace click en el botón de agregar al carrito después de la animación
        ClickByJs(GetProductoButtonAddCartAfterAnimation(productName));

    }

    public string GetSuccessMessageAfterAddingProductToCart()
    {
        return GetElementText(modalLabelProductAdded);
    }

    public void ClickContinueShoppingModal()
    {
        ClickElement(modalButtonContinueShopping);
    }

    public CartPage GoToCartPage()
    {
        ClickElement(modalButtonViewCart);
        return new CartPage(Driver);
    }
}
