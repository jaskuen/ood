namespace Cafe.Lib.Condiments;

public enum IceCubeType
{
    Dry = 1, // Сухой лед (для суровых сибирских мужиков)
    Water = 2 // Обычные кубики из воды
}

public class IceCubes : CondimentDecorator
{
    private IceCubeType _type;
    private int _quantity;

    public IceCubes(IBeverage beverage, int quantity, IceCubeType type = IceCubeType.Water) : base(beverage)
    {
        _quantity = quantity;
        _type = type;
    }

    protected override double GetCondimentCost()
    {
        return (_type == IceCubeType.Dry ? 10 : 5) * _quantity;
    }

    protected override string GetCondimentDescription()
    {
        return $"{(_type == IceCubeType.Dry ? "Dry" : "Water")} ice cubes x {_quantity}";
    }
}