using GumballMachine.Lib;
using GumballMachine.Lib.Implementation;

namespace GumballMachine.Tests;

public abstract class GumballMachineTestsBase
{
    private IGumballMachine _machine;

    private enum State
    {
        SoldOut = 0,
        NoQuarter = 1,
        HasQuarter = 2,
        Sold = 3
    }

    [SetUp]
    public void SetUp()
    {
        _machine = CreateGumballMachine(5); // Starts with 5 gumballs, NoQuarter
    }
    
    protected abstract IGumballMachine CreateGumballMachine(int ballCount);

    private State GetCurrentState()
    {
        string toStringOutput = _machine.ToString();
        if (toStringOutput.Contains("sold out")) return State.SoldOut;
        if (toStringOutput.Contains("waiting for quarter")) return State.NoQuarter;
        if (toStringOutput.Contains("waiting for turn of crank")) return State.HasQuarter;
        if (toStringOutput.Contains("delivering a gumball")) return State.Sold;
        throw new InvalidOperationException("Unknown state");
    }

    private int GetInventory()
    {
        string toStringOutput = _machine.ToString();
        var lines = toStringOutput.Split('\n');
        var inventoryLine = lines[2]; // "Inventory: X gumball(s), Y quarter(s)"
        var countStr = inventoryLine.Split(':')[1].Split(' ')[1];
        return int.Parse(countStr);
    }

    private int GetQuarters()
    {
        string toStringOutput = _machine.ToString();
        var lines = toStringOutput.Split('\n');
        var inventoryLine = lines[2]; // "Inventory: X gumball(s), Y quarter(s)"
        var countStr = inventoryLine.Split(',')[1].Split(' ')[1];
        return int.Parse(countStr);
    }

