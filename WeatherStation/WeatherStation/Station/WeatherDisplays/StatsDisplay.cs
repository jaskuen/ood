namespace WeatherStation.Station.WeatherDisplays;

public class StatsDisplay : Lib.ICustomObserver<WeatherInfo>
{
    private readonly StationMeasurableValue _temperature = new StationMeasurableValue();
    private readonly StationMeasurableValue _humidity = new StationMeasurableValue();
    private readonly StationMeasurableValue _pressure = new StationMeasurableValue();
    private readonly StationMeasurableValue _windSpeed = new StationMeasurableValue();
    private readonly StationWindDirection _windDirection = new StationWindDirection();

    public void Update(WeatherInfo data)
    {
        _temperature.AddNewValue(data.Temperature);
        _humidity.AddNewValue(data.Humidity);
        _pressure.AddNewValue(data.Pressure);
        _windSpeed.AddNewValue(data.WindSpeed);
        _windDirection.AddNewValue(data.WindDirection);

        DisplayStat(_temperature, "temperature");
        DisplayStat(_humidity, "humidity");
        DisplayStat(_pressure, "pressure");
        DisplayWind();
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

    private void DisplayWind()
    {
        Console.WriteLine($"""
                           Max wind speed: {_windSpeed.GetMax()}
                           Min wind speed: {_windSpeed.GetMin()}
                           Average wind speed: {_windSpeed.GetAverage()}
                           Average wind direction: {_windDirection.GetAverage()}
                           -------------------------------
                           """);
    }
}