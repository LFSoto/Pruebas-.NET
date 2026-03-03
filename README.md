# Proyecto de Pruebas Automatizadas - Automation Practice Demo

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Testing Practice](https://testautomationpractice.blogspot.com/).

## Requisitos
- .NET 8 SDK
- Google Chrome
- ChromeDriver (instalado automáticamente por NuGet)

## Instalación
```bash
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd AutomationPracticeDemo.Tests
dotnet restore
```

# AutomationPracticeDemo.Tests

Instrucciones para clonar, instalar dependencias y ejecutar las pruebas automáticas.

## Requisitos

- Git
- .NET 9 SDK (instálalo desde https://dotnet.microsoft.com)
- Navegador instalado (por ejemplo, Google Chrome)
- WebDriver para el navegador (por ejemplo, Chromedriver) o usa Selenium Manager (Selenium 4.6+)


## Clonar el repositorio

```bash
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd Pruebas-.NET
# (Opcional) Cambiar a la rama usada en el proyecto
git checkout Semana-2/GustavoMontero
```

## Restaurar dependencias y compilar

```bash
dotnet restore
dotnet build
```

## Ejecutar las pruebas

Ejecutar todas las pruebas del repositorio:

```bash
dotnet test
```

Ejecutar una prueba específica (ejemplo):

```bash
dotnet test --filter "FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.FormTests.Should_FillAndSubmitForm"
```

## Notas y solución de problemas

- Las pruebas usan Selenium; asegúrate de tener el navegador y su WebDriver disponible en el `PATH`, o que Selenium Manager pueda gestionar el driver automáticamente.
- Si las pruebas fallan al inicializar el WebDriver, verifica la versión del navegador y la compatibilidad con el driver.
- Puedes abrir la solución en Visual Studio 2022/2023 o VS Code y ejecutar las pruebas desde el Test Explorer o con la extensión de .NET.

Si necesitas instrucciones específicas para tu sistema operativo (Windows/macOS/Linux) o información sobre cómo configurar un WebDriver concreto, indícalo y proporciono pasos detallados.
 
## Descripción de las pruebas incluidas

El proyecto contiene pruebas NUnit ubicadas en la clase `AutomationPracticeDemo.Tests.Tests.FormTests`.

- `Should_FillAndSubmitForm`: llena el formulario, verifica selección de género, checkboxes, selects y datepickers; envía el formulario y valida el mensaje en pantalla.
- `Should_Alertsimple`: dispara una alerta simple y valida su texto.
- `Should_AlertÇonfirmation`: dispara una alerta de confirmación y la acepta.
- `Should_AlertPrompt`: dispara una alerta tipo prompt, envía texto y valida el mensaje resultante.

Cómo ejecutar pruebas individuales:

```bash
# Ejecutar todas las pruebas
dotnet test

# Ejecutar una prueba concreta por nombre completo
dotnet test --filter "FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.FormTests.Should_FillAndSubmitForm"

# Ejecutar todas las pruebas de la clase FormTests
dotnet test --filter "FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.FormTests"
```

También puedes ejecutar las pruebas desde el Test Explorer en Visual Studio o usar extensiones de .NET en VS Code.
