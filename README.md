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
