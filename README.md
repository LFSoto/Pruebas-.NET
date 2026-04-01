# Práctica:Primeros Pasos - Selenium

[![.NET 9 Selenium Tests](https://github.com/LFSoto/SOFT-740-Fundamentos/actions/workflows/dotnet-tests.yml/badge.svg?branch=MaximinoBejarano-SeleniumGIT)](https://github.com/LFSoto/SOFT-740-Fundamentos/actions/workflows/dotnet-tests.yml)
# Clonar una rama desde GitHub
1. Ve al repositorio en GitHub https://github.com/LFSoto/SOFT-740-Fundamentos.git.
2. Haz clic en el botón **Code**, luego a la izquierda de la rama actual selecciona la lista de ramas **branches**.
3. Haz click en el botón **New branch** y se muestra la ventana modal "Create a branch".
4. En el campo de texto "New branch name" escribe el nombre de la rama.
5. selecciona la rama base en el campo **Source**
6. Crea la nueva rama con el botón "Create new branch".

## Cambiar a una rama distinta desde Visual Studio (UI Git)

Para cambiar de rama usando la interfaz gráfica de Visual Studio:

1. Abre el proyecto en Visual Studio.
2. Ve al panel **Cambios de GIT** (puedes acceder desde el menú **Ver > Cambios de GIT**).
3. En la parte superior del panel, haz clic en el nombre de la rama actual (por ejemplo, `main`).
4. Se abrirá una lista de ramas disponibles en el repositorio.
5. Selecciona la rama a la que deseas cambiar (por ejemplo, `SeleniumTema2` etc.).
6. Visual Studio cambiará automáticamente a esa rama.
7. Si hay cambios remotos en esa rama, haz clic en el botón **fetch`** para descargar las actualizaciones del repositorio remoto.
8. Finalmente ejecuta en la terminalel comando dotnet restore

## Instalación de dependencias en Visual Studio (UI de NuGet)
1. Abre el proyecto en Visual Studio.
2. En el Explorador de soluciones, haz clic derecho sobre el proyecto y selecciona Administrar paquetes NuGet.
3. En la pestaña Examinar, busca e instala los paquetes necesario, ejemplo: NUnit.
4. Confirma que los paquetes aparecen en la pestaña Instalado

## Instalación de dependencias de Selenium desde la terminal
Si prefieres instalar las dependencias de Selenium usando la terminal de Visual Studio, sigue estos pasos:

1. Abre Visual Studio y carga el proyecto.
2. Ve a la terminal de powershell.
3. Ejecuta los siguientes comandos en la consola:
	dotnet add package Selenium.WebDriver
	dotnet add package Selenium.WebDriverManager

## Ejecutar las pruebas
1. Abre el panel Explorador de pruebas desde el menú Prueba > Ver > Explorador de pruebas.
2. Haz clic en Ejecutar todas las pruebas o selecciona pruebas individuales.
3. Visualiza los resultados directamente en el panel.

## Instalación de Newtonsoft.Json

Puedes instalar Newtonsoft.Json de dos maneras:

### Opción 1: Usando NuGet Package Manager

1. Abre tu proyecto en Visual Studio.
2. Ve a **Tools > NuGet Package Manager > Manage NuGet Packages for Solution**.
3. Busca `Newtonsoft.Json`.
4. Selecciona tu proyecto y haz clic en **Install**.

### Opción 2: Usando la consola de NuGet

Abre la consola de NuGet (**Tools > NuGet Package Manager > Package Manager Console**) y ejecuta:

```bash
Install-Package Newtonsoft.Json


## Se mantien la información agregada por el profesor

# Proyecto de Pruebas Automatizadas - Automation Practice Demo

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Testing Practice](https://testautomationpractice.blogspot.com/).

## Requisitos
- .NET 8 SDK
- Google Chrome
- ChromeDriver (instalado automáticamente por NuGet)

## Instalación
```bash
git clone https://github.com/LFSoto/SOFT-740-Fundamentos.git
cd AutomationPracticeDemo.Tests
dotnet restore
