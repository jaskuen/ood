using System.Drawing.Drawing2D;
using Figures.Model.Core;
using Figures.Model.Core.Events;
using Figures.Model.Figures;
using Figures.ViewModel;
using ModelPoint = Figures.Model.Core.Point;

namespace Figures.View;

/// <summary>
/// Холст с поддержкой выделения, перемещения и изменения размера фигур
/// </summary>
public sealed class CanvasControl : Panel
{
    private WindowViewModel? _windowVm;
    private DragContext? _dragContext;
    private ModelPoint? _pendingDrag;
    private string? _pendingDragId;
    private float _offsetX;
    private float _offsetY;

    private struct DragContext
    {
        public string Type; // move | resize
        public ResizeHandle? Handle;
        public ModelPoint StartPoint;
        public string FigureId;
        public Rect OriginRect;
        public Rect LastRect;
    }

    public CanvasControl()
    {
        DoubleBuffered = true;
        Dock = DockStyle.Fill;
        BackColor = Color.LightGray;
    }

    public void Bind(WindowViewModel windowVm)
    {
        _windowVm = windowVm;

        windowVm.Selection.On(EventType.SelectionChanged, _ => SafeInvalidate());

        var doc = windowVm.Session.Document;
        doc.On(EventType.FigureAdded, _ => SafeInvalidate());
        doc.On(EventType.FigureRemoved, _ => SafeInvalidate());
        doc.On(EventType.FigureChanged, _ => SafeInvalidate());
        doc.On(EventType.CanvasChanged, _ => SafeInvalidate());
        doc.On(EventType.Cleared, _ => SafeInvalidate());

        SafeInvalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (_windowVm == null) return;

        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        var doc = _windowVm.Session.Document;
        var canvasWidth = doc.CanvasWidth;
        var canvasHeight = doc.CanvasHeight;

        _offsetX = (Width - canvasWidth) / 2f;
        _offsetY = (Height - canvasHeight) / 2f;

        g.Clear(Color.LightGray);

        var state = g.Save();
        g.TranslateTransform(_offsetX, _offsetY);

        using (var bg = new SolidBrush(Color.White))
        using (var border = new Pen(Color.DarkGray, 2))
        {
            g.FillRectangle(bg, 0, 0, canvasWidth, canvasHeight);
            g.DrawRectangle(border, 0, 0, canvasWidth, canvasHeight);
        }

        foreach (var figure in doc.Figures.OrderBy(f => f.ZIndex))
        {
            DrawFigure(g, figure);
        }

        var selectedId = _windowVm.Selection.SelectedId;
        if (selectedId != null)
        {
            var figure = doc.GetFigure(selectedId);
            if (figure != null)
            {
                DrawSelectionOverlay(g, figure.GetBoundingBox());
            }
        }

        g.Restore(state);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        HandleMouseDown(new ModelPoint(e.Location.X, e.Location.Y), e.Button == MouseButtons.Left);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        HandleMouseMove(new ModelPoint(e.Location.X, e.Location.Y));
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        base.OnMouseUp(e);
        if (e.Button == MouseButtons.Left)
        {
            HandleMouseUp();
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        // Перерисовываем окно
        Invalidate();
    }

    private void DrawFigure(Graphics g, FigureModel figure)
    {
        var box = figure.GetBoundingBox();
        var attrs = figure.Attributes;

        var fillColor = ToColor(attrs.Fill, attrs.FillOpacity);
        var strokeColor = ToColor(attrs.Stroke, attrs.StrokeOpacity);

        using var fillBrush = new SolidBrush(fillColor);
        using var strokePen = new Pen(strokeColor, attrs.StrokeWidth);

        switch (figure)
        {
            case RectangleModel:
                g.FillRectangle(fillBrush, box.X, box.Y, box.Width, box.Height);
                g.DrawRectangle(strokePen, box.X, box.Y, box.Width, box.Height);
                break;

            case EllipseModel:
                g.FillEllipse(fillBrush, box.X, box.Y, box.Width, box.Height);
                g.DrawEllipse(strokePen, box.X, box.Y, box.Width, box.Height);
                break;

            case TriangleModel:
                var points = new[]
                {
                    new PointF(box.X + box.Width / 2, box.Y),
                    new PointF(box.X + box.Width, box.Y + box.Height),
                    new PointF(box.X, box.Y + box.Height)
                };
                g.FillPolygon(fillBrush, points);
                g.DrawPolygon(strokePen, points);
                break;

            case ImageModel { ImageData: not null } image:
                try
                {
                    using var ms = new MemoryStream(image.ImageData);
                    using var img = Image.FromStream(ms);
                    g.DrawImage(img, new RectangleF(box.X, box.Y, box.Width, box.Height));
                }
                catch
                {
                    // игнорируем
                }

                break;
        }
    }

    private void DrawSelectionOverlay(Graphics g, Rect box)
    {
        using var pen = new Pen(Color.Blue, 2);
        pen.DashPattern = [4f, 2f];
        g.DrawRectangle(pen, box.X, box.Y, box.Width, box.Height);

        using var handleBrush = new SolidBrush(Color.White);
        using var handlePen = new Pen(Color.Blue, 1);

        var handles = new[]
        {
            new ModelPoint(box.X, box.Y),
            new ModelPoint(box.X + box.Width / 2, box.Y),
            new ModelPoint(box.X + box.Width, box.Y),
            new ModelPoint(box.X + box.Width, box.Y + box.Height / 2),
            new ModelPoint(box.X + box.Width, box.Y + box.Height),
            new ModelPoint(box.X + box.Width / 2, box.Y + box.Height),
            new ModelPoint(box.X, box.Y + box.Height),
            new ModelPoint(box.X, box.Y + box.Height / 2)
        };

        foreach (var handle in handles)
        {
            var rect = new RectangleF(handle.X - 5, handle.Y - 5, 10, 10);
            g.FillEllipse(handleBrush, rect);
            g.DrawEllipse(handlePen, rect);
        }
    }

    private void HandleMouseDown(ModelPoint viewportPoint, bool isLeftButton)
    {
        if (!isLeftButton || _windowVm == null) return;

        var point = ViewportToCanvas(viewportPoint);
        var selectedId = _windowVm.Selection.SelectedId;
        if (selectedId != null)
        {
            var handle = HitTestHandle(point, selectedId);
            if (handle != null)
            {
                var figure = _windowVm.Session.Document.GetFigure(selectedId);
                if (figure != null)
                {
                    var box = figure.GetBoundingBox();
                    _dragContext = new DragContext
                    {
                        Type = "resize",
                        Handle = handle.Value,
                        StartPoint = point,
                        FigureId = selectedId,
                        OriginRect = box,
                        LastRect = box
                    };
                    return;
                }
            }

            // Клик по рамке, но не по точке ресайза
            var figureForBorder = _windowVm.Session.Document.GetFigure(selectedId);
            if (figureForBorder != null)
            {
                var box = figureForBorder.GetBoundingBox();
                // +- 4 пикселя
                const float borderMargin = 4f;
                var onBorder = (point.X >= box.X - borderMargin && point.X <= box.X + box.Width + borderMargin &&
                                point.Y >= box.Y - borderMargin && point.Y <= box.Y + borderMargin) || // Верхний край
                               (point.X >= box.X - borderMargin && point.X <= box.X + box.Width + borderMargin &&
                                point.Y >= box.Y + box.Height - borderMargin &&
                                point.Y <= box.Y + box.Height + borderMargin) || // Нижний
                               (point.Y >= box.Y - borderMargin && point.Y <= box.Y + box.Height + borderMargin &&
                                point.X >= box.X - borderMargin && point.X <= box.X + borderMargin) || // Левый
                               (point.Y >= box.Y - borderMargin && point.Y <= box.Y + box.Height + borderMargin &&
                                point.X >= box.X + box.Width - borderMargin &&
                                point.X <= box.X + box.Width + borderMargin); // Правый

                // Не внутри фигуры
                var insideFigure = figureForBorder.ContainsPoint(point);

                if (onBorder && !insideFigure)
                {
                    // Начинаем операцию перемещения
                    _pendingDrag = point;
                    _pendingDragId = selectedId;
                    return;
                }
            }
        }

        var hit = HitTestFigure(point);
        if (hit != null)
        {
            if (!_windowVm.Selection.IsSelected(hit.Id))
            {
                _windowVm.SelectFigure(hit.Id);
            }

            _pendingDrag = point;
            _pendingDragId = hit.Id;
        }
        else
        {
            _windowVm.ClearSelection();
            _pendingDrag = null;
            _pendingDragId = null;
        }
    }

    private void HandleMouseMove(ModelPoint viewportPoint)
    {
        if (_windowVm == null) return;

        var point = ViewportToCanvas(viewportPoint);
        if (_dragContext != null)
        {
            if (_dragContext.Value.Type == "move")
            {
                var newRect = MovedRect(_dragContext.Value.OriginRect, point, _dragContext.Value.StartPoint);
                _dragContext = _dragContext.Value with { LastRect = newRect };
                _windowVm.Session.PreviewTransform(_dragContext.Value.FigureId, newRect);
            }
            else if (_dragContext.Value.Type == "resize")
            {
                var newRect = ResizedRect(point, _dragContext.Value.OriginRect, _dragContext.Value.Handle!.Value,
                    _dragContext.Value.StartPoint);
                _dragContext = _dragContext.Value with { LastRect = newRect };
                _windowVm.Session.PreviewTransform(_dragContext.Value.FigureId, newRect);
            }

            return;
        }

        if (_pendingDrag.HasValue && _pendingDragId != null)
        {
            var dx = Math.Abs(point.X - _pendingDrag.Value.X);
            var dy = Math.Abs(point.Y - _pendingDrag.Value.Y);
            const float threshold = 3;

            if (dx > threshold || dy > threshold)
            {
                var figure = _windowVm.Session.Document.GetFigure(_pendingDragId);
                if (figure != null)
                {
                    var box = figure.GetBoundingBox();
                    _dragContext = new DragContext
                    {
                        Type = "move",
                        StartPoint = _pendingDrag.Value,
                        FigureId = _pendingDragId,
                        OriginRect = box,
                        LastRect = box
                    };
                    _pendingDrag = null;
                    _pendingDragId = null;
                }
            }
        }
    }

    private void HandleMouseUp()
    {
        if (_dragContext != null && _windowVm != null)
        {
            _windowVm.Session.CommitTransform(
                _dragContext.Value.FigureId,
                _dragContext.Value.OriginRect,
                _dragContext.Value.LastRect
            );
            _dragContext = null;
        }

        _pendingDrag = null;
        _pendingDragId = null;
    }

    private FigureModel? HitTestFigure(ModelPoint point)
    {
        if (_windowVm == null) return null;

        var figures = _windowVm.Session.Document.Figures;
        for (int i = figures.Count - 1; i >= 0; i--)
        {
            if (figures[i].ContainsPoint(point))
            {
                return figures[i];
            }
        }

        return null;
    }

    private ResizeHandle? HitTestHandle(ModelPoint point, string figureId)
    {
        if (_windowVm == null) return null;

        var figure = _windowVm.Session.Document.GetFigure(figureId);
        if (figure == null) return null;

        var box = figure.GetBoundingBox();
        var handles = new[]
        {
            (new ModelPoint(box.X, box.Y), ResizeHandle.NW),
            (new ModelPoint(box.X + box.Width / 2, box.Y), ResizeHandle.N),
            (new ModelPoint(box.X + box.Width, box.Y), ResizeHandle.NE),
            (new ModelPoint(box.X + box.Width, box.Y + box.Height / 2), ResizeHandle.E),
            (new ModelPoint(box.X + box.Width, box.Y + box.Height), ResizeHandle.SE),
            (new ModelPoint(box.X + box.Width / 2, box.Y + box.Height), ResizeHandle.S),
            (new ModelPoint(box.X, box.Y + box.Height), ResizeHandle.SW),
            (new ModelPoint(box.X, box.Y + box.Height / 2), ResizeHandle.W)
        };

        foreach (var (handlePoint, handle) in handles)
        {
            var dx = point.X - handlePoint.X;
            var dy = point.Y - handlePoint.Y;
            if (Math.Sqrt(dx * dx + dy * dy) <= 6)
            {
                return handle;
            }
        }

        return null;
    }

    private ModelPoint ViewportToCanvas(ModelPoint viewportPoint)
    {
        return new ModelPoint(
            viewportPoint.X - _offsetX,
            viewportPoint.Y - _offsetY
        );
    }

    private Rect MovedRect(Rect origin, ModelPoint current, ModelPoint start)
    {
        var dx = current.X - start.X;
        var dy = current.Y - start.Y;
        return new Rect(origin.X + dx, origin.Y + dy, origin.Width, origin.Height);
    }

    private Rect ResizedRect(ModelPoint point, Rect origin, ResizeHandle handle, ModelPoint start)
    {
        var dx = point.X - start.X;
        var dy = point.Y - start.Y;
        var box = origin;

        switch (handle)
        {
            case ResizeHandle.N:
                box = new Rect(box.X, Math.Min(box.Y + box.Height - 20, box.Y + dy), box.Width,
                    Math.Max(20, box.Height - dy));
                break;
            case ResizeHandle.S:
                box = new Rect(box.X, box.Y, box.Width, Math.Max(20, box.Height + dy));
                break;
            case ResizeHandle.W:
                box = new Rect(Math.Min(box.X + box.Width - 20, box.X + dx), box.Y, Math.Max(20, box.Width - dx),
                    box.Height);
                break;
            case ResizeHandle.E:
                box = new Rect(box.X, box.Y, Math.Max(20, box.Width + dx), box.Height);
                break;
            case ResizeHandle.NW:
                box = new Rect(Math.Min(box.X + box.Width - 20, box.X + dx),
                    Math.Min(box.Y + box.Height - 20, box.Y + dy),
                    Math.Max(20, box.Width - dx), Math.Max(20, box.Height - dy));
                break;
            case ResizeHandle.NE:
                box = new Rect(box.X, Math.Min(box.Y + box.Height - 20, box.Y + dy),
                    Math.Max(20, box.Width + dx), Math.Max(20, box.Height - dy));
                break;
            case ResizeHandle.SW:
                box = new Rect(Math.Min(box.X + box.Width - 20, box.X + dx), box.Y,
                    Math.Max(20, box.Width - dx), Math.Max(20, box.Height + dy));
                break;
            case ResizeHandle.SE:
                box = new Rect(box.X, box.Y, Math.Max(20, box.Width + dx), Math.Max(20, box.Height + dy));
                break;
        }

        return box;
    }

    private static Color ToColor(uint argb, float opacity)
    {
        var a = (byte)((argb >> 24) & 0xFF);
        var r = (byte)((argb >> 16) & 0xFF);
        var g = (byte)((argb >> 8) & 0xFF);
        var b = (byte)(argb & 0xFF);
        var finalAlpha = (byte)(a * opacity);
        return Color.FromArgb(finalAlpha, r, g, b);
    }

    private void SafeInvalidate()
    {
        if (IsHandleCreated && InvokeRequired)
        {
            BeginInvoke(new Action(Invalidate));
            return;
        }

        Invalidate();
    }
}