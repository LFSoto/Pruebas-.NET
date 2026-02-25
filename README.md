# Unit Testing Practice - .NET

## Información del Estudiante
-Nombre del Estudiante: [Kenneth Oviedo Arrieta]
- Curso: [SOFT-740 Test Automation in .NET]
- Fecha: [February 25, 2025]

## Descripción del Proyecto
Este proyecto implementa pruebas unitarias para un servicio de Calculadora. El objetivo principal fue validar las operaciones aritméticas y aplicar un horario de oficina (08:00 - 18:00) utilizando inyección de dependencias y simulacros (mocking).

## Pruebas Implementadas
He implementado los siguientes escenarios de prueba utilizando NUnit:

1. Operaciones Básicas: Se validaron Suma, Resta, Multiplicación y División.

2. Manejo de Errores: Se verificó que la división entre cero lance una excepción DivideByZeroException.

3. Validación de Tiempo: - Caso Válido: Las operaciones funcionan correctamente dentro del horario de oficina.

4. Caso Inválido: Las operaciones lanzan una excepción InvalidOperationException fuera del horario de 08:00-18:00.

## Cómo se utilizó Moq
Utilicé Moq para aislar la clase Calculadora de la hora real del sistema:

Mock de ITimeProvider: Creé un simulacro de la interfaz ITimeProvider para simular diferentes momentos del día.

Setup: Utilicé .Setup(tp => tp.Now) para devolver valores específicos de DateTime en mis pruebas.

Verificación: En CalculadoraServiceTests, utilicé .Verify() para asegurar que el repositorio solo fuera llamado cuando la operación fuera exitosa y nunca cuando ocurriera una excepción.