namespace FiguresWithFactory.Lib.Shapes;

public class PictureDraft
{
    private readonly IList<Shape> _shapes;

    public PictureDraft(IList<Shape> shapes)
    {
        _shapes = shapes;
    }

    public int GetShapesCount() => _shapes.Count;

    public Shape GetShape(int index)
    {
        if (index < 0 || index >= _shapes.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return _shapes[index];
    }
}