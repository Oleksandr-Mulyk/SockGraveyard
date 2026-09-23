# SockGraveyard Roadmap

> A production-grade microservices platform for tracking missing socks.
>
> Built with .NET 10, Aspire, gRPC, RabbitMQ, SignalR, CQRS, OpenTelemetry, and Avalonia.

---

# Vision

SockGraveyard is a fictional investigation platform that helps detectives solve the mystery of missing socks.

Users report missing socks, detectives investigate cases, inventory stores recovered socks, matching algorithms identify potential pairs, and the system notifies everyone when a case is solved.

The project exists to demonstrate modern .NET architecture through a fun and memorable domain.

---

# Architecture Goals

- .NET 10
- Aspire orchestration
- Microservices
- PostgreSQL
- EF Core
- gRPC
- RabbitMQ
- MassTransit
- SignalR
- ASP.NET Identity
- JWT + Refresh Tokens
- OpenTelemetry
- Serilog
- Hangfire
- Polly
- Avalonia
- Testcontainers
- xUnit

---

# Development Phases

## Phase 1 - Core Investigation System

Create the core business flow.

### Stage 1 - Cases Service MVP

#### Projects

```text
SockGraveyard.AppHost
SockGraveyard.ServiceDefaults
SockGraveyard.Cases
SockGraveyard.Cases.Tests
```

#### Features

- Create case
- Get case
- List cases
- Start investigation
- Close unsolved case

#### Technologies

- Aspire
- Minimal API
- EF Core
- PostgreSQL
- Serilog
- OpenTelemetry
- Health Checks

#### Definition of Done

- Cases stored in PostgreSQL
- Swagger available
- Health checks available
- Integration tests passing

---

### Stage 2 - Catalog Service

#### Projects

```text
SockGraveyard.Catalog
SockGraveyard.Catalog.Tests
```

#### Features

Sock profiles:

- Color
- Pattern
- Size
- Material

Cases reference catalog items.

#### Definition of Done

- Catalog API available
- Catalog database created
- Cases linked to catalog profiles

---

### Stage 3 - Inventory Service

#### Projects

```text
SockGraveyard.Inventory
SockGraveyard.Inventory.Tests
```

#### Features

- Store recovered socks
- gRPC communication
- Inventory management

#### Definition of Done

- Inventory service online
- gRPC endpoint working
- Aspire service discovery configured

---

### Stage 4 - Matching Engine

#### Features

- Automatic matching
- Match confidence score
- Match confirmation workflow

#### State Machine

```text
Reported
↓
Investigating
↓
Matched
↓
Closed
```

Alternative:

```text
Investigating
↓
ClosedUnsolved
```

#### Definition of Done

- Match search implemented
- Match confirmation implemented
- Reserved inventory items supported

---

# Phase 2 - Real-Time Microservices

Introduce asynchronous communication.

### Stage 5 - Notifications Service

#### Projects

```text
SockGraveyard.Notifications
SockGraveyard.Notifications.Tests
```

#### Features

- SignalR Hub
- Notification history
- Real-time updates

#### Notification Types

- CaseCreated
- InvestigationStarted
- MatchFound
- CaseClosed
- CaseClosedUnsolved

#### Definition of Done

- SignalR connected
- Notifications stored
- Realtime updates visible

---

### Stage 6 - RabbitMQ + MassTransit

#### Projects

```text
SockGraveyard.Contracts
```

#### Features

- RabbitMQ
- MassTransit
- Event-driven communication

#### Events

```text
CaseReported
InvestigationStarted
MatchFound
CaseClosed
CaseClosedUnsolved
```

#### Definition of Done

- Events published
- Events consumed
- Retry policies configured
- Dead Letter Queue configured

---

# Phase 3 - Security & User Experience

Turn the system into a usable application.

### Stage 7 - Auth Service

#### Projects

```text
SockGraveyard.Auth
SockGraveyard.Auth.Tests
```

#### Features

- ASP.NET Identity
- JWT
- Refresh Tokens
- Roles

#### Roles

```text
Citizen
Detective
Admin
```

#### Definition of Done

- Authentication working
- Authorization working
- Role restrictions enforced

---

### Stage 8 - Detective Dashboard

#### Projects

```text
SockGraveyard.DetectiveDashboard
SockGraveyard.DetectiveDashboard.Tests
```

#### Technologies

- Avalonia
- MVVM
- SignalR Client

#### Screens

```text
Login
Cases
Case Details
Notifications
Analytics
```

#### Definition of Done

- Complete investigation workflow available through UI
- SignalR updates visible in dashboard

---

# Phase 4 - Production Features

Complete the platform.

### Stage 9 - Reports Service

#### Projects

```text
SockGraveyard.Reports
SockGraveyard.Reports.Tests
```

#### Technologies

- Hangfire
- Read Models
- Event Consumers

#### Analytics

- Resolution Rate
- Most Missing Colors
- Most Dangerous Locations
- Investigation Duration

#### Definition of Done

- Reports API available
- Scheduled jobs running
- Analytics displayed in dashboard

---

### Stage 10 - Gateway & Rewards

#### Projects

```text
SockGraveyard.Gateway
SockGraveyard.Rewards
```

#### Gateway

- YARP
- Rate Limiting
- Routing
- Central Entry Point

#### Rewards

- Reward processing
- Stripe Sandbox
- Polly Retry
- Circuit Breaker

#### Definition of Done

- Single public entry point
- Reward workflow completed
- Full distributed tracing available

---

# Final Architecture

```text
Gateway (YARP)
    │
    ├── Auth
    ├── Cases
    ├── Catalog
    ├── Inventory
    ├── Rewards
    │
    └── RabbitMQ
            │
            ├── Notifications
            └── Reports

DetectiveDashboard
```

---

# Success Criteria

A detective can:

1. Log in.
2. Review reported cases.
3. Investigate a missing sock.
4. Search inventory.
5. Confirm a match.
6. Issue a reward.
7. Close the case.
8. See analytics update in real time.

At that point SockGraveyard is considered feature complete.