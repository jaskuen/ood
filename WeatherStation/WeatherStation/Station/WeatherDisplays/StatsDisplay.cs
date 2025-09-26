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
    private IDictionary<string, StatsDisplayValues> _stats =
        new Dictionary<string, StatsDisplayValues>();

    public void Update(WeatherInfo data, ObservableData source)
    {
        if (!_stats.ContainsKey(source.Id))
        {
            _stats.Add(source.Id, new StatsDisplayValues());
        }
        
        StatsDisplayValues stats = _stats[source.Id];
        
        stats.Temperature.AddNewValue(data.Temperature);
        stats.Humidity.AddNewValue(data.Humidity);
        stats.Pressure.AddNewValue(data.Pressure);
        
        Console.WriteLine($"Station name: {source.Name}");
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