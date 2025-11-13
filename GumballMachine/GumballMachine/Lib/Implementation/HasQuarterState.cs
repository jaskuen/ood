namespace GumballMachine.Lib.Implementation;

internal class HasQuarterState : IState
{
    private readonly IGumballMachineStates _gumballMachine;

    public HasQuarterState(IGumballMachineStates gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        if (_gumballMachine.GetQuarterCount() == 5)
        {
            Console.WriteLine("You can't insert another quarter");
        }
        else
        {
            _gumballMachine.AddQuarter();
        }
    }

    public void EjectQuarter()
    {
        int quarterCount = _gumballMachine.GetQuarterCount();
        Console.WriteLine($"{quarterCount} quarter{(quarterCount == 1 ? "" : "s")} returned");
        _gumballMachine.RemoveQuarters();
        _gumballMachine.SetNoQuarterState();
    }

    public void TurnCrank()
    {
        Console.WriteLine("You turned...");
        _gumballMachine.SetSoldState();
    }

    public void Dispense()
    {
        Console.WriteLine("No gumball dispensed");
    }

    public void Refill(int count)
    {
        Console.WriteLine($"Refilling a machine with {count} gumball{(count == 1 ? "" : "s")}...");
        _gumballMachine.AddBalls(count);
    }

    public override string ToString()
    {
        return "waiting for turn of crank";
    }
}