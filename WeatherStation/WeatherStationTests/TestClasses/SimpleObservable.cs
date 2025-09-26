using WeatherStation.Lib.Implementation;

namespace WeatherStationTests.TestClasses;

public struct Empty
{
}

public class SimpleObservable : CustomObservable<Empty>
{
    public SimpleObservable(string name) : base(name)
    {
    }

    protected override Empty GetChangedData()
    {
        return new Empty();
    }
}