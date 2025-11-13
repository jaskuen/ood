using Slides.Lib.Canvas;
using Slides.Lib.Shapes;
using Slides.Lib.Shapes.Implementation;

namespace Slides.Tests;

using NUnit.Framework;
using Moq;
using System.Collections.Generic;

[TestFixture]
public class ShapeGroupTests
{
    private ShapeGroup _shapeGroup;
    private Mock<ICanvas> _canvasMock;
    private Mock<Shape> _shape1Mock;
    private Mock<Shape> _shape2Mock;

    [SetUp]
    public void SetUp()
    {
        _shapeGroup = new ShapeGroup([]);
        _canvasMock = new Mock<ICanvas>();
        _shape1Mock = new Mock<Shape>();
        _shape2Mock = new Mock<Shape>();
    }

    [TearDown]
    public void TearDown()
    {
        _shapeGroup.Dispose();
    }

    [Test]
    public void InsertShape_AddsShapeAtPosition()
    {
        // Act
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 0);

        // Assert
        Assert.That(_shapeGroup.GetShapesCount(), Is.EqualTo(2));
        Assert.That(_shapeGroup.GetShapeAt(0), Is.EqualTo(_shape2Mock.Object));
        Assert.That(_shapeGroup.GetShapeAt(1), Is.EqualTo(_shape1Mock.Object));
        _shape1Mock.Verify(s => s.SetParent(_shapeGroup), Times.Once());
        _shape2Mock.Verify(s => s.SetParent(_shapeGroup), Times.Once());
    }

    [Test]
    public void RemoveShapeAt_RemovesShapeAtIndex()
    {
        // Arrange
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        _shapeGroup.RemoveShapeAt(0);

        // Assert
        Assert.That(_shapeGroup.GetShapesCount(), Is.EqualTo(1));
        Assert.That(_shapeGroup.GetShapeAt(0), Is.EqualTo(_shape2Mock.Object));
    }

    [Test]
    public void GetShapeAt_ValidIndex_ReturnsShape()
    {
        // Arrange
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);

        // Act
        var shape = _shapeGroup.GetShapeAt(0);

        // Assert
        Assert.That(shape, Is.EqualTo(_shape1Mock.Object));
    }

    [Test]
    public void GetShapesCount_ReturnsCorrectCount()
    {
        // Arrange
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        var count = _shapeGroup.GetShapesCount();

        // Assert
        Assert.That(count, Is.EqualTo(2));
    }

    [Test]
    public void GetLineStyle_AllShapesSameStyle_ReturnsStyle()
    {
        // Arrange
        var style = new LineStyle(true, new RgbaColor(255, 0, 0, 1), 2);
        _shape1Mock.Setup(s => s.GetLineStyle()).Returns(style);
        _shape2Mock.Setup(s => s.GetLineStyle()).Returns(style);
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        var result = _shapeGroup.GetLineStyle();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.IsEnabled, Is.True);
        Assert.That(result?.Color, Is.EqualTo(style.Color));
        Assert.That(result?.Width, Is.EqualTo(2));
    }

    [Test]
    public void GetLineStyle_DifferentStyles_ReturnsNull()
    {
        // Arrange
        var style1 = new LineStyle(true, new RgbaColor(255, 0, 0, 1), 2);
        var style2 = new LineStyle(true, new RgbaColor(0, 255, 0, 1), 2);
        _shape1Mock.Setup(s => s.GetLineStyle()).Returns(style1);
        _shape2Mock.Setup(s => s.GetLineStyle()).Returns(style2);
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        var result = _shapeGroup.GetLineStyle();

        // Assert
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public void GetLineStyle_FirstDifferentThenSameStyles_ReturnsStyle()
    {
        // Arrange
        var style1 = new LineStyle(true, new RgbaColor(255, 0, 0, 1), 2);
        var style2 = new LineStyle(true, new RgbaColor(0, 255, 0, 1), 3);
        _shape1Mock.Setup(s => s.GetLineStyle()).Returns(style1);
        _shape2Mock.Setup(s => s.GetLineStyle()).Returns(style2);
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);
        
        _shape2Mock.Setup(s => s.GetLineStyle()).Returns(style1);
        _shapeGroup.UpdateStrokeStyle();

        // Act
        var result = _shapeGroup.GetLineStyle();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.IsEnabled, Is.True);
        Assert.That(result?.Color, Is.EqualTo(style1.Color));
        Assert.That(result?.Width, Is.EqualTo(2));
    }

    [Test]
    public void GetFillStyle_AllShapesSameStyle_ReturnsStyle()
    {
        // Arrange
        var style = new FillStyle(true, new RgbaColor(0, 0, 255, 0.5));
        _shape1Mock.Setup(s => s.GetFillStyle()).Returns(style);
        _shape2Mock.Setup(s => s.GetFillStyle()).Returns(style);
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        var result = _shapeGroup.GetFillStyle();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.IsEnabled, Is.True);
        Assert.That(result?.Color, Is.EqualTo(style.Color));
    }

    [Test]
    public void GetFillStyle_DifferentStyles_ReturnsNull()
    {
        // Arrange
        var style1 = new FillStyle(true, new RgbaColor(0, 0, 255, 0.5));
        var style2 = new FillStyle(true, new RgbaColor(255, 0, 0, 0.5));
        _shape1Mock.Setup(s => s.GetFillStyle()).Returns(style1);
        _shape2Mock.Setup(s => s.GetFillStyle()).Returns(style2);
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        var result = _shapeGroup.GetFillStyle();

        // Assert
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public void GetFillStyle_FirstDifferentThenSameStyles_ReturnsStyle()
    {
        // Arrange
        var style1 = new FillStyle(true, new RgbaColor(0, 0, 255, 0.5));
        var style2 = new FillStyle(true, new RgbaColor(255, 0, 0, 0.5));
        _shape1Mock.Setup(s => s.GetFillStyle()).Returns(style1);
        _shape2Mock.Setup(s => s.GetFillStyle()).Returns(style2);
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);
        
        _shape2Mock.Setup(s => s.GetFillStyle()).Returns(style1);
        _shapeGroup.UpdateFillStyle();

        // Act
        var result = _shapeGroup.GetFillStyle();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result?.IsEnabled, Is.True);
        Assert.That(result?.Color, Is.EqualTo(style1.Color));
    }

    [Test]
    public void DoDraw_CallsDrawOnAllShapes()
    {
        // Arrange
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        _shapeGroup.Draw(_canvasMock.Object);

        // Assert
        _shape1Mock.Verify(s => s.Draw(_canvasMock.Object), Times.Once());
        _shape2Mock.Verify(s => s.Draw(_canvasMock.Object), Times.Once());
    }

    [Test]
    public void SetLineStyle_PropagatesToChildren()
    {
        // Arrange
        var color = new RgbaColor(100, 100, 100, 1);
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        _shapeGroup.SetLineStyle(true, color, 3);

        // Assert
        _shape1Mock.Verify(s => s.SetLineStyle(true, color, 3), Times.Once());
        _shape2Mock.Verify(s => s.SetLineStyle(true, color, 3), Times.Once());
    }

    [Test]
    public void SetFillStyle_PropagatesToChildren()
    {
        // Arrange
        var color = new RgbaColor(50, 50, 50, 0.8);
        _shapeGroup.InsertShape(_shape1Mock.Object, 0);
        _shapeGroup.InsertShape(_shape2Mock.Object, 1);

        // Act
        _shapeGroup.SetFillStyle(true, color);

        // Assert
        _shape1Mock.Verify(s => s.SetFillStyle(true, color), Times.Once());
        _shape2Mock.Verify(s => s.SetFillStyle(true, color), Times.Once());
    }

    [Test]
    public void GetShapesGroup_ReturnsSelf()
    {
        // Act
        var result = _shapeGroup.GetShapesGroup();

        // Assert
        Assert.That(result, Is.EqualTo(_shapeGroup));
    }

    [Test]
    public void Dispose_CallsDisposeOnAllShapes()
    {
        // Arrange
        Mock<ShapeGroup> newShapeGroup = new Mock<ShapeGroup>([]);
        newShapeGroup.Setup(s => s.GetShapesGroup()).Returns(newShapeGroup.Object);
        _shapeGroup.InsertShape(newShapeGroup.Object, 0);

        // Act
        _shapeGroup.Dispose();

        // Assert
        newShapeGroup.Verify(s => s.Dispose(), Times.Once());
    }
}