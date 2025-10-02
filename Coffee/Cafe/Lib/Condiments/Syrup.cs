namespace Cafe.Lib.Condiments;

public enum SyrupType
{
    Chocolate = 1,
    Maple = 2
}

public class Syrup : CondimentDecorator
{
    private readonly SyrupType _type;

    public Syrup(IBeverage beverage, SyrupType type) : base(beverage)
    {
        _type = type;
    }

    protected override double GetCondimentCost()
    {
        return 15;
    }

    protected override string GetCondimentDescription()
    {
        return $"{(_type == SyrupType.Chocolate ? "Chocolate" : "Maple")} syrup";
    }
}