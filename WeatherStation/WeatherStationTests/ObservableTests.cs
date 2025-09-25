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
        
        ICustomObserver<Empty> Observer1 = Mock.Of<ICustomObserver<Empty>>();
        ICustomObserver<Empty> Observer2 = Mock.Of<ICustomObserver<Empty>>();
        ICustomObserver<Empty> Observer3 = Mock.Of<ICustomObserver<Empty>>();
        SimpleObservable Observable = new SimpleObservable();

        string expected = "312";
        string result = String.Empty;
        
        // Добавляем номер наблюдателя (не его приоритет) в результирующую строку
        Mock.Get(Observer1)
            .Setup(x => x.Update(It.IsAny<Empty>()))
            .Callback<Empty>(_ => result += "1");
        Mock.Get(Observer2)
            .Setup(x => x.Update(It.IsAny<Empty>()))
            .Callback<Empty>(_ => result += "2");
        Mock.Get(Observer3)
            .Setup(x => x.Update(It.IsAny<Empty>()))
            .Callback<Empty>(_ => result += "3");
        
        Observable.RegisterObserver(Observer3);
        Observable.RegisterObserver(Observer1, 2);
        Observable.RegisterObserver(Observer2, 3);
        
        Observable.NotifyMembers();
        
        Assert.That(result, Is.EqualTo(expected));
    }
}