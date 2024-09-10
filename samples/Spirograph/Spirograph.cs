using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class Spirograph
{
    public Window win;

    private int numberOfLoops = 50;
    private int stepsPerLoop = 200;

    private int initHeight = 720;
    private int initWidth = 1280;

    private float innerToOuterDistanceRatio = 10000f;

    private float frequency = -2.01f;

    private Canvas canvas;
    public Spirograph()
    {
        win = new Window
        {
            Title = "Spirograph v0.1",
            Height = initHeight,
            Width = initWidth,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        canvas = new Canvas
        {
            Background = Brushes.Black,
        };

        win.Content = canvas;

        win.Resized += Redraw;
        win.Show();
    }

    private void Redraw(object sender, WindowResizedEventArgs e)
    {
        float height = (float)win.Height;
        float width = (float)win.Width;

        canvas.Height = height;
        canvas.Width = width;

        float xCenter = width / 2f;
        float yCenter = height / 2f;

        float outerArmLength = height / (2f * (1 + innerToOuterDistanceRatio));
        float innerArmLength = height / 2f - outerArmLength;
        float deltaTheta = 2 * (float)Math.PI / stepsPerLoop;

        float innerTheta = 0;
        float outerTheta = 0;

        int totalSteps = numberOfLoops * stepsPerLoop;

        Point[] points = new Point[totalSteps + 1];

        float x = innerArmLength + outerArmLength;
        float y = 0;

        points[0] = new Point(x + xCenter, y + yCenter);

        for (int step = 1; step <= totalSteps; step++)
        {
            innerTheta = innerTheta + deltaTheta;
            
            while (innerTheta > 2 * (float)Math.PI)
            {
                innerTheta = innerTheta - 2 * (float)Math.PI;
            }

            while (outerTheta > 2 * (float)Math.PI)
            {
                outerTheta = outerTheta - 2 * (float)Math.PI;
            }

            float innerX = innerArmLength * (float)Math.Cos(innerTheta);
            float innerY = innerArmLength * (float)Math.Sin(innerTheta);
            
            float outerX = 0;
            float outerY = 0;

            x = innerX + outerX;
            y = innerY + outerY;

            points[step] = new Point(x + xCenter, y + yCenter);
        }

        canvas.Children.Clear();
        canvas.Children.Add(new Polyline {Stroke = Brushes.White, StrokeThickness = 1.5, Points = points,} );
    }
}