# Ledger Core

A small banking ledger in C# / .NET 10. Portfolio project focused on OOP, SOLID and unit testing:
money, accounts and transfers between accounts.

## What it does
- `Money`: immutable value object (decimal amount + 3-letter currency). Rejects negative amounts;
  mixing currencies throws `CurrencyMismatchException`.
- Accounts: abstract `Account` (equality by `Id`), `CurrentAccount`, and `FixedDepositAccount`
  (withdraw only after the maturity date, read from an injected `IClock`).
- `TransferService`: moves money between accounts found through `IAccountRepository`.
  `Transfer` throws on failure; `TryTransfer` returns a `Result`.
- Fee policies: `IFeePolicy` with flat and percentage implementations.

## Projects
- `Ledger.Domain`: domain model and services, no external packages.
- `Ledger.App`: console app; composition root that wires the system clock, the in-memory
  repository and the transfer service.
- `Ledger.Tests`: 39 xUnit tests, including failure cases.

## Design decisions
- [ADR-002: Split withdraw into IWithdrawable](docs/adr/002-split-withdraw-isp.md)
- [ADR-003: Exceptions vs Result](docs/adr/003-exceptions-vs-result.md)
- [Money: class vs record](docs/money-class-vs-record.md)

## Run
    dotnet test src/Ledger.Tests/Ledger.Tests.csproj
    dotnet run --project src/Ledger.App

## Known limitations
- Data lives only in memory (`InMemoryAccountRepository`); everything is lost when the app stops.
- The repository does not reject duplicate Ids: two accounts with the same `Id` can be added,
  and `GetById` returns the first one.

## Tech
C# 14 / .NET 10, xUnit
