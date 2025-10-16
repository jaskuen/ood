using FiguresWithFactory.Lib.Canva;

namespace FiguresWithFactory.Lib.Shapes;

public class Painter
{
    public void DrawPicture(PictureDraft draft, ICanvas canvas)
    {
        for (int i = 0; i < draft.GetShapesCount(); i++)
        {
            draft.GetShape(i).Draw(canvas);
        }
    }
}