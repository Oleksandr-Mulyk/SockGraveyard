# SockGraveyard

A production-grade microservices platform for tracking missing socks — because someone has to solve this crisis.

Built on **.NET 10** with gRPC, CQRS, real-time case notifications, and a detective's desktop dashboard, all orchestrated with **.NET Aspire**. A serious architecture wrapped around a deeply unserious premise.

## The premise

Socks go missing in laundromats. Every day. Nobody investigates. Until now.

SockGraveyard is a case-management system for missing socks. Victims (users) file a case describing their missing sock. The system tries to match it against unpaired socks recovered from the "unmatched inventory." Detectives track cases through their lifecycle, get notified in real time when a match is found, and can review closed/unsolved case statistics from a dedicated desktop console.

## Architecture

```
                        ┌─────────────────┐
                        │  Gateway (YARP)  │
                        └────────┬─────────┘
                                 │
        ┌────────────┬──────────┼──────────┬────────────┐
        │             │          │          │            │
   ┌────▼────┐  ┌─────▼────┐ ┌──▼───┐ ┌────▼─────┐ ┌────▼─────┐
   │ Catalog │  │  Cases   │ │ Auth │ │ Inventory│ │ Rewards  │
   └─────────┘  └────┬─────┘ └──────┘ └────┬─────┘ └──────────┘
                      │                     │ gRPC
                      │            ┌────────┘
                 ┌────▼─────┐ ┌────▼──────┐  ┌─────────┐
                 │Notifications│ │  Reports  │  │ Detective│
                 │(SignalR/MQ) │ │(Hangfire) │  │Dashboard │
                 └─────────────┘ └───────────┘  │(Avalonia)│
                                                 └──────────┘

     All services orchestrated by SockGraveyard.AppHost (.NET Aspire)
```

## Services

| Service | Responsibility | Tech |
|---|---|---|
| `Gateway` | Routing, rate limiting | YARP |
| `Catalog` | Sock types — color, pattern, size, material ("distinguishing marks") | ASP.NET Core Minimal API, EF Core, PostgreSQL |
| `Cases` | Missing-sock case lifecycle (`reported → investigating → matched → closed / closed_unsolved`) | CQRS via MediatR, FluentValidation |
| `Auth` | Detective & admin accounts | ASP.NET Core Identity, JWT, refresh tokens |
| `Inventory` | Unpaired-sock stock sync with Cases | gRPC (server + client) |
| `Rewards` | "Reward for information" payouts | Stripe sandbox, Polly (retry / circuit breaker) |
| `Notifications` | Real-time + async case events | SignalR, RabbitMQ / MassTransit |
| `Reports` | Daily case-resolution analytics | Hangfire |
| `ServiceDefaults` | Shared observability | Serilog, health checks, OpenTelemetry |
| `DetectiveDashboard` | Desktop admin client | Avalonia (MVVM) |

Orchestration: `SockGraveyard.AppHost` — the entire precinct comes up with a single `dotnet run`.

## Repository structure

```
SockGraveyard/
├── src/
│   ├── SockGraveyard.AppHost/
│   ├── SockGraveyard.ServiceDefaults/
│   ├── SockGraveyard.Gateway/
│   ├── SockGraveyard.Catalog/
│   ├── SockGraveyard.Cases/
│   ├── SockGraveyard.Auth/
│   ├── SockGraveyard.Inventory/
│   ├── SockGraveyard.Rewards/
│   ├── SockGraveyard.Notifications/
│   ├── SockGraveyard.Reports/
│   └── SockGraveyard.DetectiveDashboard/
├── tests/
│   ├── SockGraveyard.Catalog.Tests/
│   ├── SockGraveyard.Cases.Tests/
│   ├── SockGraveyard.Auth.Tests/
│   ├── SockGraveyard.Inventory.Tests/
│   ├── SockGraveyard.Rewards.Tests/
│   ├── SockGraveyard.Notifications.Tests/
│   ├── SockGraveyard.Reports.Tests/
│   └── SockGraveyard.DetectiveDashboard.Tests/
├── SockGraveyard.slnx
└── README.md
```

Every service in `src/` ships with a matching test project in `tests/` — because even in SockGraveyard, unpaired socks aren't allowed.

## Running locally

Requirements: .NET 10 SDK, Docker Desktop (for PostgreSQL / RabbitMQ containers), Aspire workload.

```bash
dotnet run --project src/SockGraveyard.AppHost
```

This opens the Aspire Dashboard, showing every running service, its health status, logs, and traces.

## Testing

```bash
dotnet test
```

Each service has unit tests on its core business logic plus at least one integration test (Testcontainers or in-memory host) covering its main external dependency (database, queue, or gRPC call).

## License

MIT
