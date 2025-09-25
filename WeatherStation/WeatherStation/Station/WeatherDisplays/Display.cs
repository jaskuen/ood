using WeatherStation.Lib;

namespace WeatherStation.Station.WeatherDisplays;

public class Display : ICustomObserver<WeatherInfo>
{
    public void Update(WeatherInfo data, ICustomObservable<WeatherInfo> source)
    {
        Console.WriteLine(data.ToString());
    }
}