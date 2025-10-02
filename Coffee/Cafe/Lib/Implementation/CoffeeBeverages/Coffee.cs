namespace Cafe.Lib.Implementation.CoffeeBeverages;

public enum CoffeeSize
{
    Standard = 1,
    Double = 2,
}

public class Coffee : Beverage
{
    protected Coffee(string description) : base(description)
    {
    }

    public override double GetCost()
    {
        return 60;
    }

    protected static string GetSizedName(CoffeeSize size, string description) => size switch
    {
        CoffeeSize.Standard => $"Standard {description}",
        CoffeeSize.Double => $"Double {description}",
        _ => throw new ArgumentOutOfRangeException()
    };
}