# Calculadora - Fundamentos de Pruebas en .NET

Este proyecto es una base para practicar **pruebas unitarias en .NET** usando **NUnit** y **Moq**.  
Incluye una librería (`CalculadoraLib`) y un proyecto de pruebas (`CalculadoraTests`).  

---

## Requisitos

- [Visual Studio 2022](https://visualstudio.microsoft.com/es/)
- [.NET 6 SDK o superior](https://dotnet.microsoft.com/en-us/download/dotnet)  
- Git instalado en el sistema  

---

## Pasos para empezar

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/usuario/demo-calculadora.git
   cd demo-calculadora

2. **Restaurar dependencias**
	```bash
   dotnet restore

3. **Compilar la solución**
	```bash
   dotnet build

4. **Ejecutar las pruebas unitarias**
	```bash
   dotnet test

  ## Comandos útiles

1. Compilar:
	```bash
	dotnet build

2. Ejecutar pruebas:
	```bash
	dotnet test

3. Agregar nuevos paquetes (ejemplo: Moq):
	```bash
	dotnet add CalculadoraTests package Moq

---

# Reporte de pruebas y uso de Moq

Breve descripción de las pruebas implementadas en `CalculadoraTests` y uso de Moq para aislar dependencias.

- `CalculadoraUnitTests`:
  - Pruebas de `Sumar`, `Restar`, `Multiplicar`, `Dividir` y división por cero.
  - Se inyecta un `Mock<ITimeProvider>` en `SetUp` para devolver una hora válida (ej. 10:00) y evitar que la validación de horario impida las operaciones.

- `CalculadoraServiceTests`:
  - Pruebas que comprueban comportamiento dentro y fuera del horario mediante `Mock<ITimeProvider>` configurado con horas válidas e inválidas.

Uso de Moq

- Crear mocks: `new Mock<T>()` para `ITimeProvider`.
- Configurar comportamiento con `Setup`, por ejemplo:
  - `_timeProviderMock.Setup(tp => tp.Now).Returns(new DateTime(2026, 2, 19, 10, 0, 0));`


Ejecución de pruebas

- Desde la raíz del repositorio: `dotnet test`

Notas

- Proyecto dirigido a .NET 8 y C# 12.
- Las pruebas controlan dependencias externas mediante Moq para garantizar resultados deterministas y permitir verificaciones de interacción.
