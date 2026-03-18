using AutomationPracticeDemo.Tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages;

public class BasePage
{
    protected IWebDriver Driver;
    protected WebDriverWait Wait;

    public BasePage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
    }

    protected IWebElement GetElementUntilIsVisible(By locator, int timeoutInSeconds = 30)
    {
        try
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return localWait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

        }
        catch (WebDriverTimeoutException ex)
        {
            throw new NoSuchElementException($"El elemento con el locator {locator} no se volvió visible después de {timeoutInSeconds} segundos.", ex);
        }
    }

    protected void WaitUntilCssValueIs(By locator, string cssProperty, string expectedValue, int timeoutInSeconds = 30)
    {
        try
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            localWait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    string actualValue = element.GetCssValue(cssProperty);
                    return actualValue == expectedValue;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            });
        }
        catch (WebDriverTimeoutException ex)
        {
            throw new Exception($"El elemento con el locator {locator} no alcanzó el valor CSS '{expectedValue}' para" +
                $" la propiedad '{cssProperty}' después de {timeoutInSeconds} segundos.", ex);
        }
    }

    protected void TypeText(By locator, string text)
    {
        try {
            var element = GetElementUntilIsVisible(locator);
            element.Clear();
            element.SendKeys(text);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al escribir el texto '{text}' en el elemento con locator {locator}.", ex);
        }
    }

    protected void ClickElement(By locator)
    {
        try
        {
            var element = GetElementUntilIsVisible(locator);
            element.Click();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al hacer clic en el elemento con locator {locator}.", ex);
        }
    }

    protected void WaitUntilElementIsVisible(By locator, int timeoutInSeconds = 30)
    {
        try
        {
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
            localWait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }
        catch (WebDriverTimeoutException ex)
        {
            throw new NoSuchElementException($"El elemento con el locator {locator} no se volvió visible después de {timeoutInSeconds} segundos.", ex);
        }
    }

    public string GetElementText(By locator)
    {
        try
        {
            var element = GetElementUntilIsVisible(locator);
            return element.Text;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener el texto del elemento con locator {locator}.", ex);
        }
    }

    protected void SelectOptionByText(By locator, string optionText)
    {
        try
        {
            var element = GetElementUntilIsVisible(locator);
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(optionText);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al seleccionar la opción '{optionText}' en el elemento con locator {locator}.", ex);
        }
    }

    protected IWebElement GetElement(By locator)
    {
        try
        {
            return Driver.FindElement(locator);
        }
        catch (NoSuchElementException ex)
        {
            throw new NoSuchElementException($"El elemento con el locator {locator} no se encontró en el DOM.", ex);
        }
    }

    protected void ScrollToElement(By locator, int attempts = 3)
    {
        var retryDelayMs = 500;

        if (attempts <= 0)
            throw new Exception($"Error al hacer scroll hasta el elemento con locator {locator}: número de intentos inválido.");

        try
        {
            var element = GetElement(locator);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({ block: 'center', inline: 'nearest' });", element);

            // Small pause to allow layout to stabilize after scrolling
            Thread.Sleep(200);

            if (element.Displayed)
                return;
        }
        catch (Exception ex) when (ex is NoSuchElementException || ex is StaleElementReferenceException)
        {
            if (attempts == 1)
                throw new Exception($"Error al hacer scroll hasta el elemento con locator {locator} después de varios intentos.", ex);
            // otherwise continue to retry
        }

        // wait a bit before next attempt and retry recursively
        Thread.Sleep(retryDelayMs);
        ScrollToElement(locator, attempts - 1);
    }

    protected void UploadImage(By locator, string imageFileName)
    {
        // Obtiene el input type=file y envía la ruta completa del archivo para subirlo
        var fileInput = GetElementUntilIsVisible(locator);

        // Build images directory under project: <projectRoot>/Reporte/Images
        var projectPath = ScreenshotHelper.GetPathFromProject();
        var imagesDir = Path.Combine(projectPath, "Images", imageFileName);

        if (!File.Exists(imagesDir))
            throw new FileNotFoundException($"No se encontró la imagen '{imageFileName}' en rutas esperadas.", imagesDir);

        fileInput.SendKeys(imagesDir);
    }

    public void AcceptAlert()
    {
        IAlert alert = Driver.SwitchTo().Alert();
        alert.Accept();
    }

    protected void MouseHover(By locator)
    {
        try
        {
            var element = GetElementUntilIsVisible(locator);
            var actions = new OpenQA.Selenium.Interactions.Actions(Driver);
            actions.MoveToElement(element).Perform();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al hacer hover sobre el elemento con locator {locator}.", ex);
        }
    }

    public void ClickByJs(By locator)
    {
        try
        {
            var element = GetElementUntilIsVisible(locator);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al hacer clic por JS en el elemento con locator {locator}.", ex);
        }
    }
}
