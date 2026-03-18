using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace AutomationPracticeDemo.Tests.Tests._04_ContactUs.Asserts
{
		public class ContactUsData
		{
			private string name;
			private string email;
			private string subject;
			private string message;

			public ContactUsData(string name, string email, string subject, string message)
			{
				this.name = name;
				this.email = email;
				this.subject = subject;
				this.message = message;
			}
			public string Name
			{
				get { return name; }
				set { name = value; }
			}

			public string Email
		{
				get { return email; }
				set { email = value; }
			}

			public string Subject
		{
				get { return subject; }
				set { subject = value; }
			}

			public string Message
		{
				get { return message; }
				set { message = value; }
			}

			/// <summary>
		/// Carga una lista de objetos ContactUsData desde un archivo JSON usando JsonHelper.
		/// </summary>
		/// <param name="nombreArchivo">Nombre del archivo JSON</param>
		/// <returns>Lista de ContactUsData</returns>
		public static List<ContactUsData> LoadList(string nombreArchivo)
			{
				Console.WriteLine("nombreArchivo " + nombreArchivo);
				return JsonHelper.LoadListFromJson<ContactUsData>(nombreArchivo);
			}

		}
	}
