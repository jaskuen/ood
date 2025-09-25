using WeatherStation.Lib.Implementation;

namespace WeatherStationTests.TestClasses;

public struct Empty
{
}

public class SimpleObservable : CustomObservable<Empty>
{
    protected override Empty GetChangedData()
    {
        return new Empty();
    }
}