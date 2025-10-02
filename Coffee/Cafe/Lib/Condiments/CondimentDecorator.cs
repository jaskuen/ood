namespace Cafe.Lib.Condiments;

public class CondimentDecorator : IBeverage
{
    private readonly IBeverage _beverage;

    protected CondimentDecorator(IBeverage beverage)
    {
        _beverage = beverage;
    }

    public string GetDescription()
    {
        return $"{_beverage.GetDescription()}, {GetCondimentDescription()}";
    }

    public double GetCost()
    {
        return _beverage.GetCost() + GetCondimentCost(); 
    }

    protected virtual string GetCondimentDescription()
    {
        throw new Exception("Cannot use method of a base class");
    }

    protected virtual double GetCondimentCost()
    {
        throw new Exception("Cannot use method of a base class");
    }
}