# Proyecto de Pruebas Automatizadas - Automation Practice Demo (POM + DDT)

Este repositorio contiene un framework de pruebas automatizadas en .NET 9 usando NUnit y Selenium WebDriver. Se aplica el patrón Page Object Model (POM) y Data-Driven Testing (DDT) usando archivos JSON.

Estructura principal del proyecto (AutomationPracticeDemo.Tests):

- /Pages
  - Objetos de página (HomePage, LoginPage, RegisterPage, ProductsPage, CartPage, ContactPage, FooterComponent)
- /Tests
  - Casos de prueba que consumen los POM y proveedores de datos
- /Utils
  - Helpers y utilidades (TestBase, ScreenshotHelper, JsonDataProvider, modelos de datos)
- /Test/Data
  - Archivos JSON con datasets para DDT (contactData.json, registerData.json, newsletterData.json)
- /Reportes
  - Capturas y artefactos generados por las pruebas

## Requisitos

- .NET 9 SDK
- Google Chrome instalado
- ChromeDriver disponible en la carpeta `Drivers` del proyecto o en PATH

## Ejecutar las pruebas

1. Abrir una terminal en la carpeta del proyecto de pruebas:

   cd AutomationPracticeDemo.Tests

2. Restaurar dependencias, compilar y ejecutar tests:

   dotnet restore
   dotnet build
   dotnet test

## Notas sobre diseño

- POM: la lógica de interacción con la UI está encapsulada en clases dentro de `/Pages`. Los tests consumen esas clases
