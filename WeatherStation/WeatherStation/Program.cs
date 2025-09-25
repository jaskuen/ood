// See https://aka.ms/new-console-template for more information

using WeatherStation.Station;
using WeatherStation.Station.WeatherDisplays;

try
{

    WeatherData inData = new WeatherData("in");
    WeatherData outData = new WeatherData("out");
    StatsDisplay display = new StatsDisplay();
    inData.RegisterObserver(display);
    outData.RegisterObserver(display);

    inData.SetMeasurements(10, 50, 770);
    outData.SetMeasurements(20, 30, 762);
}
catch (Exception e)
{
    Console.WriteLine(e);
}