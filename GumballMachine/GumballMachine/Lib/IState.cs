namespace GumballMachine.Lib;

public interface IState
{
    public void InsertQuarter();
    public void EjectQuarter();
    public void TurnCrank();
    public void Refill(int count);
    public void Dispense();
    public string ToString();
}