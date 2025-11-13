namespace GumballMachine.Lib.Implementation;

internal class SoldOutState : IState
{
    private readonly IGumballMachineStates _gumballMachine;

    public SoldOutState(IGumballMachineStates gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        Console.WriteLine("You can't insert a quarter, the machine is sold out");
    }

    public void EjectQuarter()
    {
        Console.WriteLine("You can't eject, you haven't inserted a quarter yet");
    }

    public void TurnCrank()
    {
        Console.WriteLine("You turned but there's no gumballs");
    }

    public void Dispense()
    {
        Console.WriteLine("No gumball dispensed");
    }

    public void Refill(int count)
    {
        Console.WriteLine($"Refilling a machine with {count} gumball{(count == 1 ? "" : "s")}...");
        _gumballMachine.AddBalls(count);
        if (_gumballMachine.GetQuarterCount() > 0)
        {
            _gumballMachine.SetHasQuarterState();
        }
        else
        {
            _gumballMachine.SetNoQuarterState();
        }
    }

    public override string ToString()
    {
        return "sold out";
    }
}