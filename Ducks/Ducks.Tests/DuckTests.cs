using Ducks.DDuck;
using Ducks.DDuck.Behaviour.Dance;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Behaviour.Quack;
using Ducks.DDuck.Implementation;
using Moq;

namespace Ducks.Tests;

public class DuckTests
{
    private Tuple<Action, bool> _flyBehaviour;
    private Action _quackBehaviour;
    private Action _danceBehaviour;

    [SetUp]
    public void Setup()
    {
        _flyBehaviour = FlyBehaviourFactory.FlyWithWings();
        _quackBehaviour = QuackBehaviourFactory.QuackBehaviour();
        _danceBehaviour = DanceBehaviourFactory.NoDance();
    }

    [Test]
    public void TestDance_DuckDanced()
    {
        // Arrange
        var duckDanced = false;
        _danceBehaviour = () => { duckDanced = true; };

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
        var quacked = false;
        _quackBehaviour = () => quacked = true;
        
        IDuck duck = new Duck(_quackBehaviour, _flyBehaviour, _danceBehaviour);

        // Act
        duck.Fly();

        // Assert
        Assert.IsTrue(quacked == !duck.IsQuackingOnEvenFlight());
    }
}