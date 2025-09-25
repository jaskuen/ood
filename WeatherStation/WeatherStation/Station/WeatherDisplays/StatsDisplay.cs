namespace WeatherStation.Station.WeatherDisplays;

public class StatsDisplay : Lib.ICustomObserver<WeatherInfo>
{
    private readonly StationMeasurableValue _temperature = new StationMeasurableValue();
    private readonly StationMeasurableValue _humidity = new StationMeasurableValue();
    private readonly StationMeasurableValue _pressure = new StationMeasurableValue();

    public void Update(WeatherInfo data)
    {
        _temperature.AddNewValue(data.Temperature);
        _humidity.AddNewValue(data.Humidity);
        _pressure.AddNewValue(data.Pressure);

        DisplayStat(_temperature, "temperature");
        DisplayStat(_humidity, "humidity");
        DisplayStat(_pressure, "pressure");
    }

    public void Update(WeatherInfo data, IList<Lib.ICustomObserver<WeatherInfo>> observers)
    {
        Update(data);
        observers.Remove(this);
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