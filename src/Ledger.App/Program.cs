using Ledger.Domain;

var systemClock = new SystemClock();
var repo = new InMemoryAccountRepository();

var initialBalance = new Money(100m, "TRY");
var transferAmount = new Money(30m, "TRY");
var maturityDate = new DateTime(2025, 1, 1);

var currentAccount = new CurrentAccount(initialBalance, 1);
var fixedDepositAccount = new FixedDepositAccount(initialBalance, maturityDate, systemClock, 3);

repo.AddAccount(currentAccount);
repo.AddAccount(fixedDepositAccount);

var service = new TransferService(repo);

service.Transfer(fixedDepositAccount.Id, currentAccount.Id, transferAmount);

Console.WriteLine(currentAccount.Balance.Amount);
Console.WriteLine(fixedDepositAccount.Balance.Amount);
