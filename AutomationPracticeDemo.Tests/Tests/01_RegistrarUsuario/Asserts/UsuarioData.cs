

using System.Text.Json.Serialization;

namespace AutomationPracticeDemo.Tests.Tests.RegistrarUsuario.Asserts
{
	public class UsuarioData
	{
		private string name;
		private string password;
		private string firstName;
		private string lastName;
		private string address;
		private string country;
		private string state;
		private string city;
		private string zipCode;
		private string mobileNumber;

		public UsuarioData(string name,  string password,  string firstName,string lastName, string address, string country, string state, string city, string zipCode, string mobileNumber)
		{
			this.name = name;
			this.password = password;
			this.firstName = firstName;
			this.lastName = lastName;
			this.address = address;
			this.country = country;
			this.state = state;
			this.city = city;
			this.zipCode = zipCode;
			this.mobileNumber = mobileNumber;
		}
		/*
		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("password")]
		public string Password { get; set; }

		[JsonPropertyName("firstName")]
		public string FirstName { get; set; }

		[JsonPropertyName("lastName")]
		public string LastName { get; set; }

		[JsonPropertyName("address")]
		public string Address { get; set; }

		[JsonPropertyName("country")]
		public string Country { get; set; }

		[JsonPropertyName("state")]
		public string State { get; set; }

		[JsonPropertyName("city")]
		public string City { get; set; }

		[JsonPropertyName("zipcode")]
		public string Zipcode { get; set; }

		[JsonPropertyName("mobileNumber")]
		public string MobileNumber { get; set; }*/

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		public string Address
		{
			get { return address; }
			set { address = value; }
		}

		public string Country
		{
			get { return country; }
			set { country = value; }
		}

		public string State
		{
			get { return state; }
			set { state = value; }
		}

		public string City
		{
			get { return city; }
			set { city = value; }
		}

		public string Zipcode
		{
			get { return zipCode; }
			set { zipCode = value; }
		}

		public string MobileNumber
		{
			get { return mobileNumber; }
			set { mobileNumber = value; }
		}
		/// <summary>
		/// Carga una lista de objetos UsuarioData desde un archivo JSON usando JsonHelper.
		/// </summary>
		/// <param name="nombreArchivo">Nombre del archivo JSON</param>
		/// <returns>Lista de UsuarioData</returns>
		public static List<UsuarioData> LoadList(string nombreArchivo)
		{
			Console.WriteLine("nombreArchivo " + nombreArchivo);
			return JsonHelper.LoadListFromJson<UsuarioData>(nombreArchivo);
		}

	}
}