# AutomationPracticeDemo.Tests (Reqnroll)

Este repositorio contiene las pruebas automatizadas (NUnit + Selenium) enfocadas en la integración con Reqnroll.

Estado del proyecto
- Target framework: .NET 9
- Tests organizados con patrón Page Object y datos en `AutomationPracticeDemo.Tests/Resources/Data`.
- Workflow GitHub Actions incluido: `.github/workflows/dotnet-tests.yml` (ejecuta build y tests en .NET 9).

Branch `Reqnroll`
- Se creó localmente la rama `Reqnroll` con el snapshot del branch remoto `MaximinoBejarano-Reqnroll` (sin histórico original).
- Para obtener exactamente ese contenido en tu clon (si no existe localmente):
  1. Asegúrate de tener el remoto `fundamentos` apuntando a `https://github.com/LFSoto/SOFT-740-Fundamentos`.
  2. Ejecuta:
     ```bash
     git fetch fundamentos
     git checkout -B Reqnroll fundamentos/MaximinoBejarano-Reqnroll
     ```
  3. Para publicar la rama en `origin` (si corresponde):
     ```bash
     git push -u origin Reqnroll
     ```

Requisitos
- .NET 9 SDK instalado
- Google Chrome
- ChromeDriver (se obtiene vía NuGet package `Selenium.WebDriver.ChromeDriver` en el proyecto)

Instalación y ejecución
```bash
git clone <tu-fork-o-repo>
cd AutomationPracticeDemo.Tests
dotnet restore
dotnet build
dotnet test
```

Estructura relevante
- `AutomationPracticeDemo.Tests/Pages` - Page Objects
- `AutomationPracticeDemo.Tests/Resources/Data` - archivos JSON con datos de prueba
- `AutomationPracticeDemo.Tests/Tests` - tests organizados por característica
- `.github/workflows/dotnet-tests.yml` - pipeline CI para .NET 9

Notas
- El proyecto fue adaptado para leer datos desde `Resources/Data` y el .csproj copia esos archivos al output.
- Si quieres que publique la rama `Reqnroll` en `origin`, indícalo y lo hago.
