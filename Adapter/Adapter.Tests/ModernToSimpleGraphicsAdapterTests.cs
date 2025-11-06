using Adapter.GraphicsLib;
using Adapter.LibsAdapter;
using Adapter.ModernGraphicsLib;

namespace Adapter.Tests;

[TestFixture]
public class ModernToSimpleGraphicsAdapterTests
{
    private StringWriter _stringWriter;
    private ModernGraphicsRenderer _renderer;
    private ModernToSimpleGraphicsAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _stringWriter = new StringWriter();
        _renderer = new ModernGraphicsRenderer(_stringWriter);
        _adapter = new ModernToSimpleGraphicsAdapter(_renderer);
    }

    [TearDown]
    public void TearDown()
    {
        _renderer.Dispose();
        _stringWriter.Dispose();
    }

    [Test]
    public void Constructor_CallsBeginDraw()
    {
        // Arrange
        var writer = new StringWriter();
        var renderer = new ModernGraphicsRenderer(writer);

        // Act
        // ReSharper disable once NotAccessedVariable
        ICanvas adapter;
        Assert.DoesNotThrow(() => adapter = new ModernToSimpleGraphicsAdapter(renderer));

        // Assert
        StringAssert.Contains("<draw>", writer.ToString());
    }

    [Test]
    public void MoveTo_UpdatesCurrentPosition()
    {
        // Act
        _adapter.MoveTo(10, 20);

        // Assert (indirectly via LineTo behavior)
        _adapter.LineTo(30, 40);
        var output = _stringWriter.ToString();
        StringAssert.Contains("<line fromX=\"10\" fromY=\"20\" toX=\"30\" toY=\"40\">", output);
        StringAssert.Contains("<color r=\"0\" g=\"0\" b=\"0\" a=\"1\" />", output);
    }

    [Test]
    public void LineTo_CallsDrawLineWithCorrectPointsAndDefaultColor()
    {
        // Act
        _adapter.MoveTo(5, 10);
        _adapter.LineTo(15, 25);

        // Assert
        var output = _stringWriter.ToString();
        StringAssert.Contains("<draw>", output);
        StringAssert.Contains("<line fromX=\"5\" fromY=\"10\" toX=\"15\" toY=\"25\">", output);
        StringAssert.Contains("<color r=\"0\" g=\"0\" b=\"0\" a=\"1\" />", output);
    }

    [Test]
    public void SetColor_UpdatesColorForSubsequentLineTo()
    {
        // Act
        _adapter.SetColor(0xFF8000); // Orange: R=255, G=128, B=0
        _adapter.MoveTo(5, 10);
        _adapter.LineTo(15, 25);

        // Assert
        var output = _stringWriter.ToString();
        StringAssert.Contains("<line fromX=\"5\" fromY=\"10\" toX=\"15\" toY=\"25\">", output);
        StringAssert.Contains("<color r=\"1\" g=\"0,5019608\" b=\"0\" a=\"1\" />", output);
    }

    [Test]
    public void MultipleOperations_TracksPositionAndColorCorrectly()
    {
        // Act
        _adapter.MoveTo(0, 0);
        _adapter.LineTo(10, 10);
        _adapter.SetColor(0x00FF00); // Green: R=0, G=255, B=0
        _adapter.MoveTo(20, 30);
        _adapter.LineTo(40, 50);

        // Assert
        var output = _stringWriter.ToString();
        StringAssert.Contains("<line fromX=\"0\" fromY=\"0\" toX=\"10\" toY=\"10\">", output);
        StringAssert.Contains("<color r=\"0\" g=\"0\" b=\"0\" a=\"1\" />", output);
        StringAssert.Contains("<line fromX=\"20\" fromY=\"30\" toX=\"40\" toY=\"50\">", output);
        StringAssert.Contains("<color r=\"0\" g=\"1\" b=\"0\" a=\"1\" />", output);
    }

    [Test]
    public void Dispose_RendererEndsDraw()
    {
        // Act
        _renderer.Dispose(); // Explicitly dispose renderer

        // Assert
        StringAssert.Contains("</draw>", _stringWriter.ToString());
    }

    [Test]
    public void LineTo_WithoutMoveTo_UsesLastPosition()
    {
        // Act
        _adapter.LineTo(100, 200);
        var output = _stringWriter.ToString();

        // Assert
        StringAssert.Contains("<line fromX=\"0\" fromY=\"0\" toX=\"100\" toY=\"200\">", output);
        StringAssert.Contains("<color r=\"0\" g=\"0\" b=\"0\" a=\"1\" />", output);
    }

    [Test]
    public void SetColor_InvalidColorValue_StillProcessesCorrectly()
    {
        // Act
        _adapter.SetColor(unchecked((int)0xFFFFFFFF)); // Invalid high bits
        _adapter.MoveTo(5, 10);
        _adapter.LineTo(15, 25);

        // Assert
        var output = _stringWriter.ToString();
        StringAssert.Contains("<line fromX=\"5\" fromY=\"10\" toX=\"15\" toY=\"25\">", output);
        StringAssert.Contains("<color r=\"1\" g=\"1\" b=\"1\" a=\"1\" />", output);
    }
}