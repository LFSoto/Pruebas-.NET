using AutomationPracticeDemo.Tests.Tests.contacUs4.Asserts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPracticeDemo.Tests.Tests.Suscription5.Asserts
{
    public class SuscriptionDataSource
    {
        private const string nameJson = "DataSucription.json";

        /// <summary>
        /// Metodo que nos permite obtener la información del mensaje desde el archivo Json y nos permite separar los casos de prueba
        /// <returns></returns>
        public static IEnumerable<TestCaseData> SuscriptionInfo()
        {
            var listaMessageInfo = JsonHelper.LoadListFromJson<MessageInfo>(nameJson);

            foreach (var item in listaMessageInfo)
            {
                yield return new TestCaseData(item.Email);
            }
        }
    }
}
