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
        int danceCount = 0;
        Mock.Get(_danceBehaviour)
            .Setup(x => x.Dance())
            .Callback(() => danceCount++ );

        IDuck duck = new Duck(_quackBehaviour, _flyBehaviour, _danceBehaviour);
        // Act
        duck.Dance();

        // Assert
        Assert.That(danceCount, Is.EqualTo(1));
    }

    [Test]
    public void TestFly_QuacksOnFlight()
    {
        // Arrange
        int quackCount = 0;
        IDuck duck = new Duck(_quackBehaviour, _flyBehaviour, _danceBehaviour);

        Mock.Get(_quackBehaviour)
            .Setup(x => x.Quack())
            .Callback(() => quackCount++);

        // Act
        duck.Fly();

        // Assert
        Assert.That(quackCount, Is.EqualTo(1));
    }
}