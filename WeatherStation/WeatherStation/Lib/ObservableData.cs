namespace WeatherStation.Lib;

public class ObservableData
{
    public ObservableData(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; }
    public string Name { get; set; }
}