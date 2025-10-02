namespace Cafe.Lib.Implementation.CoffeeBeverages;

public class Latte : Coffee
{
    private readonly CoffeeSize _size;

    public Latte(CoffeeSize size) : base(GetSizedName(size, "Latte"))
    {
        _size = size;
    }

    public override double GetCost()
    {
        return _size == CoffeeSize.Standard ? 90 : 130;
    }
}