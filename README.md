# Ledger Core

A high-integrity, domain-driven financial ledger engine built in C# and .NET.

## Architecture

This solution follows Clean Architecture and Domain-Driven Design (DDD) principles:

- **`Ledger.Domain`**: Core domain models, invariants, Value Objects (e.g., `Money`), domain exceptions, and business rules with zero external dependencies.
- **`Ledger.Tests`**: Unit and domain invariant test suite ensuring 100% verification of domain behaviors and edge cases.

## Tech Stack
- **Language:** C# 13 / .NET 10
- **Testing:** xUnit
- **Design Paradigm:** Domain-Driven Design (DDD), Clean Architecture
