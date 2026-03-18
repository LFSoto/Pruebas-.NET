using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationPracticeDemo.Tests.Tests.RegistrarUsuario.Asserts;

namespace AutomationPracticeDemo.Tests.Tests._04_ContactUs.Asserts
{
		public static class ContactUsDataSource
		{
			private const string nameJson = "DataContactUs.json";

			/// <summary>
			/// Metodos que nos permite obtener los usuarios validos y no validos desde el archivo Json y nos permite separar los casos de prueba
			/// Se implementa el patron Yield Return para devolver los casos de prueba uno por uno
			/// ya que NUnit los consume de esa manera y se optimiza el uso de memoria
			/// </summary>
			/// <returns></returns>


			public static IEnumerable<TestCaseData> ContactUs()
			{
				var lista = JsonHelper.LoadListFromJson<ContactUsData>(nameJson);

				foreach (var item in lista)
				{
					yield return new TestCaseData(item.Name, item.Email, item.Subject, item.Message);
				}
		}
	}
}

