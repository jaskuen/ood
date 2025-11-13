namespace GumballMachine.Lib;

public class NaiveGumballMachine : IGumballMachine
{
    public enum State
    {
        SoldOut, // Жвачка закончилась
        NoQuarter, // Нет монетки
        HasQuarter, // Есть монетка
        Sold // Монетка выдана
    }

    private int _ballCount;
    private int _quarterCount;
    private State _state;

    public NaiveGumballMachine(int count)
    {
        _ballCount = count;
        _state = count > 0 ? State.NoQuarter : State.SoldOut;
    }

    public void InsertQuarter()
    {
        switch (_state)
        {
            case State.SoldOut:
                Console.WriteLine("You can't insert a quarter, the machine is sold out");
                break;
            case State.NoQuarter:
                Console.WriteLine("You inserted a quarter");
                _state = State.HasQuarter;
                _quarterCount++;
                break;
            case State.HasQuarter:
                if (_quarterCount == 5)
                {
                    Console.WriteLine("You can't insert another quarter");
                }
                else
                {
                    _quarterCount++;
                }

                break;
            case State.Sold:
                Console.WriteLine("Please wait, we're already giving you a gumball");
                break;
        }
    }

    public void EjectQuarter()
    {
        switch (_state)
        {
            case State.HasQuarter:
                Console.WriteLine($"{_quarterCount} quarter{(_quarterCount == 1 ? "" : "s")} returned");
                _quarterCount = 0;
                _state = State.NoQuarter;
                break;
            case State.NoQuarter:
                Console.WriteLine("You haven't inserted a quarter");
                break;
            case State.Sold:
                Console.WriteLine("Sorry you already turned the crank");
                break;
            case State.SoldOut:
                if (_quarterCount > 0)
                {
                    Console.WriteLine($"{_quarterCount} quarter{(_quarterCount == 1 ? "" : "s")} returned");
                    _quarterCount = 0;
                    _state = State.NoQuarter;
                }
                else
                {
                    Console.WriteLine("You can't eject, you haven't inserted a quarter yet");
                }

                break;
        }
    }

    public void TurnCrank()
    {
        switch (_state)
        {
            case State.SoldOut:
                Console.WriteLine("You turned but there's no gumballs");
                break;
            case State.NoQuarter:
                Console.WriteLine("You turned but there's no quarter");
                break;
            case State.HasQuarter:
                Console.WriteLine("You turned...");
                _state = State.Sold;
                Dispense();
                break;
            case State.Sold:
                Console.WriteLine("Turning twice doesn't get you another gumball");
                break;
        }
    }

    public void Refill(int numBalls)
    {
        _ballCount += numBalls;
        _state = numBalls > 0 ? State.NoQuarter : State.SoldOut;
    }

    public override string ToString()
    {
        string state = _state switch
        {
            State.SoldOut => "sold out",
            State.NoQuarter => "waiting for quarter",
            State.HasQuarter => "waiting for turn of crank",
            State.Sold => "delivering a gumball",
            _ => throw new InvalidOperationException("Unknown state")
        };

        return
            $"""
             Mighty Gumball, Inc.
             C#-enabled Standing Gumball Model #2025
             Inventory: {_ballCount} gumball{(_ballCount > 1 ? "s" : "")}, {_quarterCount} quarter{(_quarterCount > 1 ? "s" : "")}
             Machine is {state}
             """;
    }

    private void Dispense()
    {
        switch (_state)
        {
            case State.Sold:
                Console.WriteLine("A gumball comes rolling out the slot");
                _ballCount--;
                _quarterCount--;
                if (_ballCount == 0)
                {
                    Console.WriteLine("Oops, out of gumballs");
                    _state = State.SoldOut;
                }
                else
                {
                    if (_quarterCount > 0)
                    {
                        _state = State.HasQuarter;
                    }
                    else
                    {
                        _state = State.NoQuarter;
                    }
                }

                break;
            case State.NoQuarter:
                Console.WriteLine("You need to pay first");
                break;
            case State.SoldOut:
            case State.HasQuarter:
                Console.WriteLine("No gumball dispensed");
                break;
        }
    }
}