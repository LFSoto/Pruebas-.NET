using AutomationPracticeDemoTests;
using AutomationPracticeDemoTests.Pages;
using AutomationPracticeDemoTests.Utils;

namespace AutomationPracticeDemoTest.Tests.Products;

[TestFixture]
public class ProductsTest : BaseTest
{
    private string folderName = "ProductsTest";
    private readonly string[] _initialProductNames = 
        [ "Rust Red Linen Saree", "Frozen Tops For Kids", "Sleeveless Dress", "Winter Top",
        "Blue Top", "Colour Blocked Shirt – Sky Blue" ];
    private List<string> availableProducts;

    [SetUp]
    public void Setup()
    {
        availableProducts = new List<string>(_initialProductNames);
    }

    [Test]
    public void ValidarTotalEnCarritoPorProducto()
    {
        var producto1 = GetRandomProductName();
        var producto2 = GetRandomProductName();

        var homePage = new HomePage(Driver);
        var productsPage = homePage.TopMenu.GoToProductsPage();

        // Agregar el primer producto al carrito y validar el mensaje de éxito
        productsPage.AgregarProductoAlCarrito(producto1);
        Assert.That(productsPage.GetSuccessMessageAfterAddingProductToCart(),
            Is.EqualTo("Added!"), "El mensaje de producto agregado no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"Producto_Agregado_{producto1}".Trim(), folderName);
        productsPage.ClickContinueShoppingModal();
        // Agregar el segundo producto al carrito y validar el mensaje de éxito
        productsPage.AgregarProductoAlCarrito(producto2);
        Assert.That(productsPage.GetSuccessMessageAfterAddingProductToCart(),
            Is.EqualTo("Added!"), "El mensaje de producto agregado no es el esperado.");
        ScreenshotHelper.TakeScreenshot(Driver, $"Producto_Agregado_{producto2}".Trim(), folderName);
        var cartPage = productsPage.GoToCartPage();

        Assert.That(cartPage.GetTotalByProduct(producto1),
            Is.EqualTo(cartPage.GetPriceByProduct(producto1) * cartPage.GetQuantityByProduct(producto1)),
            $"El total del producto '{producto1}' no es correcto.");
        Assert.That(cartPage.GetTotalByProduct(producto2),
            Is.EqualTo(cartPage.GetPriceByProduct(producto2) * cartPage.GetQuantityByProduct(producto2)),
            $"El total del producto '{producto2}' no es correcto.");
        ScreenshotHelper.TakeScreenshot(Driver, $"Validacion_Cart_Products_{producto1}_{producto2}".Trim(), folderName);
    }

    public string GetRandomProductName()
    {
        // Si ya no quedan productos disponibles, reiniciar la lista
        if (availableProducts == null || availableProducts.Count == 0)
        {
            availableProducts = new List<string>(_initialProductNames);
        }
        var random = new Random();
        var index = random.Next(availableProducts.Count);
        var randomProductName = availableProducts[index];
        // Remover el producto seleccionado para evitar repetición
        availableProducts.RemoveAt(index);
        Console.WriteLine($"Nombre de producto seleccionado aleatoriamente: {randomProductName}");
        return randomProductName;
    }
}
