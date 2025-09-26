using WeatherStation.Lib;
using WeatherStation.Station.WeatherDisplays.Info;
using WeatherStation.Station.WeatherDisplays.Values;

namespace WeatherStation.Station.WeatherDisplays;

public class StatsDisplay : ICustomObserver<ObserverInfo>
{
    private IDictionary<string, StatsDisplayValues> _stats =
        new Dictionary<string, StatsDisplayValues>();

    public void Update(ObserverInfo data, ObservableData source)
    {
        if (!_stats.ContainsKey(source.Id))
        {
            _stats.Add(source.Id, new StatsDisplayValues());
        }

        StatsDisplayValues stats = _stats[source];
        bool isPro = false;

        switch (data)
        {
            case WeatherInfo weatherInfo:
                stats.Temperature.AddNewValue(weatherInfo.Temperature);
                stats.Humidity.AddNewValue(weatherInfo.Humidity);
                stats.Pressure.AddNewValue(weatherInfo.Pressure);
                break;
            case WeatherProInfo weatherProInfo:
                stats.Temperature.AddNewValue(weatherProInfo.Temperature);
                stats.Humidity.AddNewValue(weatherProInfo.Humidity);
                stats.Pressure.AddNewValue(weatherProInfo.Pressure);
                if (stats.WindSpeed == null)
                {
                    stats.WindSpeed = new StationMeasurableValue();
                }

                if (stats.WindDirection == null)
                {
                    stats.WindDirection = new StationWindDirection();
                }

                stats.WindSpeed.AddNewValue(weatherProInfo.WindSpeed);
                stats.WindDirection.AddNewValue(weatherProInfo.WindDirection);
                isPro = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(data));
        }

        Console.WriteLine($"Station name: {source.GetName()}");
        DisplayStat(stats.Temperature, "temperature");
        DisplayStat(stats.Humidity, "humidity");
        DisplayStat(stats.Pressure, "pressure");
        if (isPro)
        {
            DisplayWind(stats.WindSpeed!, stats.WindDirection!);
        }
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

    private void DisplayWind(StationMeasurableValue windSpeed, StationWindDirection windDirection)
    {
        Console.WriteLine($"""
                           Max wind speed: {windSpeed.GetMax()}
                           Min wind speed: {windSpeed.GetMin()}
                           Average wind speed: {windSpeed.GetAverage()}
                           Average wind direction: {windDirection.GetAverage()}
                           -------------------------------
                           """);
    }
}