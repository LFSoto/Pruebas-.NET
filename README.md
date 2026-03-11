# Proyecto de Pruebas Automatizadas - Automation Practice Demo

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Testing Practice](https://automationexercise.com/login).

## Requisitos
- .NET 9 SDK
- Google Chrome
- ChromeDriver (ubicado en la carpeta `Drivers` dentro del proyecto)

## Instalacióngit clone https://github.com/LFSoto/Pruebas-.NET.git
cd Pruebas-.NET


## Para compilar y ejecutar las pruebas
cd AutomationPracticeDemo.Tests
dotnet restore
dotnet build
dotnet test
## Pruebas Incluidas
- Orden de ejecución: cada test tiene [Order(n)] para forzar la secuencia (registro primero, login después, etc.).
- Navega a https://automationexercise.com/.
- Espera explícita por el enlace "Signup / Login" y hace click.
- Espera los campos del formulario de signup; genera un email único usando el campo random de la clase (user_ + random) y lo escribe junto al nombre.
- Hace click en el botón de signup y espera la carga del formulario extendido.
- Valida que el texto mostrado contenga "Account Created".
- Click en Continue (con fallback JS click si es necesario).
- Espera que aparezca el elemento “Logged in as” y valida que el usuario aparezca logueado.
- Toma captura final. No cierra sesión aquí (se deja para EnsureLoggedOut en TearDown o para el test siguiente).


- Test 2: Login con usuario registrado
- Should_LoginExistingUser_AfterRegister()
- Navega directamente a /login para evitar overlays/modals.
- Espera explícita por los inputs del formulario de login (selectores data-qa: login-email y login-password).
- Rellena email y contraseña (credenciales puestas en variables existingEmail/existingPassword).
- Hace click en el botón de login (con fallback JS click si falla).
- Espera que aparezca el indicador “Logged in as” y valida su presencia con assert.
- Toma captura.
- Espera 30 segundos (requisito explícito del usuario) y después intenta cerrar la sesión:
- Busca enlace Logout y hace click (fallback a JS click).
- Espera que reaparezca el enlace "Signup / Login" confirmando logout.
- Maneja y registra excepciones durante logout.


- 3.Should_AddProductsToCart_AndVerifyTotal()
•	Navega a /products.
•	Espera que existan botones “Add to cart” y recoge hasta 5 botones disponibles.
•	Valida que haya al menos 2 botones; si no, falla el test.
•	Para el primer producto:
•	Intenta extraer el nombre del producto por XPath relativo (fallback a nombre genérico si falla).
•	Hace scroll al elemento y hace click en Add to cart.
•	Espera el modal de confirmación y hace click en “Continue Shopping” (si existe) para seguir comprando.
•	Para el segundo producto:
•	Igual que el primero, luego hace click en “View Cart” desde el modal.
•	En la página de carrito:
•	Espera filas de la tabla del carrito; valida que haya al menos 2 filas (productos).
•	Busca un elemento que contenga "Total" (o clases con 'total') y valida su existencia.
•	Toma captura del carrito.
•	Nota: los selectores son flexibles y pueden necesitar ajuste según el DOM real.


- 4.Should_SubmitContactUsForm()
•	Navega a /contact_us.
•	Espera y localiza los campos name, email, subject (si existe) y message.
•	Rellena los campos con datos generados (email único basado en random).
•	Crea un archivo temporal local (.txt) y lo adjunta en el input[type='file'] si está disponible; registra si no existe.
•	Busca el botón de envío (input[type='submit'] o botón con texto Submit) y lo pulsa (con fallback JS click).
•	Después de enviar, el test está preparado para manejar alertas inesperadas:
•	Si aparece un alert modal del navegador, TestBase.DismissAlertIfPresent() lo cierra para evitar UnhandledAlertException.
•	Existe lógica local (WaitUntilWithAlertHandling) que reintenta waits si aparece un alert.
•	Espera y valida la presencia del mensaje de éxito: texto que contiene "Success! Your details have been submitted successfully" (o fragmento "Success!").
•	Toma captura de la página de respuesta.
•	Borra el archivo temporal creado.


- 5.Should_SubscribeToNewsletter()
•	Navega a la home y hace scroll hasta el final de la página.
•	Intenta localizar el input de newsletter en el footer mediante varios selectores (footer input[type='email'], id o name comunes, placeholder).
•	Valida que el campo exista; genera un email único (con ticks) y lo escribe.
•	Busca el botón submit/arrow próximo al input (buscando elementos following:: o botones globales con texto Subscribe/Submit).
•	Hace click en el botón (con fallback JS click).
•	Espera y valida la presencia del mensaje: «You have been successfully subscribed» o texto que contenga «successfully subscribed».
•	Toma captura final.
