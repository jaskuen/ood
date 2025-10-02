namespace Cafe.Lib.Condiments;

public class ChocolatePieces : CondimentDecorator
{
    private readonly int _quantity;
    private const int MaxPiecesCount = 5;

    public ChocolatePieces(IBeverage beverage, int quantity) : base(beverage)
    {
        if (quantity > MaxPiecesCount)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Max count of chocolate pieces: 5");
        }

        _quantity = quantity;
    }

    protected override double GetCondimentCost()
    {
        return 10 * _quantity;
    }

    protected override string GetCondimentDescription()
    {
        return $"Chocolate piece x {_quantity}";
    }
}