using Slides.Lib.Canvas;

namespace Slides.Lib.Shapes.Implementation;

public abstract class Shape : IShape
{
    protected ShapeGroup? Parent;

    protected LineStyle? LineStyle;
    protected FillStyle? FillStyle;

    public void Draw(ICanvas canvas)
    {
        if (LineStyle != null)
        {
            RgbaColor lineColor = LineStyle.Color;

            canvas.SetStrokeColor(lineColor.R, lineColor.G, lineColor.B, lineColor.A);
            canvas.SetStrokeWidth(LineStyle.Width);
        }
        else
        {
            canvas.SetStrokeColor(0, 0, 0, 0);
            canvas.SetStrokeWidth(0);
        }

        if (FillStyle != null)
        {
            RgbaColor fillColor = FillStyle.Color;

            canvas.SetFillColor(fillColor.R, fillColor.G, fillColor.B, fillColor.A);
        }
        else
        {
            canvas.SetFillColor(0, 0, 0, 0);
        }

        DoDraw(canvas);
    }

    protected abstract void DoDraw(ICanvas canvas);
    public abstract Frame GetFrame();
    public abstract void SetFrame(double x1, double y1, double x2, double y2);
    public abstract LineStyle? GetLineStyle();
    public abstract FillStyle? GetFillStyle();

    public void SetLineStyle(bool isEnabled, RgbaColor color, double width = 1)
    {
        LineStyle = new LineStyle(isEnabled, color, width);

        DoSetLineStyleToChildren(isEnabled, color, width);

        Parent?.UpdateStrokeStyle();
    }

    public void SetFillStyle(bool isEnabled, RgbaColor color)
    {
        FillStyle = new FillStyle(isEnabled, color);

        DoSetFillStyleToChildren(isEnabled, color);

        Parent?.UpdateFillStyle();
    }

    public abstract IShapes? GetShapesGroup();

    protected abstract void DoSetLineStyleToChildren(bool isEnabled, RgbaColor color, double width = 1);
    protected abstract void DoSetFillStyleToChildren(bool isEnabled, RgbaColor color);

    protected internal void SetParent(ShapeGroup parent)
    {
        if (Parent != null)
        {
            int figureIndex = 0;
            for (int i = 0; i < Parent.GetShapesCount(); i++)
            {
                Shape shape = Parent.GetShapeAt(i);

                if (shape.GetHashCode() != GetHashCode())
                {
                    continue;
                }

                figureIndex = i;
                break;
            }

            Parent.RemoveShapeAt(figureIndex);
        }

        Parent = parent;
    }

    protected List<ShapeGroup?> GetParents()
    {
        List<ShapeGroup?> parents = [];
        ShapeGroup? currentParent = Parent;
        while (currentParent != null)
        {
            parents.Add(currentParent);
            currentParent = currentParent.Parent;
        }

        return parents;
    }
}