# Proyecto de Pruebas Automatizadas - Automation Practice Demo - Practica #2 - Francinni Portuguez Castro.

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Testing Practice](https://testautomationpractice.blogspot.com/).

## Requisitos
- .NET 9 SDK
- Google Chrome
- ChromeDriver (instalado automáticamente por NuGet)

## Instalación
```bash
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd AutomationPracticeDemo.Tests
dotnet restore
```

## Pruebas
Para resolver la práctica #2, se realizaron las siguientes pruebas:
1. ***Should_FillandSubmitSection1*** con esta prueba se completa un campo de texto y posteriormente se presiona un botón, como resultados esperados se valida que se haya ingresado el texto en el campo y que el borde del botón cambie de estilo al ser presionado.
2. ***Should_SelectCheckBoxMonday*** esta prueba selecciona un checkbox y valida que el checkbox seleccionado sea el del día lunes.
3. ***Should_SelectGender*** esta prueba selecciona el radiobutton y valida que se seleccione el radiobutton del género femenino.
4. ***Should_SelectDatepicker*** esta prueba selecciona una fecha y valida que la fecha seleccionada en el calendario sea la correcta.

**Para cada una de las pruebas mencionadas anteriormente se toma un screenshot que se almacena en la carpeta 'screenshots'.**
