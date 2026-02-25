# Calculadora - Práctica 1: Uso de Mock - Francinni Portuguez Castro.

Este proyecto es una base para practicar **pruebas unitarias en .NET** usando **NUnit** y **Moq**, contiene la solución creada para la práctica 1.  
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

## Solución:

Se crea la interfaz ITimeProvider para el uso de Mock con un método DateTime.
Es utilizado en cada una de las operaciones, generando una fecha y hora para simular dentro o fuera de horario.

---

## Pruebas implementadas:

Se creó casos de prueba para las 4 operaciones básicas de las cálculadora, prueba de división entre cero, pruebas de horario permitido y fuera de horario.
A continuación se detalla la lista de casos:

## CalculadoraServiceTest

- DividirDentroHorario_DeberiaSerExitoso
- DividirFueraHorario_DeberiaSerFallido
- MultiplicarDentroHorario_DeberiaSerExitoso
- MultiplicarFueraHorario_DeberiaSerFallido
- RestarDentroHorario_DeberiaSerExitoso
- RestarFueraHorario_DeberiaSerFallido
- SumarDentroHorario_DeberiaSerExitoso
- SumarFueraHorario_DeberiaSerFallido
- SumarYGuardar_DeberiaGuardarOperacionEnRepositorio

## CalculadoraUnitTest

- Dividir_DeberíaRetornarErrorDivisionCero
- Dividir_DeberiaRetornarResultadoCorrecto
- Multiplicar_DeberiaRetornarResultadoCorrecto
- Restar_DeberiaRetornarResultadoCorrecto
- Sumar_DeberiaRetornarResultadoCorrecto
