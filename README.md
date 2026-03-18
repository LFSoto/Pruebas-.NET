# Pruebas-.NET — Semana 5 (GitHub Actions)

Este repo contiene ejemplos en .NET. En la rama `Semana5-GitActions` se incluye un ejemplo de **CI/CD con GitHub Actions** para ejecutar pruebas automatizadas (Selenium + NUnit) y publicar resultados.

## Pipeline (GitHub Actions)

Workflow: `/.github/workflows/dotnet-tests.yml`

Al ejecutarse, el pipeline:

1. Restaura dependencias (`dotnet restore`)
2. Compila en Release (`dotnet build -c Release`)
3. Ejecuta pruebas (`dotnet test -c Release`) y genera un archivo `.trx`
4. Publica artifacts:
   - Carpeta completa `TestResults/`
   - Archivo(s) `.trx` (por separado para descarga rápida)
5. Publica un resumen de resultados en el **Job Summary**
6. En `push` a `main` y si las pruebas pasan, crea un **GitHub Release** con:
   - Tag con formato `YYYY.MM.DD` y si ya existe uno del día, usa `YYYY.MM.DD.v1`, `v2`, etc.
   - Asset `automation-source.zip` con el código fuente (sin `bin/`, `obj/`, `TestResults/`)

## Requisitos (ejecución local)

- .NET SDK (recomendado **.NET 9**)
- Google Chrome
- ChromeDriver / Selenium Manager (según dependencias del proyecto)

## Clonar y cambiar a la rama

```bash
git clone https://github.com/LFSoto/Pruebas-.NET.git
cd Pruebas-.NET
git checkout Semana5-GitActions
```

## Ejecutar las pruebas localmente (igual que en CI)

```bash
cd AutomationPracticeDemo.Tests
dotnet restore
dotnet build -c Release
dotnet test -c Release --no-build \
  --logger "trx;LogFileName=test-results.trx" \
  --results-directory ../TestResults
```

Los resultados quedan en `./TestResults/` (en la raíz del repo).

## Notas

- Este pipeline está pensado como ejemplo para demostrar:
  - ejecución automática de pruebas
  - publicación de resultados
  - creación automática de releases con un artefacto (código) para reportes a management.
