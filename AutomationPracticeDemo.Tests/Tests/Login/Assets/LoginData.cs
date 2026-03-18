using AutomationPracticeDemo.Test.Utils;

namespace AutomationPracticeDemo.Test.Tests.Login.Asserts;

public class LoginData
{
    private string email;
    private string password;
    private string nombreUsuario;
    private bool isValid;

    public LoginData(string email, string password, string nombreUsuario, bool isValid)
    {
        this.email = email;
        this.password = password;
        this.isValid = isValid;
        this.nombreUsuario = nombreUsuario;
    }
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public bool IsValid
    {
        get { return isValid; }
        set { isValid = value; }
    }

    public string NombreUsuario
    {
        get { return nombreUsuario; }
        set { nombreUsuario = value; }
    }

    /// <summary>
    /// Carga una lista de objetos LoginData desde un archivo JSON usando JsonHelper.
    /// </summary>
    /// <param name="nombreArchivo">Nombre del archivo JSON</param>
    /// <returns>Lista de LoginData</returns>
    public static List<LoginData> LoadList(string nombreArchivo)
    {
        return JsonHelper.LoadListFromJson<LoginData>(nombreArchivo);
    }

}
