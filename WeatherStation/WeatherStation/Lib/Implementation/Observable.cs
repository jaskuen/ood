using System.Collections;

namespace WeatherStation.Lib.Implementation;

public class CustomObservable<T> : ICustomObservable<T>
{
    private List<KeyValuePair<ICustomObserver<T>, int>> _observers = new List<KeyValuePair<ICustomObserver<T>, int>>();
    // private IDictionary<ICustomObserver<T>, int> _observers = new Dictionary<ICustomObserver<T>, int>();

    public void RegisterObserver(ICustomObserver<T> observer, int priority = 1)
    {
        // 1 - highest priority
        int indexToInsert = _observers.BinarySearch(
            new KeyValuePair<ICustomObserver<T>, int>(observer, priority), new ObserverListComparer<T>());

        if (indexToInsert < 0)
        {
            _observers.Insert(~indexToInsert, new KeyValuePair<ICustomObserver<T>, int>(observer, priority));
        }
    }

    public void NotifyMembers()
    {
        // Сортировать при вставке
        IList<ICustomObserver<T>> currentObservers = 
            _observers
                .Select(o => o.Key)
                .ToList();
        foreach (ICustomObserver<T> observer in currentObservers)
        {
            observer.Update(GetChangedData());
        }
    }

    public void RemoveObserver(ICustomObserver<T> observer)
    {
        int index = _observers.BinarySearch(
            new KeyValuePair<ICustomObserver<T>, int>(observer, 1), new ObserverListComparer<T>());
        if (index < 0)
        {
            throw new Exception("Observer is not attached to this observable");
        }
        
        _observers.RemoveAt(index);
    }

    protected virtual T GetChangedData()
    {
        throw new Exception("Cannot use default observable class");
    }

    protected IList<ICustomObserver<T>> GetObservers()
    {
        return _observers.Select(o => o.Key).ToList();
    }
}