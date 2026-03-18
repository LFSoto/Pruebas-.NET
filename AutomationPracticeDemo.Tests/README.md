# Pruebas-.NET — Selenium POM + DDT

Proyecto de automatización de pruebas con **Selenium WebDriver** usando los patrones **Page Object Model (POM)** y **Data-Driven Testing (DDT)** sobre el sitio [automationexercise.com](https://automationexercise.com).

---

## Tecnologías utilizadas

- .NET 10
- Selenium WebDriver
- NUnit
- Page Object Model (POM)
- Data-Driven Testing (DDT)

---

## Requisitos previos

Antes de clonar y ejecutar el proyecto, asegúrate de tener instalado:

- [.NET 10 SDK](https://dotnet.microsoft.com/download) (versión 10.0.200 o superior)
- [Google Chrome](https://www.google.com/chrome/) (versión actualizada)
- Git

> Selenium Manager (incluido en Selenium 4+) descarga ChromeDriver automáticamente. No es necesario instalarlo por separado.

---

## Clonar el repositorio

```bash
# 1. Clonar el repositorio
git clone https://github.com/LFSoto/Pruebas-.NET.git

# 2. Entrar a la carpeta del proyecto
cd Pruebas-.NET

# 3. Cambiar a la rama del proyecto
git checkout KennethOviedo-SeleniumPOM-DDT
```

---

## Restaurar dependencias

Desde la carpeta raíz del proyecto ejecutar:

```bash
cd AutomationPracticeDemo.Tests
dotnet restore
```

---

## Ejecutar las pruebas

### Ejecutar todos los tests

```bash
cd AutomationPracticeDemo.Tests
dotnet test
```

### Ejecutar un test específico

```bash
dotnet test --filter "FullyQualifiedName~SignupNewUser"
```

### Ejecutar una clase completa

```bash
dotnet test --filter "FullyQualifiedName~SignupTest"
```

### Ejecutar con output detallado

```bash
dotnet test --logger "console;verbosity=detailed"
```

---

## Estructura del proyecto

```
Pruebas-.NET/
├── AutomationPracticeDemo.Tests/
│   ├── Pages/          # Page Objects (POM)
│   ├── Tests/          # Clases de prueba
│   ├── TestData/       # Datos para DDT
│   └── AutomationPracticeDemo.Tests.csproj
└── README.md
```

---

## Autor

**Kenneth Oviedo**  
Rama: `KennethOviedo-SeleniumPOM-DDT`