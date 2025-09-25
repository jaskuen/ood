using WeatherStation.Lib;

namespace WeatherStation.Station.WeatherDisplays;

public class Display : Lib.IObserver<WeatherInfo>
{
    public void Update(WeatherInfo data)
    {
        Console.WriteLine(data.ToString());
    }
}