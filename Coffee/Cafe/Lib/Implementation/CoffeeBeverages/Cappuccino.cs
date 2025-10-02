namespace Cafe.Lib.Implementation.CoffeeBeverages;

public class Cappuccino : Coffee
{
    private readonly CoffeeSize _size;

    public Cappuccino(CoffeeSize size) : base(GetSizedName(size, "Cappuccino"))
    {
        _size = size;
    }

    public override double GetCost()
    {
        return _size == CoffeeSize.Standard ? 80 : 120;
    }
}