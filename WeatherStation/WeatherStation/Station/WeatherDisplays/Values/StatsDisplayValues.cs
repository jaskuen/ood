namespace WeatherStation.Station.WeatherDisplays.Values;

public class StatsDisplayValues
{
    public StationMeasurableValue Temperature = new();
    public StationMeasurableValue Humidity = new();
    public StationMeasurableValue Pressure = new();
    public StationMeasurableValue? WindSpeed = new();
    public StationWindDirection? WindDirection = new();
}