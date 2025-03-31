# PDF Converter

Un backend en C# con .NET Core 8 para gestionar configuraciones de conversión de páginas a PDF, siguiendo una arquitectura hexagonal (Ports and Adapters) combinada con Clean Architecture.

## Descripción

Este proyecto proporciona una API RESTful para gestionar configuraciones y tareas de conversión a PDF, con una base de datos relacional optimizada. Permite operaciones CRUD sobre recursos relacionados con configuraciones, motores, documentos y tareas de trabajo.

## Estructura del Proyecto

El proyecto sigue una arquitectura hexagonal con las siguientes capas:

- **Capa API (`src/backend/Api`):** Contiene controladores RESTful y actúa como adaptador de entrada.
- **Capa Core (`src/backend/Core`):** Incluye la lógica de negocio (modelos, casos de uso, interfaces, validaciones, enums).
- **Capa de Infraestructura (`src/backend/Infrastructure`):** Maneja la persistencia y servicios externos.
- **Capa de Pruebas (`src/backend/Tests`):** Pruebas unitarias con TDD usando NUnit y NSubstitute.

## Requisitos

- .NET Core 8.0 SDK
- SQL Server (para la base de datos)
- Visual Studio 2022 o Visual Studio Code

## Configuración del Proyecto

1. Clonar el repositorio
2. Restaurar los paquetes NuGet
   ```bash
   dotnet restore
   ```
3. Compilar la solución
   ```bash
   dotnet build
   ```
4. Ejecutar las pruebas
   ```bash
   dotnet test
   ```
5. Ejecutar la aplicación
   ```bash
   dotnet run --project src/backend/Api/PdfConverter.Api.csproj
   ```

## Endpoints del API

| Método HTTP | Endpoint              | Descripción                |
|------------|-----------------------|----------------------------|
| `POST`     | `/api/[recurso]`      | Crea un recurso           |
| `GET`      | `/api/[recurso]/{id}` | Obtiene un recurso por ID |
| `GET`      | `/api/[recurso]`      | Lista todos los recursos  |
| `PUT`      | `/api/[recurso]/{id}` | Actualiza un recurso      |
| `DELETE`   | `/api/[recurso]/{id}` | Elimina un recurso        |

Donde `[recurso]` puede ser:
- `configurations`
- `engines`
- `document-configurations`
- `work-tasks`
- `profiles`
- `generated-documents`

## Tecnologías Utilizadas

- C# 12
- .NET Core 8
- Entity Framework Core
- FluentValidation
- AutoMapper
- NUnit (para pruebas)
- NSubstitute (para mocks)
- Swagger/OpenAPI

## Licencia

[MIT](LICENSE)
