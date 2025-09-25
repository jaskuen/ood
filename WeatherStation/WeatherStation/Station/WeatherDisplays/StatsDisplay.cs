using WeatherStation.Lib;

namespace WeatherStation.Station.WeatherDisplays;

public struct StatsDisplayValues()
{
    public StationMeasurableValue Temperature = new();
    public StationMeasurableValue Humidity = new();
    public StationMeasurableValue Pressure = new();
}

public class StatsDisplay : ICustomObserver<WeatherInfo>
{
    private IDictionary<ICustomObservable<WeatherInfo>, StatsDisplayValues> _stats =
        new Dictionary<ICustomObservable<WeatherInfo>, StatsDisplayValues>();

    public void Update(WeatherInfo data, ICustomObservable<WeatherInfo> source)
    {
        if (!_stats.ContainsKey(source))
        {
            _stats.Add(source, new StatsDisplayValues());
        }
        
        StatsDisplayValues stats = _stats[source];
        
        stats.Temperature.AddNewValue(data.Temperature);
        stats.Humidity.AddNewValue(data.Humidity);
        stats.Pressure.AddNewValue(data.Pressure);
        
        Console.WriteLine($"Station name: {source.GetName()}");
        DisplayStat(stats.Temperature, "temperature");
        DisplayStat(stats.Humidity, "humidity");
        DisplayStat(stats.Pressure, "pressure");
    }

    private void DisplayStat(StationMeasurableValue measurableValue, string statName)
    {
        Console.WriteLine($"""
                           Max {statName}: {measurableValue.GetMax()}
                           Min {statName}: {measurableValue.GetMin()}
                           Average {statName}: {measurableValue.GetAverage()}
                           -------------------------------
                           """);
    }

    
}