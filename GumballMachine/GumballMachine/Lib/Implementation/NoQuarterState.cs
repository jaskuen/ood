namespace GumballMachine.Lib.Implementation;

internal class NoQuarterState : IState
{
    private readonly IGumballMachineStates _gumballMachine;

    public NoQuarterState(IGumballMachineStates gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        Console.WriteLine("You inserted a quarter");
        _gumballMachine.AddQuarter();
        _gumballMachine.SetHasQuarterState();
    }

    public void EjectQuarter()
    {
        Console.WriteLine("You haven't inserted a quarter");
    }

    public void TurnCrank()
    {
        Console.WriteLine("You turned but there's no quarter");
    }

    public void Dispense()
    {
        Console.WriteLine("You need to pay first");
    }

    public void Refill(int count)
    {
        Console.WriteLine($"Refilling a machine with {count} gumball{(count == 1 ? "" : "s")}...");
        _gumballMachine.AddBalls(count);
    }

    public override string ToString()
    {
        return "waiting for quarter";
    }
}