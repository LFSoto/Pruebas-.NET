using AutomationPracticeDemoTest.Utils;
using AutomationPracticeDemoTests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemoTests.Pages;

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
        if (attempts <= 0)
            throw new Exception($"Error al hacer scroll hasta el elemento con locator {locator}: número de intentos inválido.");

        try
        {
            // Attempt to scroll the element into view
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({ block: 'center', inline: 'nearest' });", GetElement(locator));

            // Wait briefly, polling until the element is visible. This avoids Thread.Sleep and uses WebDriver's waiting mechanism.
            var localWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
            localWait.PollingInterval = TimeSpan.FromMilliseconds(200);
            localWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
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

            return;
        }
        catch (WebDriverTimeoutException)
        {
            // If we timed out waiting for visibility, we'll retry below (unless attempts exhausted)
            if (attempts == 1)
                throw new Exception($"Error al hacer scroll hasta el elemento con locator {locator} después de varios intentos.");
        }
        catch (Exception ex) when (ex is NoSuchElementException || ex is StaleElementReferenceException)
        {
            if (attempts == 1)
                throw new Exception($"Error al hacer scroll hasta el elemento con locator {locator} después de varios intentos.", ex);
            // otherwise continue to retry
        }

        // Retry recursively without Thread.Sleep; WebDriverWait handled polling
        ScrollToElement(locator, attempts - 1);
    }

    protected void UploadImage(By locator, string imageFileName)
    {
        // Obtiene el input type=file y envía la ruta completa del archivo para subirlo
        var fileInput = GetElementUntilIsVisible(locator);

        // Build images directory under project: <projectRoot>/Resource/Images
        var projectPath = AutomationUtils.GetPathFromProject();
        var imagesDir = Path.Combine(projectPath, "Resource", "Images", imageFileName);

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
