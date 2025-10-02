namespace Cafe.Lib.Condiments;

public class Cream : CondimentDecorator
{
    public Cream(IBeverage beverage) : base(beverage)
    {
    }

    protected override string GetCondimentDescription()
    {
        return "Cream";
    }

    protected override double GetCondimentCost()
    {
        return 25;
    }
}