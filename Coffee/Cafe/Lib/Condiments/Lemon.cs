namespace Cafe.Lib.Condiments;

public class Lemon : CondimentDecorator
{
    private int _quantity;

    public Lemon(IBeverage beverage, int quantity = 1) : base(beverage)
    {
        _quantity = quantity;
    }

    protected override double GetCondimentCost()
    {
        return 10 * _quantity;
    }

    protected override string GetCondimentDescription()
    {
        return $"Lemon x {_quantity}";
    }
}