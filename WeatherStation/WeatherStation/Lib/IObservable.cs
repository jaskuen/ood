namespace WeatherStation.Lib;

public interface ICustomObservable<T>
{
    public void RegisterObserver(ICustomObserver<T> observer, int priority = 1);
    public void NotifyMembers();
    public void RemoveObserver(ICustomObserver<T> observer);
    // Вынести
    public string GetName();
}