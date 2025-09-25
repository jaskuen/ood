namespace WeatherStation.Lib;

public interface IObserver<T>
{
    public void Update(T data);
}