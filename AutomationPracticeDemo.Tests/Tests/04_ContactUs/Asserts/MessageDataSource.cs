using AutomationPracticeDemo.Tests.Tests._01_SingUp.Asserts;


namespace AutomationPracticeDemo.Tests.Tests._04_ContactUs.Asserts
{
    public class MessageDataSource
    {
        private const string nameJson = "DataMessage.json";

        /// <summary>
        /// Metodo que nos permite obtener la información del mensaje desde el archivo Json y nos permite separar los casos de prueba
        /// <returns></returns>
        public static IEnumerable<TestCaseData> MessageInformation()
        {
            var listaMessageInfo = JsonHelper.LoadListFromJson<MessageInfo>(nameJson);

            foreach (var item in listaMessageInfo)
            {
                yield return new TestCaseData(item.Name,item.Email,item.Subject,item.Message);
            }
        }
    }
}
