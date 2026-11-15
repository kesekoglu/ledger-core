# ADR-003: Result for expected failures, exceptions for bugs

## Context
A transfer failed halfway and money was lost when the currencies differed. Transfer threw exceptions for normal failures, so the caller crashed when it did not catch them.

## Decision
Use Result for expected business failures, and exceptions for bugs and invalid input. We check for failures before calling Withdraw, so money cannot be lost.

## Consequences
Callers don't crash, but they can ignore the Result.