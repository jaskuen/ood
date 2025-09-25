using WeatherStationTests.TestClasses;

namespace WeatherStationTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CustomObserver_ObserverDeletesItselfFromList_DoesNotThrow()
    {
        DeleteObserver observer = new DeleteObserver();
        DeleteObserverObservable observable = new DeleteObserverObservable();
        observable.RegisterObserver(observer);
        
        Assert.DoesNotThrow(() => observable.Execute());
    }
}