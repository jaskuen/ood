namespace WeatherStation.Lib;

public interface ICustomObserver<T>
{
    public void Update(T data, ObservableData source);
}