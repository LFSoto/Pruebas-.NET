using System;
using System.Linq;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationPracticeDemo.Tests.Pages
{
    public class FormPage
    {
        private readonly IWebDriver _driver;

        public FormPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        private IWebElement NameInput => _driver.FindElement(By.Id("name"));
        private IWebElement EmailInput => _driver.FindElement(By.Id("email"));
        private IWebElement PhoneInput => _driver.FindElement(By.Id("phone"));
        private IWebElement CountryDropdown => _driver.FindElement(By.Id("country"));
        private IWebElement SubmitButton => _driver.FindElement(By.ClassName("submit-btn"));

        public void FillForm(string name, string email, string phone, string country)
        {
            NameInput.Clear();
            NameInput.SendKeys(name);

            EmailInput.Clear();
            EmailInput.SendKeys(email);

            PhoneInput.Clear();
            PhoneInput.SendKeys(phone);

            SelectCountryByText(country);
        }

        public void SelectCountryByText(string country)
        {
            if (string.IsNullOrWhiteSpace(country)) return;
            var select = new SelectElement(CountryDropdown);
            var option = select.Options.FirstOrDefault(o => string.Equals(o.Text?.Trim(), country.Trim(), StringComparison.OrdinalIgnoreCase));
            if (option != null) { select.SelectByText(option.Text); return; }
            option = select.Options.FirstOrDefault(o => o.Text != null && o.Text.IndexOf(country.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
            if (option != null) { select.SelectByText(option.Text); return; }
            try { select.SelectByValue(country); } catch { /* ignore */ }
        }

        public string GetName() => NameInput.GetAttribute("value");
        public string GetEmail() => EmailInput.GetAttribute("value");
        public string GetPhone() => PhoneInput.GetAttribute("value");

        // --- Fecha: selectores y rango ---
        // Selector de fecha 1: id="datepicker" (input text, hasDatepicker)
        public void SetDatepicker1(string date)
        {
            SetTextDateInputById("datepicker", date);
        }

        // Selector de fecha 2: id="txtDate" (readonly text, hasDatepicker)
        public void SetDatepicker2(string date)
        {
            SetTextDateInputById("txtDate", date);
        }

        // Selector de rango: inputs type="date" con ids start-date y end-date
        public void SetDateRange(string startDate, string endDate)
        {
            if (startDate == null) throw new ArgumentNullException(nameof(startDate));
            if (endDate == null) throw new ArgumentNullException(nameof(endDate));

            var startEl = _driver.FindElement(By.Id("start-date"));
            var endEl = _driver.FindElement(By.Id("end-date"));

            var startVal = ToHtmlDateValue(startDate);
            var endVal = ToHtmlDateValue(endDate);

            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].value = arguments[1]; arguments[0].dispatchEvent(new Event('input')); arguments[0].dispatchEvent(new Event('change'));", startEl, startVal);
            js.ExecuteScript("arguments[0].value = arguments[1]; arguments[0].dispatchEvent(new Event('input')); arguments[0].dispatchEvent(new Event('change'));", endEl, endVal);
        }

        private void SetTextDateInputById(string id, string date)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));
            var el = _driver.FindElement(By.Id(id));
            if (el == null) throw new NoSuchElementException($"Input with id '{id}' not found");

            try
            {
                el.Clear();
                el.SendKeys(date ?? string.Empty);
                el.SendKeys(Keys.Tab);
                return;
            }
            catch
            {
                // fallback to JS assignment
            }

            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].value = arguments[1]; arguments[0].dispatchEvent(new Event('input')); arguments[0].dispatchEvent(new Event('change'));", el, date ?? string.Empty);
        }

        private static string ToHtmlDateValue(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString)) return string.Empty;
            if (DateTime.TryParse(dateString, out var dt)) return dt.ToString("yyyy-MM-dd");

            var formats = new[] { "dd/MM/yyyy", "d/M/yyyy", "MM/dd/yyyy", "M/d/yyyy" };
            if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) return dt.ToString("yyyy-MM-dd");

            return dateString;
        }

        // --- Fin fechas ---

        public void Submit() => SubmitButton.Click();

        public string WaitForAlertAndAccept(int timeoutSeconds = 5)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(d =>
            {
                try { d.SwitchTo().Alert(); return true; }
                catch { return false; }
            });

            var alert = _driver.SwitchTo().Alert();
            var text = alert.Text;
            alert.Accept();
            return text;
        }
    }
}


