namespace Cafe.Lib.Implementation;

public class Beverage : IBeverage
{
    private string _description;

    protected Beverage(string description)
    {
        _description = description;
    }

    public string GetDescription()
    {
        return _description;
    }

    public virtual double GetCost()
    {
        throw new NotImplementedException("Cannot use method of a base class");
    }
}