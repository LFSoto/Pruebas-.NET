# Proyecto de Pruebas Automatizadas - Automation Practice Demo

Este repositorio contiene pruebas automáticas en C# (.NET 9) con NUnit y Selenium dirigidas a `https://automationexercise.com/`.

Este README explica cómo clonar el repositorio, instalar dependencias, configurar ChromeDriver y ejecutar los tests que actualmente contiene el proyecto.

## Requisitos previos

- Git
- .NET 9 SDK (descargar desde https://dotnet.microsoft.com)
- Google Chrome instalado
- Opcional: Visual Studio o VS Code

Nota sobre ChromeDriver: puede usar el paquete NuGet `Selenium.WebDriver.ChromeDriver`, Selenium Manager (Selenium 4.6+) o administrar `chromedriver` manualmente. Asegúrese de que la versión de chromedriver coincida con la versión de Chrome.

## Clonar el repositorio

```bash
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd "Pruebas-.NET"
# Cambiar a la rama usada en este entorno (opcional)
git checkout GustavoMontero-SeleniumClase3
```

## Restaurar dependencias y compilar

```bash
dotnet restore
dotnet build
```

### Añadir paquetes NuGet recomendados

Si el proyecto no incluye explícitamente los paquetes de Selenium y NUnit, agregue los siguientes (ajuste la ruta al `.csproj` si es necesario):

```bash
dotnet add AutomationPracticeDemo.Tests/AutomationPracticeDemo.Tests.csproj package Selenium.WebDriver
dotnet add AutomationPracticeDemo.Tests/AutomationPracticeDemo.Tests.csproj package Selenium.WebDriver.ChromeDriver
dotnet add AutomationPracticeDemo.Tests/AutomationPracticeDemo.Tests.csproj package NUnit
dotnet add AutomationPracticeDemo.Tests/AutomationPracticeDemo.Tests.csproj package NUnit3TestAdapter
dotnet add AutomationPracticeDemo.Tests/AutomationPracticeDemo.Tests.csproj package Microsoft.NET.Test.Sdk
```

Opcional (sólo si su código usa `ExpectedConditions`):

```bash
dotnet add AutomationPracticeDemo.Tests/AutomationPracticeDemo.Tests.csproj package DotNetSeleniumExtras.WaitHelpers
```

> Nota: el código en `FromTestclase3.cs` puede utilizar esperas lambda con `WebDriverWait.Until(drv => ...)`, lo que evita la necesidad de `DotNetSeleniumExtras.WaitHelpers`.

## Ejecutar las pruebas

Ejecutar todas las pruebas del proyecto:

```bash
dotnet test
```

Ejecutar una prueba concreta por nombre (ejemplo):

```bash
dotnet test --filter FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.FromTestclase3.registro
```

O filtrar por clase:

```bash
dotnet test --filter FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.FromTestclase3
```

## Tests existentes (archivo: `AutomationPracticeDemo.Tests/Tests/FromTestclase3.cs`)

Actualmente la clase `FromTestclase3` contiene los siguientes métodos de prueba:

- `registro` — Registra un nuevo usuario y valida que aparezca "Account Created!" y el botón "Logout".
- `userExist` — Inicia sesión con credenciales existentes y valida el mensaje "Logged in as".
- `ProductCar` — Añade dos productos al carrito ("Blue Top" y "Winter Top"), abre el carrito y valida que los totales por artículo sean correctos.
- `ContactUsform` — Envía el formulario de contacto, carga un archivo y valida el mensaje de éxito.
- `Suscripcion` — Se desplaza al pie de página, suscribe un correo y valida el mensaje de confirmación.

Revise `AutomationPracticeDemo.Tests/Tests/FromTestclase3.cs` para comprobar selectores, rutas de archivos (para cargas) y las opciones de `ChromeOptions` (por ejemplo `--headless=new`).

## Configuración de subida de archivos

El test `ContactUsform` carga un archivo desde la ruta relativa `..\\..\\..\\adjunto\\prueba.png`. Asegúrese de que ese archivo exista en la ruta relativa al directorio de trabajo del test, o modifique la ruta en el test.

## Ejecución en modo headless

Para CI o ejecución sin UI, active la opción `--headless=new` en `FromTestclase3.Setup()` o controle la opción mediante una variable de entorno.

## Integración Continua (ejemplo GitHub Actions)

Archivo de ejemplo `.github/workflows/dotnet-test.yml`:

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
        run: dotnet test --logger trx
```

## Problemas comunes y soluciones

- Error `SeleniumExtras` no encontrado: instale `DotNetSeleniumExtras.WaitHelpers` o reemplace `ExpectedConditions` por expresiones lambda con `WebDriverWait.Until(drv => ...)`.
- Error `chromedriver` incompatible: actualice Chrome o descargue la versión adecuada de `chromedriver`.
- `NoSuchElementException` o fallos intermitentes: aumente tiempos de espera explícitos (`WebDriverWait`) o mejore selectores.

## Notas finales

- Ajuste los comandos `dotnet add` a la ruta real del `.csproj` si la estructura del repo difiere.
- Si desea, puedo agregar scripts (`run-tests.ps1`, `run-tests.sh`) o un pipeline CI más detallado para su entorno.

---
Generado para la rama `GustavoMontero-SeleniumClase3` del repositorio `https://github.com/LFSoto/Pruebas-.NET`.
