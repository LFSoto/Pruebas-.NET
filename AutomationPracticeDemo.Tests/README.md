# AutomationPracticeDemo.Tests - Guía detallada de pruebas

Este documento contiene instrucciones detalladas para clonar el repositorio, instalar/restaurar dependencias y ejecutar/depurar las pruebas automatizadas incluidas en este proyecto.

Resumen del proyecto
- Proyecto de pruebas: `AutomationPracticeDemo.Tests` (C#, .NET 9) usando NUnit + Selenium.
- Objetivo: pruebas de interfaz web contra el sitio de ejemplo (tests de registro, login, formulario de contacto, carrito, etc.).

Estructura relevante
- Proyecto de tests: `AutomationPracticeDemo.Tests/AutomationPracticeDemo.Tests.csproj`
- Código de tests: `AutomationPracticeDemo.Tests/Tests/` (subcarpetas por área, p. ej. `registro1`)
  - Ejemplo: `AutomationPracticeDemo.Tests/Tests/registro1/ResgistroTest.cs`
  - Datos de prueba: `AutomationPracticeDemo.Tests/Resource/DataTest/DataAccountInfo.json`
  - Clases auxiliares / aserciones: `AutomationPracticeDemo.Tests/Tests/registro1/Asserts/*`
- Helpers / utilidades: `AutomationPracticeDemo.Tests/Utils/*` (p. ej. `JsonHelper`, `ScreenshotHelper`).

Requisitos previos
- .NET 9 SDK instalado: https://dotnet.microsoft.com
- Git
- Google Chrome (o navegador compatible con su driver de WebDriver)
- Visual Studio 2022/2026 (opcional, recomendado para depuración de tests)

Clonar el repositorio
```powershell
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd "Pruebas-.NET"
# (opcional) cambiar a la rama de trabajo
# git checkout GustavoMontero-POM-Clase4
```

Restaurar dependencias y compilar
```powershell
dotnet restore
dotnet build
```

Ejecutar las pruebas
- Ejecutar todas las pruebas del proyecto de tests:
```powershell
dotnet test AutomationPracticeDemo.Tests\AutomationPracticeDemo.Tests.csproj --logger "console;verbosity=normal"
```
- Ejecutar desde la raíz del repositorio (ejecuta todos los proyectos de test detectados):
```powershell
dotnet test
```

Ejecutar pruebas específicas
- Ejecutar una prueba por su Fully Qualified Name (ejemplo usando la clase `ResgistroTest`):
```powershell
dotnet test --filter FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.registro1.ResgistroTest.Registro
```
- Filtrar por clase (ejecuta todos los tests de la clase):
```powershell
dotnet test --filter FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.registro1.ResgistroTest
```

Cómo están construidos los tests (puntos clave)
- NUnit: los tests usan atributos de NUnit como `[Test]`, `[TestCase]`, `[TestCaseSource]` y `[SetUp]`.
- TestCaseSource y datos JSON: algunos tests usan `TestCaseSource` para leer datos desde `Resource/DataTest/DataAccountInfo.json` (clase `SingUpDataSource` en `Tests/registro1/Asserts`). Esto permite generar varias ejecuciones con distintos datos.
- Screenshots: la suite usa `ScreenshotHelper.TakeScreenshot(Driver, "Name.png")` en puntos clave; los ficheros se guardan en el directorio de trabajo del test.
- Chrome/Driver: el proyecto referencia `Selenium.WebDriver.ChromeDriver`; asegúrese de compatibilidad entre la versión de Chrome y el driver.

Depurar tests en Visual Studio
1. Abrir la solución/proyecto en Visual Studio.
2. Construir la solución (`Build > Rebuild Solution`).
3. Abrir Test Explorer (`Test > Test Explorer`).
4. Si los tests no aparecen, haga `Rebuild` y luego `Run All` en Test Explorer o ejecute `dotnet test` desde la consola para verificar errores de compilación.
5. Para depurar un test: en Test Explorer, clic derecho sobre el test y seleccionar `Debug`.

Problemas comunes y soluciones
- Test Explorer no muestra pruebas:
  - Verifique que `NUnit` sea versión 3.x y que `NUnit3TestAdapter` y `Microsoft.NET.Test.Sdk` estén restaurados.
  - Compruebe si la solución compila; si hay errores de compilación, los tests no se mostrarán.
  - Cierre/reabra Visual Studio si la detección de tests falla.
- Errores al ejecutar Selenium:
  - Revisar compatibilidad de `chromedriver` con Chrome; actualice el paquete `Selenium.WebDriver.ChromeDriver` o use Selenium Manager.
  - Para ejecución sin UI en CI, active `--headless=new` en `ChromeOptions`.
- Tests con datos externos (JSON): asegúrese de que la ruta relativa al archivo JSON sea correcta y que el archivo esté incluido en el repositorio.

Ejecución en headless / CI
- Para ejecutar sin UI (por ejemplo en pipeline): habilite las opciones headless en `ChromeOptions` dentro del setup de los tests.
- Ejemplo básico para GitHub Actions (archivo `.github/workflows/dotnet-test.yml`):
```yaml
name: .NET Tests
on: [push, pull_request]
jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '9.0.x'
      - name: Restore
        run: dotnet restore
      - name: Test
        run: dotnet test AutomationPracticeDemo.Tests/AutomationPracticeDemo.Tests.csproj --logger trx
```

Sugerencia: script para ejecutar tests localmente
- Puedo añadir un script `run-tests.ps1` que haga `dotnet restore`, `dotnet build` y `dotnet test`. ¿Desea que lo cree en `AutomationPracticeDemo.Tests\run-tests.ps1`?

Dónde mirar si algo falla
- Salida de `dotnet test` en consola (muestra errores de compilación o fallos de tests).
- Archivos de tests: `AutomationPracticeDemo.Tests/Tests/`.
- Datos de prueba: `AutomationPracticeDemo.Tests/Resource/DataTest/DataAccountInfo.json`.
- Logs o screenshots generados por la propia suite (si aplica).

Contacto y próximos pasos
- Si quiere, genero automáticamente `run-tests.ps1` y un ejemplo de workflow CI. También puedo añadir instrucciones concretas para depurar fallos habituales que obtenga al ejecutar `dotnet test`.
