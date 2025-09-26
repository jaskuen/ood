namespace WeatherStation.Station;

public class StationMeasurableValue
{
    private double _max = Double.MinValue;
    private double _min = Double.MaxValue;
    // Remove
    private double _current = 0;
    private double _sum = 0;
    private double _count = 0;

    public void AddNewValue(double value)
    {
        _current = value;
        
        if (value > _max)
        {
            _max = value;
        }

        if (value < _min)
        {
            _min = value;
        }
        
        _sum += value;
        _count++;
    }

    public double GetCurrent() => _current;
    public double GetMax() => _max;
    public double GetMin() => _min;
    public double GetAverage() => _sum / _count;
}