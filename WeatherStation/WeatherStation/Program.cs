// See https://aka.ms/new-console-template for more information

using WeatherStation.Station;
using WeatherStation.Station.WeatherDisplays;

WeatherDataPro stationOut = new WeatherDataPro("out");
WeatherData stationIn = new WeatherData("in");
StatsDisplay display = new StatsDisplay();
stationOut.RegisterObserver(display);
stationIn.RegisterObserver(display);

stationOut.SetMeasurements(10, 20, 768, 3, 10);
stationIn.SetMeasurements(20, 70, 766);
stationOut.SetMeasurements(12, 63, 778, 6, 350);