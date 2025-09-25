// See https://aka.ms/new-console-template for more information

using WeatherStation.Station;
using WeatherStation.Station.WeatherDisplays;

WeatherData station = new WeatherData();
StatsDisplay display = new StatsDisplay();
station.RegisterObserver(display);

station.SetMeasurements(10, 20, 768);
station.SetMeasurements(12, 63, 778);
station.SetMeasurements(-3, 87, 770);