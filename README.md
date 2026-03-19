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
git checkout ErickMeneses-SeleniumPOM-DDT

## Para compilar y ejecutar las pruebas
cd AutomationPracticeDemoTests
dotnet restore
dotnet build
dotnet test
```

## Descripción del Proyecto

- El proyecto se enfoca en la utilizacion de Selenium WebDriver
para automatizar pruebas funcionales en el sitio de Automation Testing Practice.
- Se han implementado pruebas para validar la funcionalidad de búsqueda, navegación y otras interacciones comunes en el sitio.
- Se ha impletando el uso de Page Object Model (POM) para mejorar la mantenibilidad y legibilidad del código de pruebas.
- Se ha implementado el uso de Data-Driven Testing (DDT) para ejecutar pruebas con diferentes conjuntos de datos,
aumentando la cobertura de las pruebas y la robustez del proyecto.

## Pruebas Incluidas
- Validacion del registro de usuario.
- Validacion de agregar productos al carrito y su total.
- Validacion de registro al newsletter.
- Validacion del formulario de contacto.
- Validacion del login con un usuario registrado.
- Validacion del login con un usuario no registrado.
