namespace BankRaceCondition.Models;
public class BankAccount
{
    private decimal balance;
    private readonly object sync = new object();

    public decimal Balance
    {
        get
        {
            lock (sync)
            {
                return balance;
            }
        }
    }
    public void DepositUnsafe(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма должна быть больше нуля");
        }
        decimal currentBalance = balance;
        Thread.SpinWait(1000);
        balance = currentBalance + amount;
    }
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма должна быть больше нуля");
        }
        lock (sync)
        {
            balance += amount;
        }
    }
}