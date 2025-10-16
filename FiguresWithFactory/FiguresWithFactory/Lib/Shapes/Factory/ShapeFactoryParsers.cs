using FiguresWithFactory.Lib.Canva;

namespace FiguresWithFactory.Lib.Shapes.Factory;

public static class ShapeFactoryParsers
{
    public static int ParseToInt(this string str)
    {
        if (!int.TryParse(str, out int result))
        {
            throw new ArgumentException($"{str} is not an integer");
        }
        
        return result;
    }

    public static double ParseToDouble(this string str)
    {
        if (!double.TryParse(str, out double result))
        {
            throw new ArgumentException($"{str} is not a floating-point number");
        }
        
        return result;
    }

    public static string ParseToString(this Color color)
    {
        return color switch
        {
            Color.Red => "red",
            Color.Green => "green",
            Color.Blue => "blue",
            Color.Yellow => "yellow",
            Color.Pink => "pink",
            _ => "black"
        };
    }

    public static Color ParseToColor(this string str)
    {
        return str switch
        {
            "red" => Color.Red,
            "green" => Color.Green,
            "blue" => Color.Blue,
            "yellow" => Color.Yellow,
            "pink" => Color.Pink,
            _ => Color.Black
        };

    }
}