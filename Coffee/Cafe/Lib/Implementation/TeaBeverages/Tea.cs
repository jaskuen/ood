namespace Cafe.Lib.Implementation.TeaBeverages;

public enum TeaType
{
    Puer = 1,
    Anchan = 2,
    Hibiscus = 3,
    Bergamot = 4,
}

public class Tea : Beverage
{
    public Tea(TeaType type) : base(TypeToString(type))
    {
    }

    public override double GetCost()
    {
        return 30;
    }

    private static string TypeToString(TeaType type) => type switch
    {
        TeaType.Puer => "Puer",
        TeaType.Anchan => "Anchan",
        TeaType.Hibiscus => "Hibiscus",
        TeaType.Bergamot => "Bergamot",
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };
}