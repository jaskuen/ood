using DrawFigures.Canvas;
using DrawFigures.Picture.Implementation;

namespace DrawFigures.Shapes.Implementation.ShapeStrategy.Implementation;

public class TextStrategy : IShapeStrategy
{
    public TextStrategy(string x, string y, string text, string fontSize)
    {
        TextPosition = new Point(x.ToFloat(), y.ToFloat());
        Text = text;
        FontSize = fontSize.ToFloat();
    }

    private Point TextPosition { get; set; }
    private float FontSize { get; set; }
    private string Text { get; set; }

    public void Draw(ICanvas canvas)
    {
        canvas.DrawText(TextPosition.X, TextPosition.Y, FontSize, Text);
    }

    public void MoveBy(float x, float y)
    {
        TextPosition.X += x;
        TextPosition.Y += y;
    }

    public string StringParams()
    {
        return $"text {TextPosition.X} {TextPosition.Y} {FontSize} {Text}";
    }
}