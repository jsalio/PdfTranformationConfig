# Plan de Implementación del Backend

Este documento describe el plan de acción para desarrollar el backend en C# con .NET Core 8 para la gestión de configuraciones de conversión de páginas a PDF, siguiendo una arquitectura hexagonal (Ports and Adapters) combinada con Clean Architecture.

## 1. Configuración Inicial del Proyecto

### 1.1 Estructura de Carpetas
- Crear la estructura base del proyecto siguiendo la arquitectura hexagonal:
  - `src/backend/Api`
  - `src/backend/Core`
  - `src/backend/Infrastructure`
  - `src/backend/Tests`
- **Nota:** Crear una solución (`.sln`) para gestionar y referenciar los distintos proyectos de manera centralizada.

### 1.2 Configuración de Proyectos
- Crear los proyectos de .NET Core 8 para cada capa.
- Configurar las referencias entre proyectos.
- **Nota:** El desarrollador humano es responsable de la inyección de dependencias. Esto debido a la restricción de tocar la infrastructure.

## 2. Implementación de la Capa Core

### 2.1 Modelos de Dominio
- Implementar las entidades de dominio basadas en las tablas de la base de datos:
  - Configuration
  - Engine
  - DocumentConfiguration
  - DocumentRetryLog
  - DocumentLocks
  - WorkTask
  - WorkTaskDetails
  - CancelationsTasks
  - Profiles
  - GeneratedDocuments
  - WorkflowCache
  - DocumentTypeCache
- **Sugerencia:** Considerar la implementación de Value Objects y Aggregates para seguir un enfoque más DDD. 

### 2.2 Enumeraciones
- Implementar enumeraciones para estados:
  - WorkTaskStatus (Pending, Working, Paused, Canceled)
  - WorkTaskDetailStatus (Pending, Converting, Converted, Error)

### 2.3 Interfaces de Repositorio
- Definir interfaces para los repositorios de cada entidad.
- Implementar interfaces genéricas para operaciones CRUD.
- **Sugerencia:** Evaluar el uso de patrones como UnitOfWork o Specification para mejorar la abstracción del acceso a datos.

### 2.4 Casos de Uso
- Implementar casos de uso para cada operación de negocio:
  - Gestión de configuraciones
  - Gestión de motores
  - Gestión de documentos
  - Gestión de tareas de trabajo
  - *Ejemplo:* Gestión de tareas de trabajo → Crear, asignar, actualizar estado, cancelar, reintentar.
  
### 2.5 Validaciones
- Implementar validadores usando FluentValidation para cada entidad y caso de uso.
- **Sugerencia:** Definir claramente si las validaciones se aplicarán a nivel de DTOs, en el dominio, o en ambos.

### 2.6 Pruebas Unitarias para el Core
- Implementar pruebas unitarias exclusivamente para los componentes del Core (modelos, casos de uso, validadores).
- Seguir el enfoque TDD: Red, Green, Refactor.
- Utilizar NUnit como framework de pruebas y NSubstitute para mocks.
- **Nota:** Las pruebas unitarias solo aplican a la capa Core, según lo especificado en los requisitos.

## 3. Implementación de la Capa API

### 3.1 DTOs
- Crear DTOs para cada entidad (entrada y salida).
- Implementar mapeos entre DTOs y entidades de dominio.

### 3.2 Controladores
- Implementar controladores RESTful para cada recurso:
  - ConfigurationsController
  - EnginesController
  - DocumentConfigurationsController
  - WorkTasksController
  - ProfilesController
  - GeneratedDocumentsController
- **Sugerencia:** Incluir soporte para paginación en aquellos endpoints que retornen colecciones grandes.

### 3.3 Middleware y Filtros
- Implementar middleware para manejo de excepciones.
- Configurar filtros de validación.
- **Sugerencia:** Utilizar un global exception handler que emplee `ProblemDetails` para un manejo de errores más estándar.

## 4. Implementación de Pruebas

### 4.1 Pruebas Unitarias
- **Nota importante:** Las pruebas unitarias solo se implementarán para los componentes de la capa Core, siguiendo la restricción especificada en los requisitos.
- Implementar pruebas unitarias para casos de uso de la capa Core.
- Implementar pruebas unitarias para validadores de la capa Core.
- Usar método Red, Green, Refactor para el desarrollo de las pruebas.
- No se implementarán pruebas unitarias para controladores u otros componentes fuera de la capa Core.

### 4.2 Mocks y Stubs
- Configurar NSubstitute para simular dependencias.
- Crear datos de prueba.
- **Sugerencia:** Considerar el uso de un FakeDbContext o InMemoryDatabase para pruebas de persistencia.

## 5. Documentación

### 5.1 Swagger/OpenAPI
- Configurar Swagger para la documentación de la API.
- Añadir comentarios XML para mejorar la documentación.

### 5.2 README y Guías
- Crear documentación de uso.
- Documentar la arquitectura y las decisiones de diseño.
- Incluir una guía de "Cómo correr las pruebas".

## 6. Integración y Configuración Final

### 6.1 Configuración de la Aplicación
- Configurar `appsettings.json` para diferentes entornos.
- Implementar logging.

### 6.2 Preparación para Despliegue
- Configurar Docker (si es necesario).
- Preparar scripts de CI/CD.
- **Sugerencia:** Incluir instrucciones para el manejo de migraciones en entornos de producción y definir la herramienta de CI/CD a utilizar (por ejemplo, GitHub Actions, GitLab CI/CD o Azure DevOps).

## Notas Importantes

- **Implementación de la Capa de Infraestructura:** La implementación de repositorios concretos, `DbContext`, migraciones y demás detalles de persistencia será responsabilidad del desarrollador humano.
- **Pruebas Unitarias:** Las pruebas unitarias solo se aplicarán a los componentes de la capa Core, siguiendo el enfoque TDD (Red, Green, Refactor).
- **Configuración de la Inyección de Dependencias:** La configuración de la inyección de dependencias será implementada manualmente por el desarrollador humano y no debe ser generada por la IA.
- Se consultará con el desarrollador humano antes de realizar cambios significativos en la arquitectura o añadir nuevas funcionalidades no especificadas.
- Se respetará la estructura de la base de datos definida sin realizar modificaciones.
