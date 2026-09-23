# Domain Model

## Overview

SockGraveyard is a fictional investigation platform dedicated to solving the global missing sock crisis.

The domain is intentionally humorous, but the business rules, architecture, and modeling follow real production practices.

The primary objective is simple:

```text
A sock disappears
↓
A case is opened
↓
A detective investigates
↓
A matching sock is found
↓
The case is closed
```

---

# Ubiquitous Language

The following terms must be used consistently across the entire solution.

| Term | Meaning |
|--------|---------|
| Sock Case | Investigation of a missing sock |
| Sock Profile | Structured description of a sock |
| Found Sock | Recovered sock stored in inventory |
| Match | Potential pairing between a missing and found sock |
| Detective | User responsible for investigations |
| Citizen | User reporting a missing sock |
| Admin | Department administrator |
| Reward | Compensation for information leading to sock recovery |
| Investigation | Active work on a sock case |

---

# Bounded Contexts

## Cases Context

Responsible for investigation workflow.

### Owns

```text
SockCase
SockMatch
CaseStatus
```

---

## Catalog Context

Responsible for describing socks.

### Owns

```text
SockProfile
Color
Pattern
Material
Size
```

---

## Inventory Context

Responsible for recovered socks.

### Owns

```text
FoundSock
Reservations
Inventory Matching Requests
```

---

## Auth Context

Responsible for identity.

### Owns

```text
Users
Roles
Permissions
Refresh Tokens
```

---

## Notifications Context

Responsible for user communication.

### Owns

```text
Notification
Notification History
Realtime Delivery
```

---

## Reports Context

Responsible for analytics.

### Owns

```text
Daily Statistics
Read Models
Aggregates
```

---

## Rewards Context

Responsible for reward processing.

### Owns

```text
Rewards
Payment Status
Reward History
```

---

# Aggregates

---

# SockCase

The central aggregate of the system.

Represents an investigation.

---

## Properties

```csharp
Id
CaseNumber
CatalogItemId
ReportedByUserId
LastKnownLocation
Status
CreatedAtUtc
ClosedAtUtc
```

---

## Responsibilities

```text
Track investigation state

Start investigation

Accept match confirmation

Close investigation

Maintain invariants
```

---

## Invariants

A case:

```text
Must have a sock profile
```

---

A case:

```text
Must start in Reported status
```

---

A closed case:

```text
Cannot be reopened
```

---

A matched case:

```text
Cannot have more than one active match
```

---

# Case Status

---

## Reported

A citizen reported a missing sock.

```text
Initial State
```

---

## Investigating

A detective accepted the case.

```text
Under Investigation
```

---

## Matched

A potential matching sock has been identified.

```text
Awaiting Confirmation
```

---

## Closed

The sock has been successfully recovered.

```text
Solved
```

---

## ClosedUnsolved

The investigation ended without success.

```text
Unsolved
```

---

# Allowed Transitions

```text
Reported
↓
Investigating
```

---

```text
Investigating
↓
Matched
```

---

```text
Matched
↓
Closed
```

---

```text
Investigating
↓
ClosedUnsolved
```

---

# Forbidden Transitions

```text
Closed
↓
Anything
```

---

```text
ClosedUnsolved
↓
Anything
```

---

```text
Reported
↓
Closed
```

---

```text
Reported
↓
Matched
```

---

# SockProfile

Defines what a sock looks like.

---

## Properties

```csharp
Id
Name
Color
Pattern
Size
Material
Notes
```

---

## Example

```text
Blue Athletic Sock

Color: Blue
Pattern: Striped
Material: Cotton
Size: M
```

---

# FoundSock

Represents an unpaired recovered sock.

---

## Properties

```csharp
Id
CatalogItemId
FoundLocation
FoundAtUtc
Reserved
```

---

## Rules

A found sock can be:

```text
Available
```

or

```text
Reserved
```

---

Reserved socks:

```text
Cannot be matched to another case
```

---

# SockMatch

Represents a possible recovery result.

---

## Properties

```csharp
Id
CaseId
FoundSockId
Confidence
CreatedAtUtc
Confirmed
```

---

## Confidence

Range:

```text
0.0 - 1.0
```

---

Examples:

```text
1.0 = Exact Match
```

---

```text
0.85 = Strong Match
```

---

```text
0.50 = Weak Match
```

---

# Matching Rules

Version 1:

```text
Color matches
AND
Pattern matches
AND
Size matches
AND
Material matches
```

↓

```text
Confidence = 1.0
```

---

Otherwise:

```text
Confidence = 0
```

---

Future versions may support weighted scoring.

---

# ApplicationUser

Represents a person interacting with the department.

---

## Properties

```csharp
Id
Email
DisplayName
CreatedAtUtc
```

---

# Roles

## Citizen

Can:

```text
Create cases

View own cases
```

Cannot:

```text
Investigate
Close cases
Confirm matches
```

---

## Detective

Can:

```text
Investigate cases

Search matches

Confirm matches

Close investigations
```

---

## Admin

Can:

```text
Manage users

Manage roles

Access all investigations
```

---

# Notification

Represents information delivered to users.

---

## Properties

```csharp
Id
Type
Message
CreatedAtUtc
```

---

## Types

```text
CaseCreated
InvestigationStarted
MatchFound
CaseClosed
CaseClosedUnsolved
RewardIssued
```

---

# Reward

Represents compensation for successfully identifying a missing sock.

---

## Properties

```csharp
Id
CaseId
Amount
Status
CreatedAtUtc
```

---

# Reward Status

## Pending

Created.

---

## Processing

Payment started.

---

## Completed

Payment succeeded.

---

## Failed

Payment failed.

---

# Domain Events

Domain events are facts that happened.

---

## CaseReported

```text
A new investigation has been opened.
```

---

## InvestigationStarted

```text
A detective started working on a case.
```

---

## MatchFound

```text
A potential sock pair was identified.
```

---

## MatchConfirmed

```text
A detective approved the match.
```

---

## CaseClosed

```text
The mystery has been solved.
```

---

## CaseClosedUnsolved

```text
The mystery remains unsolved.
```

---

## RewardIssued

```text
A reward was processed.
```

---

# Core Business Workflow

## Happy Path

```text
Citizen Reports Missing Sock
↓
Case Created
↓
Detective Starts Investigation
↓
Inventory Finds Match
↓
Match Confirmed
↓
Reward Issued
↓
Case Closed
↓
Reports Updated
↓
Notification Sent
```

---

## Unsolved Path

```text
Citizen Reports Missing Sock
↓
Investigation Started
↓
No Matches Found
↓
Case ClosedUnsolved
↓
Reports Updated
↓
Notification Sent
```

---

# Domain Rules

## Rule 1

Every investigation must reference exactly one sock profile.

---

## Rule 2

Every match must reference exactly one case and one recovered sock.

---

## Rule 3

A recovered sock can participate in only one active match.

---

## Rule 4

A reward can only be issued for a confirmed match.

---

## Rule 5

Only detectives may confirm a match.

---

## Rule 6

Only detectives may close a case.

---

## Rule 7

Every case must eventually end in either:

```text
Closed
```

or

```text
ClosedUnsolved
```

---

# Success Definition

A case is considered successfully resolved when:

```text
Match Found
AND
Match Confirmed
AND
Case Closed
```

At that point the missing sock has officially escaped the Sock Graveyard.