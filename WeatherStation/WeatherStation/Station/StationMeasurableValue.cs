namespace WeatherStation.Station;

public class StationMeasurableValue
{
    private double _max = Double.MinValue;
    private double _min = Double.MaxValue;
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
    public virtual double GetAverage() => _sum / _count;
}

public class StationWindDirection
{
    private IList<double> _directions = new List<double>();
    private double _current = 0;
    private int _count;

    public void AddNewValue(double value)
    {
        _directions.Add(value);
        _current = value;
        _count++;
    }

    public double GetAverage()
    {
        double x = 0, y = 0;
        foreach (double d in _directions)
        {
            double rad = d * Math.PI / 180;
            x += Math.Cos(rad);
            y += Math.Sin(rad);
        }
        x /= _count;
        y /= _count;
        
        double atan = Math.Atan2(y, x);
        
        return (atan * 180 / Math.PI + 360) % 360;
    }

    public double GetCurrent() => _current;
}