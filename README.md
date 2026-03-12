# Proyecto de Pruebas Automatizadas - Automation Practice Demo - Practica #4 - Francinni Portuguez Castro.

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Exercise](https://automationexercise.com).

## Requisitos
- .NET 9 SDK
- Google Chrome última versión (Versión 146.0.7680.72)
- ChromeDriver (instalado automáticamente por NuGet) -- Versión compatible con Chrome Versión 146.0.7680.72

## Instalación
```bash
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd AutomationPracticeDemo.Tests
dotnet restore
dotnet build
```

## Estructura del proyecto
•	Tests/ — Flujos de pruebas automatizadas utilizando NUnit y Selenium WebDriver. Se incluyen en una unica clase de pruebas ya que no se utiliza POM. 
•	Resource/ — archivo para adjuntar en caso de prueba: Caso4_ContactUsForm
•	Screenshots/ — capturas generadas por tests

## Pruebas
Para resolver la práctica #3, se realizaron los siguientes flujos:
1. ***Caso1_RegistroUsuarioNuevo*** con esta prueba se realiza el registro de un usuario nuevo, se genera un email aleatorio para evitar conflictos, se valida que se muestre la opción de 'Logout' y se toma un screenshot del resultado.
1. ***Caso2_LoginUsuarioExistente*** esta prueba realiza el proceso de Login con un usuario existente, y se valida que se muestre el nombre de dicho usuario, se captura un screenshot del resultado.
3. ***Caso3_AgregarProductosAlCarrito*** esta prueba se agregan productos al carrito, se valida que el número de productos en el carrito sea el correcto y se toma un screenshot del resultado.
4. ***Caso4_ContactUsForm*** esta prueba se completa el formulario de contacto, se valida que se muestre un mensaje de éxito y se captura un screenshot del resultado.
5. ***Caso5_SuscripcionAlNewsletter*** esta prueba realiza la suscripción al newsletter, se valida que se muestre un mensaje de éxito y se toma un screenshot del resultado.
