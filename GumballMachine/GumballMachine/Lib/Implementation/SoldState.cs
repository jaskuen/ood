namespace GumballMachine.Lib.Implementation;

internal class SoldState : IState
{
    private readonly IGumballMachineStates _gumballMachine;

    public SoldState(IGumballMachineStates gumballMachine)
    {
        _gumballMachine = gumballMachine;
    }

    public void InsertQuarter()
    {
        Console.WriteLine("Please wait, we're already giving you a gumball");
    }

    public void EjectQuarter()
    {
        Console.WriteLine("Sorry, you already turned the crank");
    }

    public void TurnCrank()
    {
        Console.WriteLine("Turning twice doesn't get you another gumball");
    }

    public void Dispense()
    {
        _gumballMachine.ReleaseBall();
        if (_gumballMachine.GetBallCount() == 0)
        {
            Console.WriteLine("Oops, out of gumballs");
            _gumballMachine.SetSoldOutState();
        }
        else
        {
            if (_gumballMachine.GetQuarterCount() > 0)
            {
                _gumballMachine.SetHasQuarterState();
            }
            else
            {
                _gumballMachine.SetNoQuarterState();
            }
        }
    }

    public void Refill(int count)
    {
        Console.WriteLine("Please wait until we give you a gumball");
    }

    public override string ToString()
    {
        return "delivering a gumball";
    }
}