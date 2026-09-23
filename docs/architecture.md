# Architecture

## Overview

SockGraveyard is a production-style microservices platform for investigating missing socks.

The system allows citizens to report missing socks, detectives to investigate cases, inventory workers to register recovered socks, and the platform to identify potential matches.

The project is intentionally built around a humorous domain while using real-world architectural patterns and technologies.

---

# Business Flow

```text
Citizen reports a missing sock
            ↓
Case created
            ↓
Detective investigates
            ↓
Inventory searched
            ↓
Potential match found
            ↓
Match confirmed
            ↓
Reward issued
            ↓
Case closed
            ↓
Reports updated
            ↓
Notifications sent
```

---

# System Architecture

```text
                    ┌───────────────┐
                    │ Gateway(YARP) │
                    └───────┬───────┘
                            │

      ┌──────────┬──────────┼──────────┬──────────┐
      ▼          ▼          ▼          ▼          ▼

    Auth      Cases      Catalog   Inventory   Rewards

                 │                      │
                 │ gRPC                 │
                 ▼                      │

            Matching Engine            │

                 │                      │
                 └──────────┬───────────┘
                            ▼

                       RabbitMQ

           ┌────────────┬────────────┐
           ▼            ▼            ▼

    Notifications    Reports     Future Services


           ▼
       SignalR


           ▼
    Detective Dashboard
```

---

# Architecture Principles

## Service Ownership

Every service owns:

- Its data
- Its business rules
- Its database
- Its API

Other services must never access another service database.

---

## Database Per Service

Every bounded context owns its persistence layer.

```text
Cases DB
Catalog DB
Inventory DB
Auth DB
Reports DB
Rewards DB
Notifications DB
```

A single PostgreSQL server may host multiple databases.

---

## Event Driven Communication

Business events are the primary integration mechanism.

Examples:

```text
CaseReported
InvestigationStarted
MatchFound
CaseClosed
CaseClosedUnsolved
RewardIssued
```

Services publish events through RabbitMQ.

Consumers react independently.

---

## Service-to-Service Communication

Synchronous communication:

```text
gRPC
```

Used for:

```text
Cases → Inventory
```

Asynchronous communication:

```text
RabbitMQ + MassTransit
```

Used for:

```text
Cases → Notifications
Cases → Reports
Cases → Rewards
```

---

# Bounded Contexts

---

## Auth

### Responsibility

Identity and access management.

### Owns

```text
Users
Roles
Refresh Tokens
```

### Technologies

```text
ASP.NET Identity
JWT
Refresh Tokens
```

### Public API

```text
Register
Login
Refresh Token
Assign Roles
```

---

## Cases

### Responsibility

Investigation lifecycle.

### Owns

```text
Sock Cases
State Machine
Match Confirmation
```

### State Machine

```text
Reported
↓
Investigating
↓
Matched
↓
Closed
```

Alternative path:

```text
Investigating
↓
ClosedUnsolved
```

### Technologies

```text
Minimal API
EF Core
PostgreSQL
```

---

## Catalog

### Responsibility

Sock descriptions.

### Owns

```text
Color
Pattern
Size
Material
```

### Technologies

```text
Minimal API
EF Core
PostgreSQL
```

### Example

```text
Blue
Striped
Cotton
M
```

---

## Inventory

### Responsibility

Recovered socks storage.

### Owns

```text
Recovered socks
Reservations
Matching requests
```

### Technologies

```text
gRPC
EF Core
PostgreSQL
```

### Public Contract

```text
FindMatch()
ReserveSock()
```

---

## Rewards

### Responsibility

Reward processing.

### Owns

```text
Rewards
Payment status
Payment history
```

### Technologies

```text
Stripe Sandbox
Polly
EF Core
```

### States

```text
Pending
Processing
Completed
Failed
```

---

## Notifications

### Responsibility

Realtime communication.

### Owns

```text
Notification History
SignalR Hub
```

### Sources

```text
RabbitMQ Events
```

### Outputs

```text
SignalR
REST API
```

---

## Reports

### Responsibility

Analytics and projections.

### Owns

```text
Read Models
Aggregates
Statistics
```

### Technologies

```text
Hangfire
MassTransit
EF Core
```

### Metrics

```text
Resolution Rate
Average Resolution Time
Most Missing Colors
Most Common Locations
```

---

# Client Applications

---

## Detective Dashboard

Desktop application for detectives.

### Technologies

```text
Avalonia
MVVM
SignalR Client
JWT
```

### Features

```text
Login

View Cases

Investigate Cases

Review Matches

Realtime Notifications

Analytics
```

---

# Infrastructure

---

## AppHost

Application orchestration.

### Responsibilities

```text
Service registration

Containers

Dependencies

Service discovery
```

### Technology

```text
.NET Aspire
```

---

## ServiceDefaults

Shared infrastructure.

### Includes

```text
OpenTelemetry

Health Checks

Structured Logging

Service Discovery

Resilience Defaults
```

---

# Messaging

## RabbitMQ

Message broker.

### Exchanges

```text
cases
notifications
reports
rewards
```

### Features

```text
Retries

Dead Letter Queue

Delayed Delivery
```

Implemented via:

```text
MassTransit
```

---

# Observability

## Logging

Technology:

```text
Serilog
```

Every important operation logs:

```text
CaseId
UserId
TraceId
CorrelationId
```

---

## Tracing

Technology:

```text
OpenTelemetry
```

Example trace:

```text
Dashboard
↓
Gateway
↓
Cases
↓
Inventory
↓
RabbitMQ
↓
Notifications
```

---

## Health Checks

Every service exposes:

```text
/health
```

The Aspire Dashboard becomes the operational control center.

---

# Security

Authentication:

```text
JWT Bearer
```

Authorization:

```text
Role Based Access Control
```

Roles:

```text
Citizen
Detective
Admin
```

---

# Repository Structure

```text
src/

SockGraveyard.AppHost
SockGraveyard.ServiceDefaults

SockGraveyard.Contracts

SockGraveyard.Gateway

SockGraveyard.Auth
SockGraveyard.Cases
SockGraveyard.Catalog
SockGraveyard.Inventory
SockGraveyard.Notifications
SockGraveyard.Reports
SockGraveyard.Rewards

SockGraveyard.DetectiveDashboard
```

```text
tests/

SockGraveyard.Auth.Tests
SockGraveyard.Cases.Tests
SockGraveyard.Catalog.Tests
SockGraveyard.Inventory.Tests
SockGraveyard.Notifications.Tests
SockGraveyard.Reports.Tests
SockGraveyard.Rewards.Tests
SockGraveyard.DetectiveDashboard.Tests
```

---

# Non Goals

The project is not intended to demonstrate:

- Kubernetes
- Service Mesh
- Multi-region deployment
- Distributed transactions
- Event sourcing
- CQRS read/write separation for every service

Those topics may be explored in future versions.

---

# Final Goal

Demonstrate modern .NET architecture through a complete and memorable business domain while keeping the codebase understandable, maintainable, and enjoyable to explore.