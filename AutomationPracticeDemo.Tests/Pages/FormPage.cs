using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace AutomationPracticeDemo.Tests.Pages;

public class FormPage
{
    private readonly IWebDriver _driver;

    public FormPage(IWebDriver driver)
    {
        _driver = driver;
    }

    private IWebElement NameInput => _driver.FindElement(By.Id("name"));
    private IWebElement EmailInput => _driver.FindElement(By.Id("email"));
    private IWebElement PhoneInput => _driver.FindElement(By.Id("phone"));
    private IWebElement CountryDropdown => _driver.FindElement(By.Id("country"));
    private IWebElement SubmitButton => _driver.FindElement(By.ClassName("submit-btn"));

    // Week Day Checkboxes
    private IWebElement CheckBoxSunday => _driver.FindElement(By.Id("sunday"));
    private IWebElement CheckBoxMonday => _driver.FindElement(By.Id("monday"));
    private IWebElement CheckBoxTuesday => _driver.FindElement(By.Id("tuesday"));
    private IWebElement CheckBoxWednesday => _driver.FindElement(By.Id("wednesday"));
    private IWebElement CheckBoxThursday => _driver.FindElement(By.Id("thursday"));
    private IWebElement CheckBoxFriday => _driver.FindElement(By.Id("friday"));
    private IWebElement CheckBoxSaturday => _driver.FindElement(By.Id("saturday"));

    // Dropdown County
    private IWebElement DropdownCountry => _driver.FindElement(By.Id("country"));

    // Date Picker 1
    private IWebElement DatePicker1 => _driver.FindElement(By.Id("datepicker"));

    // Date Picker 2
    private IWebElement LabelDatePicker2 => _driver.FindElement(By.XPath("//p[text()='Date Picker 2  (dd/mm/yyyy) : ']"));
    private IWebElement DatePicker2 => _driver.FindElement(By.Id("txtDate"));
    private IWebElement SelectDatePicker2Year => _driver.FindElement(By.ClassName("ui-datepicker-year"));
    private IWebElement SelectDatePicker2Month => _driver.FindElement(By.ClassName("ui-datepicker-month"));
    private IWebElement TdDatePicker2Day => _driver.FindElement(By.Id("txtDate"));

    // Data Picker 3 Rango

    private IWebElement DatePicker3 => _driver.FindElement(By.Id("start-date"));
    private IWebElement DatePicker4 => _driver.FindElement(By.Id("end-date"));

    // Alerts
    private IWebElement LabelSimpleAlert => _driver.FindElement(By.Id("alertBtn"));


    private IWebElement GetDayDataPicker2(string day)
    {
       return _driver.FindElement(By.XPath($"//table[@class='ui-datepicker-calendar']//a[@data-date='{day}']"));
    }


    public void FillForm(string name, string email, string phone, string country)
    {
        NameInput.SendKeys(name);
        EmailInput.SendKeys(email);
        PhoneInput.SendKeys(phone);
        CountryDropdown.SendKeys(country);
    }

    public void Submit()
    {
        SubmitButton.Click();
    }

    public void DoCheckOnAllDaysOfTheWeek()
    {
        CheckBoxSunday.Click();
        CheckBoxMonday.Click();
        CheckBoxTuesday.Click();
        CheckBoxWednesday.Click();
        CheckBoxThursday.Click();
        CheckBoxFriday.Click();
        CheckBoxSaturday.Click();
    }

    public bool IsAllDayOfTheWeekChecked()
    {
        return CheckBoxSunday.Selected &&
               CheckBoxMonday.Selected &&
               CheckBoxTuesday.Selected &&
               CheckBoxWednesday.Selected &&
               CheckBoxThursday.Selected &&
               CheckBoxFriday.Selected &&
               CheckBoxSaturday.Selected;
    }

    public void SelectCountry(string country)
    {
        // Selecciona el país del dropdown por texto visible.
        var select = new SelectElement(DropdownCountry);
        select.SelectByText(country);
    }

    public bool IsCountrySelected(string country)
    {
        var select = new SelectElement(DropdownCountry);
        return select.SelectedOption.Text.Equals(country, StringComparison.OrdinalIgnoreCase);
    }

    public void SelectDateInDatePickerOne(string date)
    {
        // Envía la fecha y luego un TAB para salir del campo (trigger de cambio si aplica)
        DatePicker1.SendKeys(date);
    }

    public string GetSelectedDateInDatePickerOne()
    {
        var selectedDate = DatePicker1.GetAttribute("value");
        Console.WriteLine($"Fecha seleccionada en DatePicker1: {selectedDate}");
        return selectedDate;
    }

    public void SelectDateInDatePickerTwo(string date)
    {
        // Valida que la fecha venga en el formato "dd/MM/yyyy " (ej: 31/12/2026)
        if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            throw new FormatException($"La fecha debe tener el formato 'dd/MM/yyyy ¡'. Valor recibido: '{date}'.");
        }

        // Descomponer la fecha en variables día, mes y año
        var day = parsedDate.Day;     // día (ej: 31)
        // Mes en formato abreviado (ej: "Mar", "Jan", "Jul")
        var month = parsedDate.ToString("MMM", CultureInfo.InvariantCulture);
        var year = parsedDate.Year;   // año (ej: 2026)

        // Accionar el datepicker (acción adicional puede ser necesaria según el control)

        DatePicker1.SendKeys(Keys.Tab);
        LabelDatePicker2.Click();
        DatePicker2.Click();
        //DatePicker2.Click();
        var selectYear = new SelectElement(SelectDatePicker2Year);
        selectYear.SelectByText(year.ToString());
        var selectMonth = new SelectElement(SelectDatePicker2Month);
        // Seleccionar el mes por texto abreviado (ej: "Mar", "Jan") según lo solicitado
        selectMonth.SelectByText(month);
        GetDayDataPicker2(day.ToString()).Click();

    }

    public string GetSelectedDateInDatePickerTwo()
    {
        var selectedDate = DatePicker2.GetAttribute("value");
        Console.WriteLine($"Fecha seleccionada en DatePicker2: {selectedDate}");
        return selectedDate;
    }

    public void ClickOnSimpleAlerLabel()
    {
        LabelSimpleAlert.Click();
    }

    public string GetSimpleAlertText()
    {
        var alert = _driver.SwitchTo().Alert();
        return alert.Text;
    }
    public void AcceptSimpleAlert()
    {
        var alert = _driver.SwitchTo().Alert();
        alert.Accept();
    }

    public bool IsAlertPresent()
    {
        try
        {
            _driver.SwitchTo().Alert();
            return true;
        }
        catch (NoAlertPresentException)
        {
            return false;
        }
    }
}
