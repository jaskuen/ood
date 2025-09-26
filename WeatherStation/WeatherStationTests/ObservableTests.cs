using Moq;
using WeatherStation.Lib;
using WeatherStationTests.TestClasses;

namespace WeatherStationTests;

public class Tests
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Observer_ObserverDeletesItselfFromList_DoesNotThrow()
    {
        DeleteObserver observer = new DeleteObserver();
        DeleteObserverObservable observable = new DeleteObserverObservable();
        observable.RegisterObserver(observer);

        Assert.DoesNotThrow(() => observable.Execute());
    }

    [Test]
    public void Observer_SetPriority_CorrectOrder()
    {
        // В тесте задаем порядок для наблюдателей: 3 1 2
        // Он должен быть в итоговой строке
        
        ICustomObserver<Empty> observer1 = Mock.Of<ICustomObserver<Empty>>();
        ICustomObserver<Empty> observer2 = Mock.Of<ICustomObserver<Empty>>();
        ICustomObserver<Empty> observer3 = Mock.Of<ICustomObserver<Empty>>();
        SimpleObservable observable = new SimpleObservable();

        string expected = "312";
        string result = String.Empty;
        
        // Добавляем номер наблюдателя (не его приоритет) в результирующую строку
        Mock.Get(observer1)
            .Setup(x => x.Update(It.IsAny<Empty>()))
            .Callback<Empty>(_ => result += "1");
        Mock.Get(observer2)
            .Setup(x => x.Update(It.IsAny<Empty>()))
            .Callback<Empty>(_ => result += "2");
        Mock.Get(observer3)
            .Setup(x => x.Update(It.IsAny<Empty>()))
            .Callback<Empty>(_ => result += "3");
        
        observable.RegisterObserver(observer1, 2);
        observable.RegisterObserver(observer2, 3);
        observable.RegisterObserver(observer3);
        
        observable.NotifyMembers();
        
        Assert.That(result, Is.EqualTo(expected));
    }
}