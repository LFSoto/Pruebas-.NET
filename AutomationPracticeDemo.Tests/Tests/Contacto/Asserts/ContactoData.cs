

namespace AutomationPracticeDemo.Tests.Tests.Contacto.Asserts
{
    public class ContactoData
    {
        public ContactoData(string nombre, string correo, string asunto, string mensaje, string archivo)
        {
            this.Nombre = nombre;
            this.Correo = correo;
            this.Asunto = asunto;
            this.Mensaje = mensaje;
            this.Archivo = archivo;
        }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Asunto { get; set; }

        public string Mensaje { get; set; }
        public string Archivo { get; set; }
    }
}
