# Milestones

This document tracks the implementation progress of SockGraveyard.

Each milestone represents a meaningful product increment that can be demonstrated independently.

---

# Milestone 1
## The Investigation Department Opens

### Goal

Launch the first operational service.

### Services

```text
Cases
PostgreSQL
AppHost
ServiceDefaults
```

### Features

- Create case
- View case
- List cases
- Start investigation
- Close as unsolved

### Deliverables

- Aspire orchestration
- PostgreSQL integration
- EF Core migrations
- Swagger
- Health Checks
- Serilog logging
- Integration tests

### Success Criteria

A missing sock case can complete its basic lifecycle.

### Demo

```text
Create Case
↓
Investigating
↓
ClosedUnsolved
```

---

# Milestone 2
## The Sock Identification Bureau

### Goal

Introduce structured sock descriptions.

### Services

```text
Catalog
```

### Features

- Create sock profiles
- Manage colors
- Manage patterns
- Manage materials
- Manage sizes

### Deliverables

- Catalog database
- Catalog API
- Catalog tests

### Success Criteria

Cases reference catalog entries instead of free-text descriptions.

### Demo

```text
Blue
Striped
M
Cotton
```

---

# Milestone 3
## The Lost Sock Warehouse

### Goal

Introduce recovered sock inventory.

### Services

```text
Inventory
```

### Features

- Register recovered socks
- Manage inventory
- Inventory lookup

### Technologies

```text
gRPC
```

### Deliverables

- Inventory API
- Inventory database
- gRPC service
- Service discovery

### Success Criteria

Cases can communicate with Inventory.

### Demo

```text
Cases
↓
gRPC
↓
Inventory
```

---

# Milestone 4
## The First Solved Mystery

### Goal

Implement automatic matching.

### Features

- Matching engine
- Match score
- Match confirmation
- Inventory reservation

### New Status

```text
Matched
```

### Deliverables

- Matching service
- Match workflow
- State machine updates

### Success Criteria

A missing sock can be matched with a recovered sock.

### Demo

```text
Case
↓
Search Match
↓
Matched
↓
Closed
```

---

# Milestone 5
## The Precinct Gets Real-Time Communication

### Goal

Introduce live notifications.

### Services

```text
Notifications
```

### Technologies

```text
SignalR
```

### Features

- Realtime updates
- Notification history
- Live broadcasts

### Notification Types

```text
CaseCreated
InvestigationStarted
MatchFound
CaseClosed
CaseClosedUnsolved
```

### Success Criteria

Users receive updates instantly.

### Demo

```text
Create Case
↓
SignalR
↓
Notification Appears
```

---

# Milestone 6
## Event-Driven Investigations

### Goal

Introduce asynchronous communication.

### Technologies

```text
RabbitMQ
MassTransit
```

### New Project

```text
Contracts
```

### Events

```text
CaseReported
InvestigationStarted
MatchFound
CaseClosed
CaseClosedUnsolved
```

### Deliverables

- RabbitMQ
- Consumers
- Retry policies
- Dead Letter Queue

### Success Criteria

Services communicate through events.

### Demo

```text
Cases
↓
Publish Event
↓
RabbitMQ
↓
Notifications
```

---

# Milestone 7
## Detective Authentication System

### Goal

Secure the department.

### Services

```text
Auth
```

### Technologies

```text
Identity
JWT
Refresh Tokens
```

### Roles

```text
Citizen
Detective
Admin
```

### Features

- Registration
- Login
- Refresh tokens
- Role management

### Success Criteria

Authorization protects the platform.

### Demo

```text
Citizen
↓
Create Case

Detective
↓
Investigate Case
```

---

# Milestone 8
## Detective Workstation

### Goal

Provide a desktop experience.

### Services

```text
DetectiveDashboard
```

### Technologies

```text
Avalonia
MVVM
SignalR Client
```

### Screens

```text
Login
Cases
Case Details
Notifications
Analytics
```

### Success Criteria

A detective can perform investigations without Swagger.

### Demo

```text
Login
↓
Open Case
↓
Investigate
↓
Confirm Match
↓
Close Case
```

---

# Milestone 9
## Intelligence & Analytics Bureau

### Goal

Provide department reporting.

### Services

```text
Reports
```

### Technologies

```text
Hangfire
Read Models
Event Consumers
```

### Analytics

```text
Resolution Rate
Top Missing Colors
Most Dangerous Locations
Average Investigation Time
```

### Deliverables

- Scheduled jobs
- Statistics API
- Dashboard analytics

### Success Criteria

Management can evaluate department performance.

### Demo

```text
Reports
↓
Charts
↓
Trends
↓
Insights
```

---

# Milestone 10
## Production Release

### Goal

Complete the platform.

### Services

```text
Gateway
Rewards
```

### Technologies

```text
YARP
Polly
Stripe Sandbox
Rate Limiting
Circuit Breaker
```

### Features

- API Gateway
- Reward processing
- Resilience policies
- Centralized routing

### Deliverables

- YARP gateway
- Polly retry policies
- Rewards workflow
- Full tracing

### Success Criteria

Entire platform works as a production-style distributed system.

### Demo

```text
Citizen Reports Sock
↓
Case Created
↓
Detective Investigates
↓
Inventory Match Found
↓
Match Confirmed
↓
Reward Issued
↓
Case Closed
↓
Reports Updated
↓
Real-Time Notifications Sent
```

---

# Release Roadmap

## Phase 1
Core Investigation System

```text
Milestone 1
Milestone 2
Milestone 3
Milestone 4
```

Result:

```text
A missing sock can be reported,
investigated,
matched,
and closed.
```

---

## Phase 2
Real-Time Microservices

```text
Milestone 5
Milestone 6
```

Result:

```text
Realtime event-driven architecture.
```

---

## Phase 3
Security & User Experience

```text
Milestone 7
Milestone 8
```

Result:

```text
Authenticated users
and a real desktop application.
```

---

## Phase 4
Production Features

```text
Milestone 9
Milestone 10
```

Result:

```text
A complete production-style platform.
```

---

# Project Completion Definition

SockGraveyard is considered complete when:

- Citizens can report missing socks.
- Detectives can investigate cases.
- Inventory can track recovered socks.
- Matching engine can identify potential pairs.
- Notifications are delivered in real time.
- Events flow through RabbitMQ.
- Users authenticate using JWT.
- Dashboard supports full workflows.
- Reports generate operational analytics.
- Rewards are processed.
- Gateway exposes a single public entry point.
- OpenTelemetry provides full distributed tracing.

At this point the mystery of disappearing socks can finally be solved.