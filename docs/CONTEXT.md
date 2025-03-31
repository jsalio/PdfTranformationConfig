# Descripción General del Proyecto

Este proyecto consiste en el desarrollo de un backend en C# con .NET Core 8 para gestionar configuraciones de conversión de páginas a PDF. El sistema debe permitir operaciones CRUD sobre recursos relacionados con configuraciones, motores, documentos y tareas de trabajo, utilizando una arquitectura hexagonal (Ports and Adapters) combinada con Clean Architecture.

**Directorio de trabajo:** `src/backend`  
**Objetivo:** Proporcionar una API RESTful para gestionar configuraciones y tareas de conversión a PDF, con una base de datos relacional optimizada.  
**Fecha de referencia:** 30 de marzo de 2025  

## Estructura del Proyecto

El proyecto sigue una arquitectura hexagonal con las siguientes capas:

- **Capa API:** Contiene controladores RESTful y actúa como adaptador de entrada.
- **Capa Core:** Incluye la lógica de negocio (modelos, casos de uso, interfaces, validaciones, enums).
- **Capa de Infraestructura:** Maneja la persistencia y servicios externos (a implementar por el desarrollador humano).
- **Capa de Pruebas:** Pruebas unitarias con TDD usando NUnit y NSubstitute.

## Endpoints del API

| Método HTTP | Endpoint              | Descripción                |
|------------|-----------------------|----------------------------|
| `POST`     | `/api/[recurso]`      | Crea un recurso           |
| `GET`      | `/api/[recurso]/{id}` | Obtiene un recurso por ID |
| `GET`      | `/api/[recurso]`      | Lista todos los recursos  |
| `PUT`      | `/api/[recurso]/{id}` | Actualiza un recurso      |
| `DELETE`   | `/api/[recurso]/{id}` | Elimina un recurso        |

- `[recurso]`: Nombre en plural del recurso (ej. `configurations`, `engines`).
- `{id}`: Identificador único (generalmente un entero o GUID).

## Modelos de Base de Datos

La base de datos relacional está diseñada con las siguientes tablas principales y de caché.

**Nota importante:** La implementación de la capa de base de datos (incluyendo migraciones y configuraciones específicas) será manejada por el desarrollador humano y no debe ser modificada por la IA sin consulta explícita.

### Tablas Principales

- **Configuration**: `id (PK)`, `name`, `description`, `workflowID`, `engineId (FK)`, `retryLimit`
- **Engine**: `id (PK)`, `name`, `description`, `requirements (JSON)`, `acceptOcr`
- **DocumentConfiguration**: `id (PK)`, `configurationId (FK)`, `documentTypeId`, `convertToPDF`, `applyOcr`
- **DocumentRetryLog**: `id (PK)`, `lockId (FK)`, `documentId`, `lastRetry`, `errorReason`, `validForCount`
- **DocumentLocks**: `id (PK)`, `documentId`, `created`, `lastWork`
- **WorkTask**: `id (PK)`, `name`, `created`, `configurationId (FK)`, `status (enum: Pending, Working, Paused, Canceled)`
- **WorkTaskDetails**: `id (PK)`, `workTaskId (FK)`, `documentId`, `status (enum: Pending, Converting, Converted, Error)`, `created`, `lastDateWork`
- **CancelationsTasks**: `id (PK)`, `worktaskId (FK)`, `justificacion`, `created`
- **Profiles**: `id (PK)`, `name`, `permissions (JSON)`
- **GeneratedDocuments**: `id (PK)`, `workTaskId (FK)`, `documentId`, `fileName`, `directory`, `created`

### Tablas de Caché

- **WorkflowCache**: `workflowID (PK)`, `workflowName`
- **DocumentTypeCache**: `documentTypeId (PK)`, `documentTypeName`

## Instrucciones para el Agente de IA

El agente de IA de Cursor debe asistir en la implementación del backend siguiendo estas directrices:

### Qué Hacer

#### **Generación de Código:**
- Crear controladores RESTful basados en la estructura de la capa API.
- Implementar interfaces y clases para casos de uso en la capa Core.
- Generar modelos de dominio en C# que coincidan con las tablas de la base de datos.
- Escribir pruebas unitarias siguiendo TDD (`Red`, `Green`, `Refactor`) con NUnit y NSubstitute.

- **Nota importante** Las pruebas unitarias  solo aplican al core siguiendo TDD (`Red`, `Green`, `Refactor`).

#### **Sugerencias:**
- Proponer validaciones para los modelos usando FluentValidation o atributos.
- Sugerir optimizaciones en los casos de uso.

#### **Estructura:**
- Seguir la organización en capas: `Api`, `Core`, `Infrastructure`, `Tests`.
- Usar inyección de dependencias en la configuración del proyecto.

### Límites y Restricciones

#### **No Modificar la Base de Datos:**
- La estructura de la base de datos ya está definida y no debe ser alterada (ni tablas, ni campos, ni relaciones) sin consulta explícita al desarrollador humano.
- La capa de acceso a datos (repositorios, `DbContext`, migraciones) será implementada manualmente y no debe ser generada por la IA.
- La configuración de la inyección de dependencias será implementada manualmente y no debe ser generada por la IA.

#### **No Asumir Implementaciones Externas:**
- No generar código para servicios externos (ej. sincronización de tablas de caché) a menos que se solicite explícitamente.
- No decidir cómo se implementarán los detalles de persistencia.

#### **Confirmar Cambios Importantes:**
- Si el agente identifica una mejora o discrepancia (ej. agregar un endpoint nuevo), debe preguntar al desarrollador antes de implementarla.
- No generar imágenes ni editar código fuera de lo solicitado.

#### **Evitar Juicios Subjetivos:**
- No intentar decidir qué recursos o datos son "correctos" o "incorrectos" (ej. validar contenido de campos JSON más allá de su formato).

## Notas Finales

- **Consulta al Desarrollador:** Si hay dudas sobre la implementación (ej. formato de DTOs, manejo de enums), el agente debe pedir aclaraciones.
- **Propósito:** El agente debe asistir en la escritura de código limpio y funcional, respetando la arquitectura hexagonal y los modelos de base de datos definidos.