    [Test]
    public void Constructor_WithZeroGumballs_SetsSoldOutState()
    {
        _machine = new Lib.Implementation.GumballMachine(0);
        Assert.That(GetCurrentState(), Is.EqualTo(State.SoldOut));
        Assert.That(GetInventory(), Is.EqualTo(0));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void Constructor_WithGumballs_SetsNoQuarterState()
    {
        Assert.That(GetCurrentState(), Is.EqualTo(State.NoQuarter));
        Assert.That(GetInventory(), Is.EqualTo(5));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void InsertQuarter_InNoQuarterState_AddsQuarterAndSetsHasQuarter()
    {
        _machine.InsertQuarter();
        Assert.That(GetCurrentState(), Is.EqualTo(State.HasQuarter));
        Assert.That(GetQuarters(), Is.EqualTo(1));
    }

    [Test]
    public void InsertQuarter_InHasQuarterState_AddsQuarterUpToFive()
    {
        _machine.InsertQuarter(); // 1 quarter
        _machine.InsertQuarter(); // 2 quarters
        _machine.InsertQuarter(); // 3 quarters
        _machine.InsertQuarter(); // 4 quarters
        _machine.InsertQuarter(); // 5 quarters
        Assert.That(GetCurrentState(), Is.EqualTo(State.HasQuarter));
        Assert.That(GetQuarters(), Is.EqualTo(5));
    }

    [Test]
    public void InsertQuarter_InHasQuarterStateWithFiveQuarters_DoesNotAddMore()
    {
        _machine.InsertQuarter();
        for (int i = 0; i < 4; i++) _machine.InsertQuarter(); // 5 quarters
        _machine.InsertQuarter(); // Try to add 6th
        Assert.That(GetCurrentState(), Is.EqualTo(State.HasQuarter));
        Assert.That(GetQuarters(), Is.EqualTo(5));
    }

    [Test]
    public void InsertQuarter_InSoldOutState_DoesNotChangeStateOrQuarters()
    {
        _machine = new Lib.Implementation.GumballMachine(0);
        _machine.InsertQuarter();
        Assert.That(GetCurrentState(), Is.EqualTo(State.SoldOut));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void EjectQuarter_InHasQuarterState_ReturnsAllQuartersAndSetsNoQuarter()
    {
        _machine.InsertQuarter();
        _machine.InsertQuarter(); // 2 quarters
        _machine.EjectQuarter();
        Assert.That(GetCurrentState(), Is.EqualTo(State.NoQuarter));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void EjectQuarter_InNoQuarterState_DoesNotChangeStateOrQuarters()
    {
        _machine.EjectQuarter();
        Assert.That(GetCurrentState(), Is.EqualTo(State.NoQuarter));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void EjectQuarter_InSoldOutState_DoesNotChangeStateOrQuarters()
    {
        _machine = new Lib.Implementation.GumballMachine(0);
        _machine.EjectQuarter();
        Assert.That(GetCurrentState(), Is.EqualTo(State.SoldOut));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void TurnCrank_InHasQuarterState_DispensesGumballAndSetsNoQuarterIfNoQuartersLeft()
    {
        _machine.InsertQuarter(); // 1 quarter
        _machine.TurnCrank();
        Assert.That(GetCurrentState(), Is.EqualTo(State.NoQuarter));
        Assert.That(GetInventory(), Is.EqualTo(4));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void TurnCrank_InHasQuarterStateWithMultipleQuarters_StaysInHasQuarter()
    {
        _machine.InsertQuarter();
        _machine.InsertQuarter(); // 2 quarters
        _machine.TurnCrank();
        Assert.That(GetCurrentState(), Is.EqualTo(State.HasQuarter));
        Assert.That(GetInventory(), Is.EqualTo(4));
        Assert.That(GetQuarters(), Is.EqualTo(1));
    }

    [Test]
    public void TurnCrank_InHasQuarterStateWithOneGumball_SetsSoldOut()
    {
        _machine = new Lib.Implementation.GumballMachine(1);
        _machine.InsertQuarter();
        _machine.TurnCrank();
        Assert.That(GetCurrentState(), Is.EqualTo(State.SoldOut));
        Assert.That(GetInventory(), Is.EqualTo(0));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void TurnCrank_InNoQuarterState_DoesNotChangeStateOrInventory()
    {
        _machine.TurnCrank();
        Assert.That(GetCurrentState(), Is.EqualTo(State.NoQuarter));
        Assert.That(GetInventory(), Is.EqualTo(5));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void TurnCrank_InSoldOutState_DoesNotChangeStateOrInventory()
    {
        _machine = new Lib.Implementation.GumballMachine(0);
        _machine.TurnCrank();
        Assert.That(GetCurrentState(), Is.EqualTo(State.SoldOut));
        Assert.That(GetInventory(), Is.EqualTo(0));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void Refill_FromSoldOut_SetsNoQuarterAndUpdatesInventory()
    {
        _machine = new Lib.Implementation.GumballMachine(0);
        _machine.Refill(10);
        Assert.That(GetCurrentState(), Is.EqualTo(State.NoQuarter));
        Assert.That(GetInventory(), Is.EqualTo(10));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void Refill_FromNoQuarter_UpdatesInventoryAndStaysInNoQuarter()
    {
        _machine.Refill(20);
        Assert.That(GetCurrentState(), Is.EqualTo(State.NoQuarter));
        Assert.That(GetInventory(), Is.EqualTo(25));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void Refill_WithZeroGumballs_SetsSoldOutAndResetsQuarters()
    {
        _machine = new Lib.Implementation.GumballMachine(0);

        _machine.InsertQuarter();
        Assert.That(GetCurrentState(), Is.EqualTo(State.SoldOut));
        Assert.That(GetInventory(), Is.EqualTo(0));
        Assert.That(GetQuarters(), Is.EqualTo(0));
    }

    [Test]
    public void ToString_ReturnsCorrectFormat()
    {
        var expected =
            """
            Mighty Gumball, Inc.
            C#-enabled Standing Gumball Model #2025
            Inventory: 5 gumballs, 0 quarter
            Machine is waiting for quarter
            """;
        Assert.That(_machine.ToString(), Is.EqualTo(expected));
    }
}

public class StateGumballMachineTests : GumballMachineTestsBase
{
    protected override IGumballMachine CreateGumballMachine(int ballCount)
    {
        return new Lib.Implementation.GumballMachine(ballCount);
    }
}

public class NaiveGumballMachineTests : GumballMachineTestsBase
{
    protected override IGumballMachine CreateGumballMachine(int ballCount)
    {
        return new NaiveGumballMachine(ballCount);
    }
}