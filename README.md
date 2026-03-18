Proyecto de Automatización – AutomationPracticeDemo.Tests

Descripción
Este proyecto implementa pruebas automatizadas sobre el sitio Automation Practice Demo, utilizando Selenium WebDriver, NUnit y el patrón Page Object Model (POM).
Se aplica Data-Driven Testing (DDT) con archivos JSON para manejar los datos de prueba.

Estructura del proyecto
AutomationPracticeDemo.Tests
│
├── Models
│   └── ContactUsData.cs         # Modelos para mapear datos del JSON
│   └── UserData.cs              # Modelos para mapear datos del JSON
├── Pages
│   ├── ContactUsPage.cs         # Page Object para contacto
│   ├── LoginPage.cs             # Page Object para login
│   ├── NewsletterPage.cs        # Page Object para newsletter
│   ├── ProductPage.cs           # Page Object para carrito
│   └── SignupPage.cs            # Page Object para registro
├── Resource
│   └── DataTest
│       ├── DataAccountInfo.json # Datos de usuarios para registro
│       ├── DataMessage.json     # Datos para Contact Us
│       └── DataUser.json        # Datos para Login
│   └── Paisaje123.jpg
├── Tests
│   ├── 01_SignUp
│       └── Asserts
│           ├── SignupAsserts.cs
│   │   └── RegistroUsuarioTests.cs
│   ├── 02_Login
│        └── Asserts
│           ├── DataSource.cs
│           ├── LoginAsserts.cs
│           ├── LoginData.cs
│   │   └── LoginUsuarioTests.cs
│   ├── 03_AddProductsCart
│        └── Asserts
│           ├── CartAsserts.cs
│   │   └── CarritoTests.cs
│   ├── 04_ContactUs
│        └── Asserts
│           ├── ContactUsAsserts.cs
│   │   └── ContactUsTests.cs
│   └── 05_Newsletter
│        └── Asserts
│           ├── NewsletterAsserts.cs
│       └── NewsletterTests.cs
├── Utils
│   ├── DataHelper.cs            # Helper para leer JSON
│   ├── JsonHelper.cs
│   ├── ScreenshotHelper.cs
│   └── TestBase.cs              # Configuración base de los tests

Tecnologías utilizadas
- C# .NET 9.0
- NUnit (framework de pruebas)
- Selenium WebDriver (automatización web)
- Newtonsoft.Json (lectura de datos JSON)

Escenarios de prueba implementados
- Registro de usuario nuevo: Flujo completo de creación de cuenta usando datos de DataAccountInfo.json.
- Login de usuario existente: Validación de inicio de sesión con datos de DataUser.json.
- Agregar productos al carrito: Validación de flujo de compra.
- Formulario de contacto: Envío de mensajes usando DataMessage.json.
- Suscripción al newsletter: Validación de suscripción exitosa.

Instrucciones de instalación y ejecución
🔹 Clonar el repositorio
    git clone https://github.com/tu-usuario/AutomationPracticeDemo.Tests.git
    cd AutomationPracticeDemo.Tests
🔹 Restaurar dependencia
    dotnet restore
    Esto instalará todos los paquetes NuGet necesarios (NUnit, Selenium, Newtonsoft.Json, Moq).
🔹 Ejecutar las pruebas desde la consola
    dotnet test
    Para ejecutar solo un test específico
    dotnet test --filter "FullyQualifiedName~AutomationPracticeDemo.Tests.Tests.RegistroUsuarioTests.RegistroUsuarioNuevo"
    Para ver resultados detallados:
    dotnet test -v n

Ejecutar las pruebas desde Visual Studio
- Abre la solución en Visual Studio.
- Menú Prueba → Ejecutar todas las pruebas.
- O abre el Explorador de pruebas (Prueba → Ventanas → Explorador de pruebas) y ejecuta el escenario que quieras.
- Los resultados aparecerán en la ventana de Explorador de pruebas con estado: ✔️ Correcto o ❌ Fallido.

Evidencias
- Si un test falla, el ScreenshotHelper genera capturas en la carpeta configurada.
- Esto permite documentar errores y adjuntar evidencia en la entrega.

Autor

- Dayana Porras Monestel
