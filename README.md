# Smart Home HERA Platform

Backend (Web Services) del proyecto **Smart Home HERA** (Dev-il-team). Plataforma web para la
gestión integral de hogares inteligentes (IoT): centraliza el control, monitoreo, automatización
y análisis de consumo de los dispositivos del hogar.

Construido con .NET / ASP.NET Core siguiendo **Domain-Driven Design (DDD)** y CQRS, replicando la
estructura y patrones del proyecto de referencia `learning-center-platform`.

## Bounded Contexts

| Bounded Context   | Épica (informe) | Descripción |
|-------------------|-----------------|-------------|
| **Iam**           | EP02            | Identidad y acceso: registro, inicio de sesión seguro (JWT), autorización. |
| **Profiles**      | EP02            | Perfil del propietario del hogar (Home Owner). |
| **Devices**       | EP03            | Gestión de dispositivos IoT y habitaciones (Rooms). Contexto rico con eventos. |
| **Automation**    | EP05            | Rutinas y escenas programadas (Morning Routine, triggers). |
| **Monitoring**    | EP04            | Panel de control y monitoreo remoto (cámaras de vigilancia). |
| **EnergyAnalytics**| EP06           | Análisis de consumo energético (reportes de consumo). |
| **Subscriptions** | EP07            | Gestión de suscripciones (plan Básico / Premium). |
| **Shared**        | EP08 (técnico)  | Kernel compartido: repositorios base, UnitOfWork, middleware, ProblemDetails, mediator. |

## Estructura por capas (por cada Bounded Context)

- **Domain**: `Model/Aggregates`, `Model/Entities`, `Model/Commands`, `Model/Queries`,
  `Model/ValueObjects`, `Model/Events`, `Model/Errors`, `Repositories`.
- **Application**: `CommandServices` / `QueryServices` (interfaces) e `Internal/*` (implementaciones),
  `Internal/EventHandlers`, `Acl`.
- **Infrastructure**: `Persistence/EntityFrameworkCore/{Configuration,Repositories}`.
- **Interfaces**: `Rest` (Controllers), `Rest/Resources`, `Rest/Transform` (Assemblers).
- **Resources**: mensajes de localización (en / es).

## Tecnologías

.NET 10, ASP.NET Core, Entity Framework Core (MySQL), Cortex.Mediator, BCrypt.Net, JWT,
Swagger / OpenAPI, Humanizer.

## Ejecución

```bash
dotnet restore
dotnet run --project Dev-ilTeam.Hera.Platform
```

La API queda documentada en `/swagger`.
