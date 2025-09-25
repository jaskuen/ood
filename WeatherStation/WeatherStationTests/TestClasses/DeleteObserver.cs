using WeatherStation.Lib;
using WeatherStation.Lib.Implementation;

namespace WeatherStationTests.TestClasses;

public struct Data
{
    public IList<ICustomObserver<Data>> Observers;
}

public class DeleteObserver : ICustomObserver<Data>
{
    public void Update(Data data, ICustomObservable<Data> source)
    {
        data.Observers.Remove(this);
    }
}

public class DeleteObserverObservable : CustomObservable<Data>
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