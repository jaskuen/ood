namespace Cafe.Lib.Condiments;

public class Cinnamon : CondimentDecorator
{
    public Cinnamon(IBeverage beverage) : base(beverage)
    {
    }

    protected override double GetCondimentCost()
    {
        return 20;
    }

    protected override string GetCondimentDescription()
    {
        return "Cinnamon";
    }
}