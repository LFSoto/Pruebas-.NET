

namespace AutomationPracticeDemo.Tests.Tests.Login.Asserts
{
    public static class LoginDataSource
    {
        private const string nameJson = "DataUser.json";

        /// <summary>
        /// Metodos que nos permite obtener los usuarios validos y no validos desde el archivo Json y nos permite separar los casos de prueba
        /// Se implementa el patron Yield Return para devolver los casos de prueba uno por uno
        /// ya que NUnit los consume de esa manera y se optimiza el uso de memoria
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestCaseData> UsersIsValid()
        {
              var lista = JsonHelper.LoadListFromJson<LoginData>(nameJson);

            foreach (var item in lista)
            {
                if (item.IsValid)
                {
                    yield return new TestCaseData(item.Email,item.Password);
                }
            }
        }

        public static IEnumerable<TestCaseData> UsersNotValid()
        {
          var lista = JsonHelper.LoadListFromJson<LoginData>(nameJson);

            foreach (var item in lista)
            {
                if (!item.IsValid)
                {
                    yield return new TestCaseData(item.Email, item.Password);
                }
            }
        }

        public static IEnumerable<TestCaseData> AllUsers()
        {
             var lista = JsonHelper.LoadListFromJson<LoginData>(nameJson);

            foreach (var item in lista)
            {
                yield return new TestCaseData(item)
                    .SetName($"LoginTest_{item.Email}_{(item.IsValid ? "Valido" : "Invalido")}");
            }
        }
    }
}
