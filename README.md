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

## Pruebas realizadas
1. Prueba elemento web RadioButton de selección de genero, captura de pantalla usando el método estándar de Selenium.
2. Prueba elemento web CheckBox de selección de días de la semana, captura de pantalla usando el método estándar de Selenium.
3. Prueba elemento web DatePicker selección de rango de fechas (inicio y fin) invocando el evento click del botón Submit para calculo de diferencia de días, captura de pantalla usando el método estándar de Selenium.
4. Prueba elemento web Alerts "prompt alert" invocando el evento click para abrir la alerta y verificar el texto "Please enter your name:", captura de pantalla utilizando una captura de pantalla completa por inpedimento al utilizar el metodo estandar de selenium al tener una alerta abierta.

