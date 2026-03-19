using Newtonsoft.Json;

public static class JsonHelper
{
    private static string ResolveJsonPath(string nameFile)
    {
        if (string.IsNullOrWhiteSpace(nameFile))
            throw new ArgumentException("El nombre del archivo no puede estar vacío.", nameof(nameFile));

        // 1) Prefer a file copied to the test output folder (works best in CI)
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var candidates = new List<string>
        {
            Path.GetFullPath(Path.Combine(baseDir, "Resource", "DataTest", nameFile)),

            // 2) Fallbacks when running from bin/Debug|Release
            Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "Resource", "DataTest", nameFile)),
            Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "Resource", "DataTest", nameFile)),

            // 3) If the repo keeps Resource at solution root while tests are in a subfolder
            Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "..", "Resource", "DataTest", nameFile)),
        };

        // Also try resolving from current working directory (GitHub Actions runs from repo root)
        var cwd = Directory.GetCurrentDirectory();
        candidates.Add(Path.GetFullPath(Path.Combine(cwd, "Resource", "DataTest", nameFile)));

        // And from the test project folder if present
        candidates.Add(Path.GetFullPath(Path.Combine(cwd, "AutomationPracticeDemo.Tests", "Resource", "DataTest", nameFile)));

        foreach (var p in candidates.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            if (File.Exists(p))
                return p;
        }

        throw new FileNotFoundException($"No se encontró el archivo JSON '{nameFile}'. Rutas probadas:\n- {string.Join("\n- ", candidates)}");
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