namespace WeatherStation.Lib;

public interface IObservable<T>
{
    public void RegisterObserver(IObserver<T> observer);
    public void NotifyMembers();
    public void RemoveObserver(IObserver<T> observer);
}