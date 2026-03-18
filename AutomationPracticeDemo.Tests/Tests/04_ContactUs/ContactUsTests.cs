using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.ContactUsData;
using AutomationPracticeDemo.Tests.Pages;
using Newtonsoft.Json;

namespace AutomationPracticeDemo.Tests.Tests
{
    public class ContactUsTests : Base.TestBase
    {
        // Método que lee el JSON y devuelve los casos
        public static IEnumerable<ContactMessage> GetMessages()
        {
            string rutaJson = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resource\DataTest\DataMessage.json");
            var jsonContent = File.ReadAllText(rutaJson);
            return JsonConvert.DeserializeObject<List<ContactMessage>>(jsonContent);
        }

        [Test, TestCaseSource(nameof(GetMessages))]
        public void ContactUsForm(ContactMessage msg)
        {
            var contactPage = new ContactUsPage(Driver, Wait);
            contactPage.GoTo();

            string rutaImagen = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Resource\Paisaje123.jpg"));

            contactPage.FillForm(msg.Name, msg.Email, msg.Subject, msg.Message, rutaImagen);
            contactPage.Submit();

            Assert.That(contactPage.IsSuccess(), Is.True);
        }
    }
}
