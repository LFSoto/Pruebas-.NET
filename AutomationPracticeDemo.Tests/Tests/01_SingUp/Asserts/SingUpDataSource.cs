
using AutomationPracticeDemo.Tests.Tests.Login.Asserts;

namespace AutomationPracticeDemo.Tests.Tests._01_SingUp.Asserts
{
    public class SingUpDataSource
    {
        private const string nameJson = "DataAccountInfo.json";

        /// <summary>
        /// Metodos que nos permite obtener la información de la cuenta desde el archivo Json y nos permite separar los casos de prueba
        /// <returns></returns>
        public static IEnumerable<TestCaseData> AccountInformation()
        {
            var listaAccountInfo = JsonHelper.LoadListFromJson<AccountData>(nameJson);
            
            foreach (var item in listaAccountInfo)
            {
                AccountInfo infoAccount = item.GetAccountInformation();
                yield return new TestCaseData(infoAccount.Name,item);
            }
        }

    }
}
