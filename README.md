# Proyecto de Pruebas Automatizadas - Automation Practice Demo

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Testing Practice](https://testautomationpractice.blogspot.com/).

## Requisitos
- .NET 8 SDK
- Google Chrome
- ChromeDriver (instalado automáticamente por NuGet)

## Instalación
```bash
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd Pruebas-.NET

## Para cambiar a la rama específica del proyecto
git checkout ErickMeneses-SeleniumClase3

## Para compilar y ejecutar las pruebas
cd AutomationPracticeDemo.Tests
dotnet restore
dotnet build
dotnet test
```
## Descripción del Proyecto

El proyecto se enfoca en la utilizacion de Selenium WebDriver
para automatizar pruebas funcionales en el sitio de Automation Testing Practice.
Se han implementado pruebas para validar la funcionalidad de búsqueda, navegación y otras interacciones comunes en el sitio.

## Pruebas Incluidas
Validacion del registro de usuario.
Validacion de agregar productos al carrito y su total.
Validacion de registro al newsletter.
Validacion del formulario de contacto.
Validacion del login con un usuario registrado.
