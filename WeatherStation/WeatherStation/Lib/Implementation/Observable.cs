namespace WeatherStation.Lib.Implementation;

public class CustomObservable<T> : ICustomObservable<T>
{
    private IDictionary<ICustomObserver<T>, int> _observers = new Dictionary<ICustomObserver<T>, int>();

    public void RegisterObserver(ICustomObserver<T> observer, int priority = 1)
    {
        // 1 - highest priority
        if (!_observers.TryAdd(observer, priority))
        {
            throw new Exception("Observer is already added");
        }
    }

    public void NotifyMembers()
    {
        IList<ICustomObserver<T>> currentObservers = 
            _observers
                .OrderBy(o => o.Value)
                .Select(o => o.Key)
                .ToList();
        foreach (ICustomObserver<T> observer in currentObservers)
        {
            observer.Update(GetChangedData(), this);
        }
    }

    public void RemoveObserver(ICustomObserver<T> observer)
    {
        if (!_observers.Remove(observer))
        {
            throw new Exception("Observer is not attached to this observable");
        }
    }

    public virtual string GetName() => "";

    protected virtual T GetChangedData()
    {
        throw new Exception("Cannot use default observable class");
    }

    protected IList<ICustomObserver<T>> GetObservers()
    {
        return _observers.Keys.ToList();
    }
}