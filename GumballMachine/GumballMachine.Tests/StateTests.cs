using GumballMachine.Lib;
using GumballMachine.Lib.Implementation;
using Moq;

namespace GumballMachine.Tests;

[TestFixture]
public class HasQuarterStateTests
{
    private Mock<IGumballMachineStates> _mockGumballMachine;
    private HasQuarterState _state;

    [SetUp]
    public void SetUp()
    {
        _mockGumballMachine = new Mock<IGumballMachineStates>();
        _state = new HasQuarterState(_mockGumballMachine.Object);
    }

    [Test]
    public void InsertQuarter_WithFewerThanFiveQuarters_CallsAddQuarter()
    {
        // Arrange
        _mockGumballMachine.Setup(m => m.GetQuarterCount()).Returns(3);

        // Act
        _state.InsertQuarter();

        // Assert
        _mockGumballMachine.Verify(m => m.AddQuarter(), Times.Once());
    }

    [Test]
    public void InsertQuarter_WithFiveQuarters_DoesNotCallAddQuarter()
    {
        // Arrange
        _mockGumballMachine.Setup(m => m.GetQuarterCount()).Returns(5);

        // Act
        _state.InsertQuarter();

        // Assert
        _mockGumballMachine.Verify(m => m.AddQuarter(), Times.Never());
    }

    [Test]
    public void EjectQuarter_CallsRemoveQuartersAndSetNoQuarterState()
    {
        // Arrange
        _mockGumballMachine.Setup(m => m.GetQuarterCount()).Returns(2);

        // Act
        _state.EjectQuarter();

        // Assert
        _mockGumballMachine.Verify(m => m.RemoveQuarters(), Times.Once());
        _mockGumballMachine.Verify(m => m.SetNoQuarterState(), Times.Once());
    }

    [Test]
    public void TurnCrank_CallsSetSoldState()
    {
        // Act
        _state.TurnCrank();

        // Assert
        _mockGumballMachine.Verify(m => m.SetSoldState(), Times.Once());
    }

    [Test]
    public void Dispense_DoesNotInteractWithMachine()
    {
        // Act
        _state.Dispense();

        // Assert
        _mockGumballMachine.VerifyNoOtherCalls();
    }

    [Test]
    public void Refill_CallsAddBallsWithCorrectCount()
    {
        // Arrange
        int refillCount = 10;

        // Act
        _state.Refill(refillCount);

        // Assert
        _mockGumballMachine.Verify(m => m.AddBalls(refillCount), Times.Once());
    }

    [Test]
    public void ToString_ReturnsCorrectString()
    {
        // Act
        string result = _state.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("waiting for turn of crank"));
    }
}

[TestFixture]
public class NoQuarterStateTests
{
    private Mock<IGumballMachineStates> _mockGumballMachine;
    private NoQuarterState _state;

    [SetUp]
    public void SetUp()
    {
        _mockGumballMachine = new Mock<IGumballMachineStates>();
        _state = new NoQuarterState(_mockGumballMachine.Object);
    }

    [Test]
    public void InsertQuarter_CallsAddQuarterAndSetHasQuarterState()
    {
        // Act
        _state.InsertQuarter();

        // Assert
        _mockGumballMachine.Verify(m => m.AddQuarter(), Times.Once());
        _mockGumballMachine.Verify(m => m.SetHasQuarterState(), Times.Once());
    }

    [Test]
    public void EjectQuarter_DoesNotInteractWithMachine()
    {
        // Act
        _state.EjectQuarter();

        // Assert
        _mockGumballMachine.VerifyNoOtherCalls();
    }

    [Test]
    public void TurnCrank_DoesNotInteractWithMachine()
    {
        // Act
        _state.TurnCrank();

        // Assert
        _mockGumballMachine.VerifyNoOtherCalls();
    }

    [Test]
    public void Dispense_DoesNotInteractWithMachine()
    {
        // Act
        _state.Dispense();

        // Assert
        _mockGumballMachine.VerifyNoOtherCalls();
    }

    [Test]
    public void Refill_CallsAddBallsWithCorrectCount()
    {
        // Arrange
        int refillCount = 10;

        // Act
        _state.Refill(refillCount);

        // Assert
        _mockGumballMachine.Verify(m => m.AddBalls(refillCount), Times.Once());
    }

    [Test]
    public void ToString_ReturnsCorrectString()
    {
        // Act
        string result = _state.ToString();

        // Assert
        Assert.That(result, Is.EqualTo("waiting for quarter"));
    }
}

