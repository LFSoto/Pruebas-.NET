

using System.Xml.Linq;
using AutomationPracticeDemo.Tests.Tests.RegistrarUsuario.Asserts;

namespace AutomationPracticeDemo.Tests.Tests.RegistrarUsuario.Asserts
{
	public static class UsuarioDataSource
	{
		private const string nameJson = "DataAccountInfo.json";

		/// <summary>
		/// Metodos que nos permite obtener los usuarios validos y no validos desde el archivo Json y nos permite separar los casos de prueba
		/// Se implementa el patron Yield Return para devolver los casos de prueba uno por uno
		/// ya que NUnit los consume de esa manera y se optimiza el uso de memoria
		/// </summary>
		/// <returns></returns>

		
		public static IEnumerable<TestCaseData> RegisterUser()
		{
			var lista = JsonHelper.LoadListFromJson<UsuarioData>(nameJson);

			foreach (var item in lista)
			{
				Console.WriteLine(" item.Name " + item.Name);
				yield return new TestCaseData(item.Name, item.Password, item.FirstName, item.LastName, item.Address, item.Country, item.State, item.City, item.Zipcode, item.MobileNumber);
				
			}
		}
	}
}