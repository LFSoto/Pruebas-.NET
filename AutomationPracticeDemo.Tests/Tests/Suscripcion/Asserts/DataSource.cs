

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AutomationPracticeDemo.Tests.Tests.Suscripcion.Asserts
{
    public static class SuscripcionDataSource
    {
        private const string nameJson = "DataSuscripcion.json";

        /// <summary>
        /// Metodos que nos permite obtener los usuarios validos y no validos desde el archivo Json y nos permite separar los casos de prueba
        /// Se implementa el patron Yield Return para devolver los casos de prueba uno por uno
        /// ya que NUnit los consume de esa manera y se optimiza el uso de memoria
        /// </summary>
        /// <returns></returns>
    
        public static IEnumerable<TestCaseData> TodosRegistros()
        {
             var lista = JsonHelper.LoadListFromJson<SuscripcionData>(nameJson);

            foreach (var item in lista)
            {
                yield return new TestCaseData(item);
            }
        }
    }
}
