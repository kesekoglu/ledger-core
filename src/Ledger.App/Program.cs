using Ledger.Domain;

var systemClock = new SystemClock();
var repo = new InMemoryAccountRepository();

var initialBalance = new Money(100m, "TRY");
var transferAmount = new Money(30m, "TRY");
var dateTime = new DateTime(2030, 1, 1);

var cAccount1 = new CurrentAccount(initialBalance, 1);
var cAccount2 = new CurrentAccount(initialBalance, 2);
var fAccount1 = new FixedDepositAccount(initialBalance, dateTime, systemClock, 3);

repo.AddAccount(cAccount1);
repo.AddAccount(cAccount2);
repo.AddAccount(fAccount1);

var service = new TransferService(repo);

service.Transfer(3, 1, transferAmount);

Console.WriteLine(cAccount1.Balance.Amount);
Console.WriteLine(fAccount1.Balance.Amount);
