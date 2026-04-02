# AutomationPracticeDemo.Tests (Reqnroll)

Este repositorio contiene las pruebas automatizadas (NUnit + Selenium) enfocadas en la integración con Reqnroll.

Estado del proyecto
- Target framework: .NET 9
- Workflow GitHub Actions incluido: `.github/workflows/dotnet-tests.yml` (ejecuta build y tests en .NET 9).

Branch `Reqnroll`

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
