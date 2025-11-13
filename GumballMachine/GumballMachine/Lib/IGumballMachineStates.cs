namespace GumballMachine.Lib;

internal interface IGumballMachineStates : IGumballMachine
{
    internal void ReleaseBall();
    internal int GetBallCount();
    internal int GetQuarterCount();
    internal void AddBalls(int count);
    internal void AddQuarter();
    internal void RemoveQuarters();
    internal void SetSoldOutState();
    internal void SetNoQuarterState();
    internal void SetSoldState();
    internal void SetHasQuarterState();
}