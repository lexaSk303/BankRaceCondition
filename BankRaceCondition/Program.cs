using BankRaceCondition.Models;
using BankRaceCondition.Services;

const int workers = 10;
const int operations = 1000;
const decimal amount = 10;

decimal expected = workers * operations * amount;

Console.WriteLine("=== BANK RACE CONDITION ===\n");
Console.WriteLine($"Количество задач: {workers}");
Console.WriteLine($"Операций на задачу: {operations}");
Console.WriteLine($"Сумма пополнения: {amount}");
Console.WriteLine($"Ожидаемый баланс: {expected}\n");

var simulator = new BankSimulator();
Console.WriteLine("--- 1. Запуск без синхронизации ---");
var accountUnsafe = new BankAccount();

await simulator.RunUnsafeAsync(accountUnsafe, workers, operations, amount);

Console.WriteLine($"Фактический баланс: {accountUnsafe.Balance}");
Console.WriteLine($"Разница: {expected - accountUnsafe.Balance}\n");

Console.WriteLine("--- 2. Запуск С синхронизацией ---");
var accountSafe = new BankAccount();

await simulator.RunSafeAsync(accountSafe, workers, operations, amount);

Console.WriteLine($"Фактический баланс: {accountSafe.Balance}");
Console.WriteLine($"Разница: {expected - accountSafe.Balance}");
