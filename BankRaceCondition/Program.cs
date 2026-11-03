using BankRaceCondition.Models;
using BankRaceCondition.Services;

const int workers = 10;
const int operations = 1000;
const decimal amount = 10;
decimal expected = workers * operations * amount;

Console.WriteLine("=== 1. ТЕСТ ПОПОЛНЕНИЯ ===");
var accountSafe = new BankAccount();
var simulator = new BankSimulator();

await simulator.RunSafeAsync(accountSafe, workers, operations, amount);

Console.WriteLine($"Ожидаемый баланс: {expected}");
Console.WriteLine($"Фактический баланс: {accountSafe.Balance}");
Console.WriteLine($"Разница: {expected - accountSafe.Balance}\n");
Console.WriteLine("=== 2. ТЕСТ СПИСАНИЯ ===");

var withdrawalAccount = new BankAccount();

withdrawalAccount.Deposit(1000);
Console.WriteLine($"Начальный баланс: {withdrawalAccount.Balance}");

int successfulWithdrawals = await simulator.RunWithdrawalsAsync(
    withdrawalAccount,
    workers: 10,
    operationsPerWorker: 100,
    amount: 100);

Console.WriteLine($"Успешных списаний: {successfulWithdrawals}");
Console.WriteLine($"Остаток: {withdrawalAccount.Balance}");

if (withdrawalAccount.Balance < 0)
{
    Console.WriteLine("Ошибка: Баланс ушел в минус!");
}
else
{
    Console.WriteLine("Успех: Баланс корректен, овердрафта нет.");
}