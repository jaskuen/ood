namespace WeatherStation.Lib;

public interface ICustomObserver<T>
{
    // Убрать зависимость от Observable, брать уникальный идентификатор
    public void Update(T data, ObservableData source);
}