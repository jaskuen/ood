using WeatherStation.Lib.Implementation;

namespace WeatherStationTests.TestClasses;

public struct Data
{
    public IList<WeatherStation.Lib.IObserver<Data>> Observers;
}

public class DeleteObserver : WeatherStation.Lib.IObserver<Data>
{
    public void Update(Data data)
    {
        data.Observers.Remove(this);
    }
}

public class DeleteObserverObservable : Observable<Data>
{
    public void Execute()
    {
        NotifyMembers();
    }

    protected override Data GetChangedData()
    {
        Data d = new Data
        {
            Observers = GetObservers()
        };

        return d;
    }
}