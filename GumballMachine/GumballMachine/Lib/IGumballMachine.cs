namespace GumballMachine.Lib;

public interface IGumballMachine
{
    public void InsertQuarter();
    public void EjectQuarter();
    public void TurnCrank();
    public void Refill(int count);
}