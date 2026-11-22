# SOLID in Ledger

Where each principle shows up in this codebase.

| Principle | Where | What it buys |
|---|---|---|
| OCP | `IFeePolicy` + `FlatFeePolicy`, `PercentageFeePolicy` | A new fee type is a new class; tested classes are not edited. |
| LSP | `FixedDepositAccount` ([ADR-002](adr/002-split-withdraw-isp.md)) | It could not keep `Account.Withdraw`'s promise, so a loop over accounts crashed. |
| ISP | `IWithdrawable` | Only accounts that can withdraw implement it; no class is forced to have `Withdraw`. |
| DIP | `TransferService(IAccountRepository)`, `FixedDepositAccount(IClock)` | Classes depend on interfaces; tests pass `FakeClock` to control the date. |