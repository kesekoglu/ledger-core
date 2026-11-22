# ADR-001: Fee calculation with Strategy (IFeePolicy) instead of a switch

## Context
Fees were first calculated in one `FeeCalculator` with a `switch` on the fee type (012a2f3).
Every new fee type meant editing that class, which was already tested.

## Decision
Each fee type is its own class behind `IFeePolicy` (`FlatFeePolicy`, `PercentageFeePolicy`, 1d9887f).
The switch-based `FeeCalculator` was deleted (b114cd5), so each rule lives in one place.

## Consequences
A third policy (`TieredFeePolicy`) was added in the tests without touching `Ledger.Domain`.
Risks: more small classes; nothing in the domain uses `IFeePolicy` yet; the percentage fee is not rounded.