using Moq;
using WeatherStation.Lib;
using WeatherStation.Station;
using WeatherStation.Station.WeatherDisplays;
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
        DeleteObserverObservable observable = new DeleteObserverObservable("delete");
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
        SimpleObservable Observable = new SimpleObservable("simple");

        string expected = "312";
        string result = String.Empty;

        // Добавляем номер наблюдателя (не его приоритет) в результирующую строку
        Mock.Get(Observer1)
            .Setup(x => x.Update(It.IsAny<Empty>(), It.IsAny<ObservableData>()))
            .Callback(() => result += "1");
        Mock.Get(Observer2)
            .Setup(x => x.Update(It.IsAny<Empty>(), It.IsAny<ObservableData>()))
            .Callback(() => result += "2");
        Mock.Get(Observer3)
            .Setup(x => x.Update(It.IsAny<Empty>(), It.IsAny<ObservableData>()))
            .Callback(() => result += "3");

        Observable.RegisterObserver(Observer3);
        Observable.RegisterObserver(Observer1, 2);
        Observable.RegisterObserver(Observer2, 3);

        Observable.NotifyMembers();

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Observer_ThereAreDifferentObservables_CanGetThemCorrectly()
    {
        // Задаем порядок отправления сообщений от станций: 3 1 2
        ICustomObserver<WeatherInfo> observer = Mock.Of<ICustomObserver<WeatherInfo>>();

        string expected = "312";
        string result = String.Empty;
        
        WeatherData observable1 = new("1");
        observable1.RegisterObserver(observer);
        WeatherData observable2 = new("2");
        observable2.RegisterObserver(observer);
        WeatherData observable3 = new("3");
        observable3.RegisterObserver(observer);
        
        Mock.Get(observer)
            .Setup(x => x.Update(It.IsAny<WeatherInfo>(), It.IsAny<ObservableData>()))
            .Callback<WeatherInfo, ObservableData>((_, data) =>
            {
                if (data.Id == observable1.Id)
                {
                    result += "1";
                }
                if (data.Id == observable2.Id)
                {
                    result += "2";
                }
                if (data.Id == observable3.Id)
                {
                    result += "3";
                }
            });
        
        observable3.SetMeasurements(0, 0, 0);
        observable1.SetMeasurements(0, 0, 0);
        observable2.SetMeasurements(0, 0, 0);
        
        Assert.That(result, Is.EqualTo(expected));
    }
}