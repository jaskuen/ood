namespace Cafe.Lib.Condiments;

public class ChocolateCrumbs : CondimentDecorator
{
    private readonly int _mass;

    public ChocolateCrumbs(IBeverage beverage, int mass) : base(beverage)
    {
        _mass = mass;
    }

    protected override double GetCondimentCost()
    {
        return 2 * _mass;
    }

    protected override string GetCondimentDescription()
    {
        return $"Chocolate crumbs {_mass}g";
    }
}