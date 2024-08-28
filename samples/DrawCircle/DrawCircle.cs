using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class DrawCircle
{
    public Window win;

    Canvas canvas;
    int numSegments = 100;
    float fractionalRadius = .8f;
    public DrawCircle()
    {
        float winHeight = 360;
        float winWidth = 640;
        float radius = fractionalRadius * winHeight / 2;

        win = new Window
        {
            Height = winHeight,
            Width = winWidth,
            Title = "DrawCircle 1.0",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        canvas = new Canvas
        {
            Background = Brushes.Black,
        };

        win.Resized += Draw;
        win.Content = canvas;
        win.Show();
    }

    void Draw(object sender, WindowResizedEventArgs e)
    {
        canvas.Children.Clear();

        float smallerDim = (float)win.Height;

        if (win.Width < smallerDim)
        {
            smallerDim = (float)win.Width;
        }

        float radius = fractionalRadius * smallerDim / 2;

        float x0 = radius;
        float y0 = 0;

        float xc = (float)win.Width / 2;
        float yc = (float)win.Height / 2;

        for (int segmentNum = 1; segmentNum < numSegments; ++segmentNum)
        {
            float theta = segmentNum * 2f * (float)Math.PI / numSegments;

            float x1 = radius * (float)Math.Cos(theta);
            float y1 = radius * (float)Math.Sin(theta);

            AddLine(canvas, xc + x0, yc + y0, xc + x1, yc + y1);

            x0 = x1;
            y0 = y1;
        }

        AddLine(canvas, xc + x0, yc + y0, xc + radius, yc + 0);
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