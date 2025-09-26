namespace WeatherStation.Lib.Implementation;

public class ObserverListComparer<T> : IComparer<KeyValuePair<ICustomObserver<T>, int>>
{
    public int Compare(KeyValuePair<ICustomObserver<T>, int> x, KeyValuePair<ICustomObserver<T>, int> y)
    {
        if (x.Key.Equals(y.Key))
        {
            return 0;
        }
        
        return x.Value.CompareTo(y.Value);
    }
}