using Newtonsoft.Json;

public static class JsonHelper
{
    private static string? TryFindByCaseInsensitiveName(string directory, string fileName)
    {
        if (!Directory.Exists(directory))
            return null;

        var match = Directory
            .EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly)
            .FirstOrDefault(f => string.Equals(Path.GetFileName(f), fileName, StringComparison.OrdinalIgnoreCase));

        return match;
    }

    private static string ResolveJsonPath(string nameFile)
    {
        if (string.IsNullOrWhiteSpace(nameFile))
            throw new ArgumentException("El nombre del archivo no puede estar vacío.", nameof(nameFile));

        var baseDir = AppDomain.CurrentDomain.BaseDirectory;

        // candidate directories where Resource/DataTest may live
        var candidateDirs = new List<string>
        {
            Path.GetFullPath(Path.Combine(baseDir, "Resource", "DataTest")),
            Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "Resource", "DataTest")),
            Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "Resource", "DataTest")),
            Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "..", "Resource", "DataTest")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resource", "DataTest")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "AutomationPracticeDemo.Tests", "Resource", "DataTest")),
        };

        foreach (var dir in candidateDirs.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var exact = Path.Combine(dir, nameFile);
            if (File.Exists(exact))
                return Path.GetFullPath(exact);

            // Linux is case-sensitive; allow mismatched casing by searching the directory.
            var ci = TryFindByCaseInsensitiveName(dir, nameFile);
            if (ci != null)
                return Path.GetFullPath(ci);
        }

        throw new FileNotFoundException($"No se encontró el archivo JSON '{nameFile}'. Directorios probados:\n- {string.Join("\n- ", candidateDirs)}");
    }

    /// <summary>
    /// Carga y deserializa un archivo JSON en una lista de objetos del tipo especificado.
    /// </summary>
    /// <typeparam name="T">Clase destino para deserializar</typeparam>
    /// <param name="pathFile">nombre completo del archivo Json</param>
    /// <returns>Lista de objetos del tipo T</returns>
    public static List<T> LoadListFromJson<T>(string nameFile)
    {
        var pathJson = ResolveJsonPath(nameFile);

        var contenidoJson = File.ReadAllText(pathJson);
        var lista = JsonConvert.DeserializeObject<List<T>>(contenidoJson);

        return lista ?? new List<T>();
    }

    /// <summary>
    /// Carga y deserializa un archivo JSON en un solo objeto del tipo especificado.
    /// </summary>
    /// <typeparam name="T">Clase destino para deserializar</typeparam>
    /// <param  name="pathFile"> Nombre completo del archivo Json</param>
    /// <returns>Objeto del tipo T</returns>
    public static T LoadObjectFromJson<T>(string nameFile)
    {
        var pathJson = ResolveJsonPath(nameFile);

        var contenidoJson = File.ReadAllText(pathJson);
        var objeto = JsonConvert.DeserializeObject<T>(contenidoJson);

        if (objeto == null)
            throw new InvalidOperationException("No se pudo deserializar el contenido JSON.");

        return objeto;
    }
}