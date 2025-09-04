using Ducks.DDuck;
using Ducks.DDuck.Behaviour.Dance;
using Ducks.DDuck.Behaviour.Dance.Implementation;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Behaviour.Fly.Implementation;
using Ducks.DDuck.Behaviour.Quack;
using Ducks.DDuck.Behaviour.Quack.Implementation;
using Ducks.DDuck.Implementation;
using Moq;

namespace Ducks.Tests;

public class DuckTests
{
    private readonly IFlyBehaviour _flyBehaviour = Mock.Of<IFlyBehaviour>();
    private readonly IQuackBehaviour _quackBehaviour = Mock.Of<IQuackBehaviour>();
    private readonly IDanceBehaviour _danceBehaviour = Mock.Of<IDanceBehaviour>();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestDance_DuckDanced()
    {
        // Arrange
        bool duckDanced = false;
        Mock.Get(_danceBehaviour)
            .Setup(x => x.Dance())
            .Callback(() => duckDanced = true);

        IDuck duck = new Duck(_quackBehaviour, _flyBehaviour, _danceBehaviour);
        // Act
        duck.Dance();

        // Assert
        Assert.IsTrue(duckDanced);
    }

    [Test]
    public void TestFly_QuacksOnFlight()
    {
        // Arrange
        bool quacked = false;
        IDuck duck = new Duck(_quackBehaviour, _flyBehaviour, _danceBehaviour);

        Mock.Get(_quackBehaviour)
            .Setup(x => x.Quack())
            .Callback(() => quacked = !duck.IsQuackingOnEvenFlight());

        // Act
        duck.Fly();

        // Assert
        Assert.IsTrue(quacked);
    }
}