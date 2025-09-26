using WeatherStation.Lib.Implementation;
using WeatherStation.Station.WeatherDisplays.Info;

namespace WeatherStation.Station;

public record WeatherProInfo : ObserverInfo
{
    public double Temperature;
    public double Humidity;
    public double Pressure;
    public double WindSpeed;
    public double WindDirection;

    public override string ToString()
    {
        return $"""
                Current temperature: {Temperature}
                Current humidity: {Humidity}
                Current pressure: {Pressure}
                Current wind speed: {WindSpeed}
                Current wind direction: {WindDirection}
                """;
    }
}

public class WeatherDataPro : CustomObservable<ObserverInfo>
{
    private double _temperature;
    private double _humidity;
    private double _pressure;
    private double _windSpeed;
    private double _windDirection;

    public WeatherDataPro(string name) : base(name)
    {
    }

    public double GetTemperature()
    {
        return _temperature;
    }

    public double GetHumidity()
    {
        return _humidity;
    }

    public double GetPressure()
    {
        return _pressure;
    }

    public double GetWindSpeed()
    {
        return _windSpeed;
    }

    public double GetWindDirection()
    {
        return _windDirection;
    }

    public void MeasurementsChanged()
    {
        NotifyMembers();
    }

    public void SetMeasurements(double temperature, double humidity, double pressure, double windSpeed,
        double windDirection)
    {
        _temperature = temperature;
        _humidity = humidity;
        _pressure = pressure;
        _windSpeed = windSpeed;
        _windDirection = windDirection;

        MeasurementsChanged();
    }
    
    protected override WeatherProInfo GetChangedData()
    {
        WeatherProInfo data = new WeatherProInfo();
        data.Temperature = GetTemperature();
        data.Humidity = GetHumidity();
        data.Pressure = GetPressure();
        data.WindSpeed = GetWindSpeed();
        data.WindDirection = GetWindDirection();

        return data;
    }
}