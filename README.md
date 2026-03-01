# Práctica: Primeros Pasos - Selenium

Este proyecto contiene un esqueleto en .NET con NUnit y Selenium para practicar pruebas funcionales sobre el sitio [Automation Testing Practice](https://testautomationpractice.blogspot.com/).

Contenido
- Tests que interactúan con elementos: text boxes, dropdown, buttons y alerts.
- Capturas generadas durante la ejecución se guardan en la carpeta `Screenshots`.

Requisitos
- .NET 9 SDK
- Chrome instalado

Instrucciones
1. Clonar el repositorio:
   git clone <URL-del-repositorio>
2. Cambiar a la rama de la práctica (opcional si ya existe):
   git checkout Semana-2/JuanRamirez
3. Restaurar paquetes y compilar:
   dotnet restore
   dotnet build
4. Ejecutar pruebas:
   dotnet test --logger:trx

Notas
- Las capturas se guardan en `bin/Debug/net9.0/Screenshots` (o en el directorio de trabajo según ejecución).
- Ajustar el driver de Chrome si es necesario (usar la versión adecuada del ChromeDriver).

Autor
- Juan Ramirez (rama: Semana-2/JuanRamirez)
