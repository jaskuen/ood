// See https://aka.ms/new-console-template for more information

using System.Text;
using Slides.Lib.Canvas;
using Slides.Lib.Canvas.Implementation;
using Slides.Lib.Shapes;
using Slides.Lib.Shapes.Implementation;

ISlide slide = new Slide();
StringWriter writer = new StringWriter();

using (SvgCanvas canvas = new SvgCanvas(writer))
{
    // Define colors
    RgbaColor orange = new RgbaColor(255, 165, 0, 1);     // Orange for head/body
    RgbaColor pink = new RgbaColor(255, 182, 193, 1);     // Pink for ears/nose
    RgbaColor green = new RgbaColor(0, 128, 0, 1);        // Green for eyes
    RgbaColor black = new RgbaColor(0, 0, 0, 1);          // Black for outlines
    RgbaColor white = new RgbaColor(255, 255, 255, 1);    // White for background

    // Set slide background
    slide.SetBackgroundColor(white);

    // Head: Ellipse
    Shape head = new Ellipse(new Point(400, 250), 100, 80);
    head.SetFillStyle(true, orange);
    head.SetLineStyle(true, black, 2);
    slide.InsertShape(head);

    // Left Ear: Triangle
    Shape leftEar = new Triangle(320, 200, 360, 120, 400, 180);
    leftEar.SetFillStyle(true, pink);
    leftEar.SetLineStyle(true, black, 2);
    slide.InsertShape(leftEar);

    // Right Ear: Triangle
    Shape rightEar = new Triangle(480, 200, 440, 120, 400, 180);
    rightEar.SetFillStyle(true, pink);
    rightEar.SetLineStyle(true, black, 2);
    slide.InsertShape(rightEar);

    // Left Eye: Ellipse
    Shape leftEye = new Ellipse(new Point(360, 230), 15, 20);
    leftEye.SetFillStyle(true, green);
    leftEye.SetLineStyle(true, black, 1);
    slide.InsertShape(leftEye);

    // Right Eye: Ellipse
    Shape rightEye = new Ellipse(new Point(440, 230), 15, 20);
    rightEye.SetFillStyle(true, green);
    rightEye.SetLineStyle(true, black, 1);
    slide.InsertShape(rightEye);

    // Nose: Triangle
    Shape nose = new Triangle(390, 270, 410, 270, 400, 290);
    nose.SetFillStyle(true, pink);
    nose.SetLineStyle(true, black, 1);
    slide.InsertShape(nose);

    // Body: Rectangle
    Shape body = new Rectangle(350, 330, 450, 500);
    body.SetFillStyle(true, orange);
    body.SetLineStyle(true, black, 2);
    slide.InsertShape(body);

    // Whiskers: Four thin Rectangles
    Shape whisker1 = new Rectangle(340, 270, 380, 272);
    whisker1.SetFillStyle(false, black);
    whisker1.SetLineStyle(true, black, 1);
    slide.InsertShape(whisker1);

    Shape whisker2 = new Rectangle(340, 280, 380, 282);
    whisker2.SetFillStyle(false, black);
    whisker2.SetLineStyle(true, black, 1);
    slide.InsertShape(whisker2);

    Shape whisker3 = new Rectangle(420, 270, 460, 272);
    whisker3.SetFillStyle(false, black);
    whisker3.SetLineStyle(true, black, 1);
    slide.InsertShape(whisker3);

    Shape whisker4 = new Rectangle(420, 280, 460, 282);
    whisker4.SetFillStyle(false, black);
    whisker4.SetLineStyle(true, black, 1);
    slide.InsertShape(whisker4);

    // Draw the slide
    slide.Draw(canvas);
}

Console.WriteLine("Enter file name:");
string userInput;
if (!string.IsNullOrWhiteSpace(userInput = Console.ReadLine()!.ToLower()))
{
    using FileStream fs = new(userInput, FileMode.Create);
    fs.Write(Encoding.UTF8.GetBytes(writer.ToString()));
}