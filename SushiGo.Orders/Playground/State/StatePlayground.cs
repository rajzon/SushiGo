namespace Playground.State;

internal sealed class StatePlayground : IPlayground
{
    public void Run()
    {
        Console.WriteLine("---------------- StatePlayground --------------");
        var account = new Account(100);
        account.MakeTransaction(80);
        account.MakeTransaction(100);
        account.MakeTransaction(100);
        account.TakeTransaction(70);
        account.TakeTransaction(10);
        account.TakeTransaction(10);
    }
}

public interface IAccountState
{
    void MakeTransaction(Account account, decimal balance);
    void TakeTransaction(Account account, decimal balance);
}

public class Account(decimal balance)
{
    public decimal Balance { get; private set; } = balance;
    private IAccountState _state { get; set; } = balance > 0 ? new NormalAccountState() : new InDebtAccountState();

    public void MakeTransaction(decimal balance)
    {
        _state.MakeTransaction(this, balance);
    }

    public void TakeTransaction(decimal balance)
    {
        _state.TakeTransaction(this, balance);
    }

    public void AddBalance(decimal balance)
    {
        Balance += balance;
    }

    public void RemoveBalance(decimal balance)
    {
        Balance -= balance;
    }

    public void SetState(IAccountState state)
    {
        _state = state;
    }
}

public class InDebtAccountState : IAccountState
{
    public void MakeTransaction(Account account, decimal balance)
    {
        if (account.Balance < balance)
        {
            Console.WriteLine($"InDebtAccountState: Making transaction failed, balance: {account.Balance}");
        }
        
        if (account.Balance >= balance)
        {
            account.RemoveBalance(balance);
            Console.WriteLine($"InDebtAccountState: Making transaction setting state to NormalAccountState, balance: {account.Balance}");
            account.SetState(new NormalAccountState());
            return;
        }
    }

    public void TakeTransaction(Account account, decimal balance)
    {
        account.AddBalance(balance);
        Console.WriteLine($"InDebtAccountState: Taking transaction, balance: {account.Balance}");
        if (account.Balance >= 0)
        {
            Console.WriteLine($"InDebtAccountState: Take transaction setting state to NormalAccountState");
            account.SetState(new NormalAccountState());
        }
    }
}

public class NormalAccountState : IAccountState
{
    public void MakeTransaction(Account account, decimal balance)
    {
        account.RemoveBalance(balance);
        Console.WriteLine($"NormalAccountState: Making transaction, balance: {account.Balance}");

        if (account.Balance < 0)
        {
            Console.WriteLine($"NormalAccountState: Set State to InDebtAccountState");
            account.SetState(new InDebtAccountState());
        }
    }

    public void TakeTransaction(Account account, decimal balance)
    {
        account.AddBalance(balance);
        Console.WriteLine($"NormalAccountState: Taking transaction, balance: {account.Balance}");
    }
}