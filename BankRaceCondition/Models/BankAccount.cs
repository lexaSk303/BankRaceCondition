namespace BankRaceCondition.Models;

public class BankAccount
{
    private decimal _balance;
    private readonly object _sync = new object();

    public decimal Balance
    {
        get
        {
            lock (_sync)
            {
                return _balance;
            }
        }
    }
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма должна быть больше нуля");

        lock (_sync)
        {
            _balance += amount;
        }
    }
    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма должна быть больше нуля");
        }
        lock (_sync)
        {
            if (_balance < amount)
            {
                return false; 
            }
            _balance -= amount;
            return true; 
        }
    }
}