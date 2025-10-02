namespace Cafe.Lib.Condiments;

public class CoconutFlakes : CondimentDecorator
{
    private readonly int _mass;

    public CoconutFlakes(IBeverage beverage, int mass) : base(beverage)
    {
        _mass = mass;
    }

    protected override double GetCondimentCost()
    {
        return 1 * _mass;
    }

    protected override string GetCondimentDescription()
    {
        return $"Coconut flakes {_mass}g";
    }
}