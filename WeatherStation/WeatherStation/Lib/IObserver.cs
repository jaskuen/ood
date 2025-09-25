namespace WeatherStation.Lib;

public interface ICustomObserver<T>
{
    public void Update(T data, ICustomObservable<T> source);
}