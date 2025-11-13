namespace GumballMachine.Lib.Implementation;

public class GumballMachine : IGumballMachineStates
{
    private IState _currentState;
    private int _ballCount;
    private int _quarterCount;

    private SoldState _soldState;
    private SoldOutState _soldOutState;
    private NoQuarterState _noQuarterState;
    private HasQuarterState _hasQuarterState;

    public GumballMachine(int gumballCount)
    {
        _soldState = new SoldState(this);
        _soldOutState = new SoldOutState(this);
        _noQuarterState = new NoQuarterState(this);
        _hasQuarterState = new HasQuarterState(this);

        _ballCount = gumballCount;
        _currentState = _soldOutState;

        if (_ballCount > 0)
        {
            _currentState = _noQuarterState;
        }
    }

    public void InsertQuarter()
    {
        _currentState.InsertQuarter();
    }

    public void EjectQuarter()
    {
        _currentState.EjectQuarter();
    }

    public void TurnCrank()
    {
        _currentState.TurnCrank();
        _currentState.Dispense();
    }

    public void Refill(int count)
    {
        _currentState.Refill(count);
    }

    public override string ToString()
    {
        return $"""
               Mighty Gumball, Inc.
               C#-enabled Standing Gumball Model #2025
               Inventory: {_ballCount} gumball{(_ballCount > 1 ? "s" : "")}, {_quarterCount} quarter{(_quarterCount > 1 ? "s" : "")}
               Machine is {_currentState.ToString()}
               """;
    }

    public void ReleaseBall()
    {
        if (_ballCount > 0)
        {
            Console.WriteLine("A gumball comes rolling out the slot...");
            --_ballCount;
            --_quarterCount;
        }
    }

    public int GetBallCount()
    {
        return _ballCount;
    }

    public int GetQuarterCount()
    {
        return _quarterCount;
    }

    public void AddBalls(int count)
    {
        _ballCount += count;
    }

    public void AddQuarter()
    {
        _quarterCount += 1;
    }

    public void RemoveQuarters()
    {
        _quarterCount = 0;
    }

    public void SetSoldOutState()
    {
        _currentState = _soldOutState;
    }

    public void SetNoQuarterState()
    {
        _currentState = _noQuarterState;
    }

    public void SetSoldState()
    {
        _currentState = _soldState;
    }

    public void SetHasQuarterState()
    {
        _currentState = _hasQuarterState;
    }
}