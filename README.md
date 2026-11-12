# 🌎 Mundo: Full-Stack Web App (.NET 9)

[![CI](https://github.com/damdavidprieto/Mundo/actions/workflows/dotnet.yml/badge.svg)](https://github.com/damdavidprieto/Mundo/actions/workflows/dotnet.yml)
![Tech: .NET 9](https://img.shields.io/badge/Stack-.NET%209-512bd4?style=flat-square)
![Stack: Razor Pages](https://img.shields.io/badge/UI-Razor%20Pages-green?style=flat-square)
![Database: SQL Server](https://img.shields.io/badge/DB-SQL%20Server-red?style=flat-square)

> **Sobre este proyecto**
> Este repositorio constituye un ejemplo de aplicación web moderna desarrollada con el último stack de **.NET 9**. Se conserva como parte de un portfolio de ingeniería de software para demostrar competencias en el desarrollo de aplicaciones web server-side y modelado de datos.

---

## 🎯 Arquitectura del Proyecto

El sistema está dividido en dos capas principales siguiendo principios de separación de responsabilidades:

- **MundoAPI**: La capa de presentación y lógica de aplicación. Utiliza **Razor Pages** para una gestión eficiente de las vistas y el ciclo de vida de la página en el servidor.
- **Mundo (Infrastructure)**: Capa de acceso a datos y modelos de dominio. Implementa el contexto de base de datos con **Entity Framework Core 9** y modelos autogenerados para alta fidelidad con el esquema de SQL Server.

---

## 🛠️ Stack Tecnológico

*   **Runtime**: [.NET 9.0](https://dotnet.microsoft.com/download/dotnet/9.0) (LTS)
*   **Web Framework**: **Razor Pages** para una arquitectura orientada a páginas clara y testeable.
*   **ORM**: [Entity Framework Core 9.0](https://docs.microsoft.com/ef/core/) para SQL Server.
*   **Tooling**: Configuración optimizada con **EF Core Power Tools** para ingeniería inversa de bases de datos.
*   **Static Assets**: Integración moderna de assets estáticos nativa de .NET 9 (`MapStaticAssets`).

---

## 🚀 Despliegue Local

Para ejecutar este proyecto en un entorno de desarrollo:

1.  **Requisitos**: .NET 9 SDK y SQL Server local.
2.  **Configuración**: Actualizar la cadena de conexión en `MundoAPI/appsettings.json`.
3.  **Ejecución**:
    ```bash
    cd MundoAPI
    dotnet run
    ```

## 🧪 Tests

Los tests unitarios (xUnit, EF Core InMemory) se ejecutan en cada push con GitHub Actions:

```bash
dotnet test
```

---
*Este repositorio es un testimonio de ingeniería aplicada a soluciones web modernas y eficientes.*
