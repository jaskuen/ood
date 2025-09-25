namespace WeatherStation.Lib.Implementation;

public class Observable<T> : IObservable<T>
{
    private IList<IObserver<T>> _observers = new List<IObserver<T>>();
    
    public void RegisterObserver(IObserver<T> observer)
    {
        _observers.Add(observer);
    }

    public void NotifyMembers()
    {
        IList<IObserver<T>> currentObservers = new List<IObserver<T>>(_observers);
        foreach (IObserver<T> observer in currentObservers)
        {
            observer.Update(GetChangedData());
        }
    }

    public void RemoveObserver(IObserver<T> observer)
    {
        _observers.Remove(observer);
    }

    protected virtual T GetChangedData()
    {
        throw new NotImplementedException("Cannot use default observable class");
    }

    protected IList<IObserver<T>> GetObservers()
    {
        return _observers;
    }
}