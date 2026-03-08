# Documentación de la clase `FromTestclase3`

Archivo: `AutomationPracticeDemo.Tests/Tests/FromTestclase3.cs`

Descripción
- Clase de pruebas basada en NUnit y Selenium WebDriver que realiza end-to-end sobre https://automationexercise.com/.
- Cada método marcado con `[Test]` es un caso de prueba independiente ejecutable vía `dotnet test`.

Requisitos
- .NET 9 SDK
- Google Chrome (versión estable)
- ChromeDriver compatible (puede obtenerse vía NuGet: `Selenium.WebDriver.ChromeDriver` o gestionado por Selenium Manager)
- Paquetes NuGet recomendados en el proyecto de tests:
  - `Selenium.WebDriver`
  - `Selenium.WebDriver.ChromeDriver`
  - `NUnit`, `NUnit3TestAdapter`, `Microsoft.NET.Test.Sdk`
  - Opcional: `DotNetSeleniumExtras.WaitHelpers` (si decide usar `ExpectedConditions`)

Cómo ejecutar solo las pruebas de esta clase
- Desde la raíz del repositorio:

```bash
# Ejecuta únicamente los tests de la clase FromTestclase3
dotnet test --filter FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.FromTestclase3
```

Notas sobre `Setup()`
- `ChromeOptions` se configura en `Setup()`. Para ejecución sin UI active/descomente la opción `--headless=new`.
- `Driver` se crea con `new ChromeDriver(options)` y navega a `https://automationexercise.com/`.

Resumen de tests y consideraciones
- `registro` — Rellena formulario de registro con un email aleatorio (`PracticaClase3{n}@cenfotec.com`), selecciona `country` vía `SelectElement` y valida el mensaje `Account Created!`.
  - Atención: si la UI cambia, los `data-qa` XPaths pueden necesitar actualización.

- `userExist` — Valida inicio de sesión con credenciales fijas y presencia del texto `Logged in as`.

- `ProductCar` — Añade `Blue Top` y `Winter Top` al carrito, usa `WebDriverWait` con expresiones lambda para esperar elementos, valida que `total == precio * cantidad` por artículo.
  - Compruebe formatos de precios antes de parsear (`Replace("Rs.", "")`).

- `ContactUsform` — Rellena formulario de contacto, adjunta archivo. La ruta usada es relativa:
  - `Path.GetFullPath(@"..\..\..\adjunto\prueba.png")` — asegúrese de que el archivo exista en esa ruta relativa al directorio de trabajo del test.
  - Acepta la alerta y valida el mensaje de éxito.

- `Suscripcion` — Hace scroll al footer, ingresa email en `id="susbscribe_email"` y valida el mensaje en `id="success-subscribe"`.

Problemas comunes y soluciones rápidas
- Error `SeleniumExtras` no encontrado: instale `DotNetSeleniumExtras.WaitHelpers` o reemplace `ExpectedConditions` por lambdas con `WebDriverWait.Until(drv => ...)`.
- `NoSuchElementException` o fallos intermitentes: aumentar tiempo de espera, usar selectores más robustos o ejecutar en modo no-headless para depurar.
- `chromedriver` incompatible: actualice Chrome o descargue la versión de chromedriver correspondiente.

Recomendaciones
- Mantenga los selectores (XPaths/CSS) en una clase de Page Objects para facilitar mantenimiento.
- Añada logs o capturas en fallo (screenshot) en `TearDown()` para diagnóstico.

Si desea, creo un `README.md` dentro de la carpeta `AutomationPracticeDemo.Tests/Tests` (archivo actual) o agrego scripts `run-tests.ps1`/`run-tests.sh` para ejecutar solo esta clase.   