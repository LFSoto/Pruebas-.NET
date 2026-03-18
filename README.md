# Proyecto de Pruebas Automatizadas - Automation Practice Demo - Practica #4 - Francinni Portuguez Castro.

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Exercise](https://automationexercise.com).
Para este entregable se han implementado 5 casos de prueba que cubren diferentes funcionalidades del sitio, como el registro de usuarios, inicio de sesión, agregar productos al carrito, completar el formulario de contacto y suscripción al newsletter.
Se realiza el uso de POM. Además de DDT para algunos casos de prueba, utilizando un archivo JSON para proporcionar diferentes datos de entrada.

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
•	/Pages/ - clases que representan las páginas del sitio, con métodos para interactuar con los elementos de la página.
•	/Resource/ - archivos de recursos, como archivos JSON para datos de prueba.
•	/Screenshots/ — capturas de pantalla generadas por las pruebas, organizadas nombre y fecha de ejecución.
•	/Tests/ - clases que contienen los casos de prueba, cada clase representa un flujo de prueba específico.
•	/Utils/ - clases utilitarias para funciones comunes, como generación de datos aleatorios, manejo de archivos, etc.

## Pruebas
Para resolver la práctica #4, se realizaron los siguientes flujos siguiendo el patrón Page Object Model (POM) y Data-Driven Testing (DDT) para el manejo de datos:
1. ***Caso1_RegistroUsuarioNuevo*** se realiza el registro de un usuario nuevo, se genera un email aleatorio para evitar conflictos, se utilizan datos desde un archivo .json, se valida que se muestre la opción de 'Logout' y se toma un screenshot del resultado.
1. ***Caso2_LoginTest*** esta prueba realiza el proceso de Login, se utilizan datos válidos e inválidos desde un archivo .json para validar el ingreso exitoso y fallido, se capturan screenshots del resultado ya sea exitoso o fallido.
3. ***Caso3_AgregarProductos*** esta prueba se agregan productos al carrito, se valida que total del carrito sea el correcto y se toma un screenshot del resultado.
4. ***Caso4_ContactUs*** esta prueba se completa el formulario de contacto, se utlizan datos tomados de un archivo .json, se valida que se muestre un mensaje de éxito y se captura un screenshot del resultado.
5. ***Caso5_SuscripcionAlNewsletter*** esta prueba realiza la suscripción al newsletter, se valida que se muestre un mensaje de éxito y se toma un screenshot del resultado.
