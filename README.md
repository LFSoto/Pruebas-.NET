# Pruebas-.NET — Semana 4 (POM)

Este repositorio contiene varios ejercicios/proyectos. Para **Semana 4** se utiliza el proyecto `AutomationPracticeDemo.Tests` con **Selenium WebDriver + NUnit** aplicando **Page Object Model (POM)** y datos de prueba.

## Requisitos

- .NET SDK (recomendado **.NET 9**; el proyecto en este repo está orientado a .NET moderno)
- Google Chrome
- ChromeDriver (administrado por NuGet / Selenium Manager según los paquetes del proyecto)

## Clonar el repositorio

```bash
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd Pruebas-.NET
```

## Cambiar a la rama de Semana 4

```bash
git checkout Semana4-POM
```

## Restaurar dependencias, compilar y ejecutar pruebas

Desde la raíz del repo:

```bash
cd AutomationPracticeDemo.Tests
dotnet restore
dotnet build
dotnet test
```

## Estructura relevante

- `AutomationPracticeDemo.Tests/Pages/`: Page Objects (POM)
- `AutomationPracticeDemo.Tests/Tests/`: pruebas automatizadas (NUnit)
- `AutomationPracticeDemo.Tests/Resource/`: recursos y datos de prueba (JSON/imagenes si aplica)
- `AutomationPracticeDemo.Tests/Utils/`: utilidades (base de pruebas, screenshots, helpers)

## Ejecutar una prueba específica

```bash
dotnet test --filter "FullyQualifiedName~AutomationPracticeDemo.Tests" 
```

> Nota: también puedes ejecutar las pruebas desde Visual Studio usando el **Test Explorer**.
