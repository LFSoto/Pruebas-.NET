namespace AutomationPracticeDemoTest.Utils;

public static class AutomationUtils
{
    /// <summary>
    /// Busca el directorio raíz del proyecto ascendiendo desde <c>AppContext.BaseDirectory</c>
    /// hasta encontrar un archivo <c>*.csproj</c>.
    /// </summary>
    /// <returns>Ruta completa del directorio del proyecto o <c>AppContext.BaseDirectory</c> si no se encuentra.</returns>
    public static string GetPathFromProject()
    {
        // Start from the test assembly base directory and walk up until a .csproj is found
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (dir.GetFiles("*.csproj", SearchOption.TopDirectoryOnly).Any())
                return dir.FullName;
            dir = dir.Parent;
        }

        // Fallback to the base directory if no project file was found
        return AppContext.BaseDirectory;
    }
}
