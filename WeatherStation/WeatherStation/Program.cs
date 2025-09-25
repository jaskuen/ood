// See https://aka.ms/new-console-template for more information

using WeatherStation.Station;
using WeatherStation.Station.WeatherDisplays;

WeatherDataPro station = new WeatherDataPro();
StatsDisplay display = new StatsDisplay();
station.RegisterObserver(display);

station.SetMeasurements(10, 20, 768, 3, 10);
station.SetMeasurements(12, 63, 778, 6, 350);
station.SetMeasurements(-3, 87, 770, 4, 340);