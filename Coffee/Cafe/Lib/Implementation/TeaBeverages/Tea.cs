namespace Cafe.Lib.Implementation.TeaBeverages;

public class Tea : Beverage
{
    protected Tea(string description) : base(description)
    {
    }

    public override double GetCost()
    {
        return 30;
    }
}