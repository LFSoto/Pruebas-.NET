using AutomationPracticeDemo.Tests.Pages;
using AutomationPracticeDemo.Tests.Utils;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using NUnit.Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace AutomationPracticeDemo.Tests.Tests
{
    public class FormTests : TestBase
    {

        /*[Test]
         public void Full_Form()
         {
             var formPage = new FormPage(Driver);
             formPage.FillForm("Juan Perez", "juan@test.com", "88888888", "Costa Rica");
             formPage.Submit();
             Assert.Pass("Formulario llenado y enviado."); 
    }*/

        [Test]
        public void Register_User()
        {
            var formPage = new FormPage(Driver);

        }
    }
}
