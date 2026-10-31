# ADR-002: Split Withdraw into IWithdrawable

## Context
FixedDepositAccount inherited Withdraw from Account but could not keep that promise, so the monthly fee loop crashed at runtime and the remaining accounts were never charged.

## Decision
Withdraw moved to IWithdrawable; only CurrentAccount implements it. Balance uses protected set because CurrentAccount (a subclass) must update Balance in Withdraw.

## Consequences
Putting a FixedDepositAccount into a withdrawal list is now a compile-time error. Risk: subclasses can set Balance without rules.