

namespace AutomationPracticeDemo.Tests.Tests.Registro.Asserts
{
    public class RegistroData
    {
        public RegistroData(string nombre, string correo, string contrasena, string primerNombre, string apellido, string direccion, 
            string pais, string estado, string ciudad, string codigoZip, string numeroTelefono){
            this.Nombre = nombre;
            this.Correo = correo;
            this.Contrasena = contrasena;
            this.PrimerNombre = primerNombre;
            this.Apellido = apellido;  
            this.Direccion = direccion;
            this.Pais = pais;
            this.Estado = estado;
            this.Ciudad = ciudad;
            this.CodigoZip  = codigoZip;
            this.NumeroTelefono = numeroTelefono;
        }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string PrimerNombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string CodigoZip { get; set; }
        public string NumeroTelefono { get; set; }

        /// <summary>
        /// Carga una lista de objetos LoginData desde un archivo JSON usando JsonHelper.
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo JSON</param>
        /// <returns>Lista de LoginData</returns>
        public static List<RegistroData> LoadList(string nombreArchivo)
        {
            return JsonHelper.LoadListFromJson<RegistroData>(nombreArchivo);
        }

    }
}