public class SoldOutStateTests
{
    private Mock<IGumballMachineStates> _mockGumballMachine;
    private SoldOutState _state;

    [SetUp]
    public void SetUp()
    {
        _mockGumballMachine = new Mock<IGumballMachineStates>();
        _state = new SoldOutState(_mockGumballMachine.Object);
    }

    [Test]
    public void InsertQuarter_DoesNotInteractWithMachine()
    {
        // Act
        _state.InsertQuarter();
    }

    [Test]
    public void EjectQuarter_DoesNotInteractWithMachine()
    {
        // Act
        _state.EjectQuarter();
    }

    [Test]
    public void TurnCrank_DoesNotInteractWithMachine()
    {
        // Act
        _state.TurnCrank();
    }

    [Test]
    public void Dispense_DoesNotInteractWithMachine()
    {
        // Act
        _state.Dispense();
    }

    [Test]
    public void Refill_WithNoQuarters_CallsAddBallsAndSetsNoQuarterState()
    {
        // Act
        int refillCount = 10;
        _mockGumballMachine.Setup(m => m.GetQuarterCount()).Returns(0);

        _state.Refill(refillCount);

        // Assert
        _mockGumballMachine.Verify(m => m.AddBalls(refillCount), Times.Once());
        _mockGumballMachine.Verify(m => m.SetNoQuarterState(), Times.Once());
    }

    [Test]
    public void Refill_WithQuarters_CallsAddBallsAndSetsHasQuarterState()
    {
        int refillCount = 5;
        _mockGumballMachine.Setup(m => m.GetQuarterCount()).Returns(2);

        _state.Refill(refillCount);

        _mockGumballMachine.Verify(m => m.AddBalls(refillCount), Times.Once());
        _mockGumballMachine.Verify(m => m.SetHasQuarterState(), Times.Once());
    }

    [Test]
    public void ToString_ReturnsCorrectString()
    {
        string result = _state.ToString();
        Assert.That(result, Is.EqualTo("sold out"));
    }
}

[TestFixture]
public class SoldStateTests
{
    private Mock<IGumballMachineStates> _mockGumballMachine;
    private SoldState _state;

    [SetUp]
    public void SetUp()
    {
        _mockGumballMachine = new Mock<IGumballMachineStates>();
        _state = new SoldState(_mockGumballMachine.Object);
    }

    [Test]
    public void InsertQuarter_DoesNotInteractWithMachine()
    {
        _state.InsertQuarter();
    }

    [Test]
    public void EjectQuarter_DoesNotInteractWithMachine()
    {
        _state.EjectQuarter();
    }

    [Test]
    public void TurnCrank_DoesNotInteractWithMachine()
    {
        _state.TurnCrank();
    }

    [Test]
    public void Dispense_WithNoBallsLeft_CallsReleaseBallAndSetsSoldOutState()
    {
        _mockGumballMachine.Setup(m => m.GetBallCount()).Returns(0);
        _mockGumballMachine.Setup(m => m.GetQuarterCount()).Returns(0);

        _state.Dispense();

        _mockGumballMachine.Verify(m => m.ReleaseBall(), Times.Once());
        _mockGumballMachine.Verify(m => m.SetSoldOutState(), Times.Once());
    }

    [Test]
    public void Dispense_WithBallsAndNoQuarters_CallsReleaseBallAndSetsNoQuarterState()
    {
        _mockGumballMachine.Setup(m => m.GetBallCount()).Returns(1);
        _mockGumballMachine.Setup(m => m.GetQuarterCount()).Returns(0);

        _state.Dispense();

        _mockGumballMachine.Verify(m => m.ReleaseBall(), Times.Once());
        _mockGumballMachine.Verify(m => m.SetNoQuarterState(), Times.Once());
    }

    [Test]
    public void Dispense_WithBallsAndQuarters_CallsReleaseBallAndSetsHasQuarterState()
    {
        _mockGumballMachine.Setup(m => m.GetBallCount()).Returns(1);
        _mockGumballMachine.Setup(m => m.GetQuarterCount()).Returns(2);

        _state.Dispense();

        _mockGumballMachine.Verify(m => m.ReleaseBall(), Times.Once());
        _mockGumballMachine.Verify(m => m.SetHasQuarterState(), Times.Once());
    }

    [Test]
    public void Refill_DoesNotInteractWithMachine()
    {
        _state.Refill(10);
    }

    [Test]
    public void ToString_ReturnsCorrectString()
    {
        string result = _state.ToString();
        Assert.That(result, Is.EqualTo("delivering a gumball"));
    }
}