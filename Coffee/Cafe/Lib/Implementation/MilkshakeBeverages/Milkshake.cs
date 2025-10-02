namespace Cafe.Lib.Implementation.MilkshakeBeverages;

public enum MilkshakeSize
{
    Small = 1,
    Medium = 2,
    Large = 3
}

public class Milkshake : Beverage
{
    private readonly MilkshakeSize _size;
    
    public Milkshake(MilkshakeSize size) : base(GetSizedName(size))
    {
        _size = size;
    }

    public override double GetCost()
    {
        return _size switch
        {
            MilkshakeSize.Small => 50,
            MilkshakeSize.Medium => 60,
            MilkshakeSize.Large => 80,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static string GetSizedName(MilkshakeSize size) => size switch
    {
        MilkshakeSize.Small => "Small milkshake",
        MilkshakeSize.Medium => "Medium milkshake",
        MilkshakeSize.Large => "Large milkshake",
        _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
    };
}