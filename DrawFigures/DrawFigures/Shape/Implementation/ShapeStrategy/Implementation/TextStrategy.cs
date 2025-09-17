using DrawFigures.Canvas;

namespace DrawFigures.Shape.Implementation.ShapeStrategy.Implementation;

public class TextStrategy : IShapeStrategy
{
    public TextStrategy(float x, float y, string text, float fontSize)
    {
        TextPosition = new Point(x, y);
        Text = text;
        FontSize = fontSize;
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

    public string StringParams() =>  $"text {TextPosition.X} {TextPosition.Y} {FontSize} {Text}";
}