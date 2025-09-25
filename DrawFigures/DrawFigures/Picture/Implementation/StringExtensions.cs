namespace DrawFigures.Picture.Implementation;

public static class StringExtensions
{
    public static float ToFloat(this string str)
    {
        return !float.TryParse(str, out var result)
            ? throw new FormatException($"'{str}' is not a valid float.")
            : result;
    }
}