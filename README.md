# Proyecto de Pruebas Automatizadas - Automation Practice Demo

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Testing Practice](https://testautomationpractice.blogspot.com/).

## Requisitos
- .NET 9 SDK
- Google Chrome
- ChromeDriver (ubicado en la carpeta `Drivers` dentro del proyecto)

## Instalacióngit clone https://github.com/LFSoto/Pruebas-.NET.git
cd Pruebas-.NET

## Para cambiar a la rama específica del proyecto
git checkout ErickMeneses-SeleniumT2

## Para compilar y ejecutar las pruebas
cd AutomationPracticeDemo.Tests
dotnet restore
dotnet build
dotnet test
## Pruebas Incluidas
- Contiene pruebas que llenan y envían un formulario,
- Verifican los checkboxes de los días de la semana,
- Prueban la selección de país en un dropdown,
- Validan los datepickers, usando texto y usando el calendario.
- Comprueban mensajes de alerta.
