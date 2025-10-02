namespace Cafe.Lib.Condiments;

public enum LiquorType
{
    Nut = 1,
    Chocolate = 2,
}

public class Liquor : CondimentDecorator
{
    private readonly LiquorType _type;

    public Liquor(IBeverage beverage, LiquorType type) : base(beverage)
    {
        _type = type;
    }


    protected override double GetCondimentCost()
    {
        return 50;
    }

    protected override string GetCondimentDescription()
    {
        return $"{(_type == LiquorType.Nut ? "Nut" : "Chocolate")} liquor";
    }
}