# Calculadora - Fundamentos de Pruebas en .NET

Este proyecto es una base para practicar **pruebas unitarias en .NET** usando **NUnit** y **Moq**.  
Incluye una librería (`CalculadoraLib`) y un proyecto de pruebas (`CalculadoraTests`).  

---

## Requisitos

- [Visual Studio 2022](https://visualstudio.microsoft.com/es/)
- [.NET 8 SDK o superior](https://dotnet.microsoft.com/en-us/download/dotnet)  
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

## Configuracion para el uso del Interface ITimeProvider
En el proyecto `CalculadoraLib`, se ha definido una interfaz `ITimeProvider` para abstraer
la obtención de la hora actual. Esto es útil para facilitar las pruebas unitarias,
permitiendo simular diferentes momentos del tiempo sin depender del reloj del sistema.

## Pruebas unitarias implementadas
1. **Suma**: Verifica que la función de suma devuelve el resultado correcto.
2. **Resta**: Verifica que la función de resta devuelve el resultado correcto.
3. **Multiplicación**: Verifica que la función de multiplicación devuelve el resultado correcto.
4 . **División**: Verifica que la función de división devuelve el resultado correcto
6. **División por cero**: Verifica que la función de división lanza una excepción al intentar dividir por cero.
7. Operacion en hora fuera del rango permitido: Verifica que las operaciones se comportan correctamente
cuando se simula una hora fuera del rango permitido utilizando `ITimeProvider`.
8. Operacion en hora dentro del rango permitido: Verifica que las operaciones se comportan correctamente

## Pruebas del Servicio
1. **Servicio de Suma y Guardado**: Verifica que el servicio de suma funciona correctamente y guarda el resultado en la base de datos simulada.
2. **Servicio de Suma y Guardado Hora Fuera del Rango Permitido**: Verifica que el servicio de suma lanza una excepción cuando se simula una hora fuera del rango permitido utilizando `ITimeProvider`.

