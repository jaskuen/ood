using WeatherStation.Lib.Implementation;
using WeatherStation.Station.WeatherDisplays.Info;

namespace WeatherStation.Station;

public record WeatherInfo : ObserverInfo
{
    public double Temperature;
    public double Humidity;
    public double Pressure;

    public override string ToString()
    {
        return $"""
                Current temperature: {Temperature}
                Current humidity: {Humidity}
                Current pressure: {Pressure}
                """;
    }
}

public class WeatherData : CustomObservable<ObserverInfo>
{
    private double _temperature;
    private double _humidity;
    private double _pressure;

    public WeatherData(string name) : base(name)
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

    public void MeasurementsChanged()
    {
        NotifyMembers();
    }

    public void SetMeasurements(double temperature, double humidity, double pressure)
    {
        _temperature = temperature;
        _humidity = humidity;
        _pressure = pressure;

        MeasurementsChanged();
    }
    
    protected override WeatherInfo GetChangedData()
    {
        WeatherInfo data = new WeatherInfo();
        data.Temperature = GetTemperature();
        data.Humidity = GetHumidity();
        data.Pressure = GetPressure();

        return data;
    }
}