using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class DrawSquareBetter
{
    public Window win;

    public DrawSquareBetter()
    {
        float winHeight = 360;
        float winWidth = 640;
        float squareSize = 180;

        win = new Window
        {
            Height = winHeight,
            Width = winWidth,
            Title = "DrawSquareBetter 1.0",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        Canvas canvas = new Canvas
        {
            Background = Brushes.Black,
        };

        float centerX = winWidth / 2;
        float centerY = winHeight / 2;
        float halfSquare = squareSize / 2;

        AddLine(canvas,
            centerX - halfSquare, centerY - halfSquare,  // start
            centerX + halfSquare, centerY - halfSquare); // end
        
        AddLine(canvas,
            centerX + halfSquare, centerY - halfSquare,  // start
            centerX + halfSquare, centerY + halfSquare); // end
        
        AddLine(canvas,
            centerX + halfSquare, centerY + halfSquare,  // start
            centerX - halfSquare, centerY + halfSquare); // end
        
        AddLine(canvas,
            centerX - halfSquare, centerY + halfSquare,  // start
            centerX - halfSquare, centerY - halfSquare); // end

        win.Content = canvas;
        win.Show();
    }

    void AddLine(Canvas canvas,
        float startX, float startY,
        float endX, float endY)
    {
        Line line = new Line
        {
            StartPoint = new Point(startX, startY),
            EndPoint = new Point(endX, endY),
            Stroke = Brushes.White,
            StrokeThickness = 1.5,
        };
        
        canvas.Children.Add(line);
    }
}