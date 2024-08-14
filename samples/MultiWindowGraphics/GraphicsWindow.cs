// This class actually creates its own window and
// shows it. Thus, its methods are actual object
// methods, not statics.

using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class GraphicsWindow
{
    Random rand = new Random();
    public GraphicsWindow()
    {
        var win = new Window
        {
            Title = "GraphicsWindow",
            Height = 240,
            Width = 320,
        };

        var canvas = new Canvas
        {
            Background = Brushes.Navy,
        };

        // Whenever the canvas is clicked or changes size,
        // call the Draw method to fill it with colorful
        // rectangles. Note that this means a reference is
        // held to this object in two of the canvas's events,
        // so you don't need to assign the object reference
        // to anything when you use "new GraphicsWindow()."

        canvas.PointerPressed += (s, e) => Draw(canvas);
        canvas.SizeChanged += (s, e) => Draw(canvas);

        win.Content = canvas;

        // The SizeChanged event will be raised when we show
        // the window, so the canvas will have rectangles in it
        // as soon as it appears.

        win.Show();
    }

    void Draw(Canvas canvas)
    {
        // The canvas retains all the rectangles, so it can
        // redraw them when it needs to. We'll start by clearing
        // all of those so the canvas will be blank.

        canvas.Children.Clear();

        // Get the current dimensions of the canvas.

        float cw = (float)canvas.Bounds.Width;
        float ch = (float)canvas.Bounds.Height;

        // Draw some rectangles of random sizes and colors,
        // making sure each one fits inside the canvas.

        for (int i = 0; i < 100; ++i)
        {
            var b = new SolidColorBrush();

            b.Color = new Color(0XFF, 
                (byte)(256 * rand.NextDouble()),
                (byte)(256 * rand.NextDouble()),
                (byte)(256 * rand.NextDouble()));

            float w = cw * (float)rand.NextDouble();
            float h = ch * (float)rand.NextDouble();

            float x = (cw - w) * (float)rand.NextDouble();
            float y = (ch - h) * (float)rand.NextDouble();

            Rectangle r = new Rectangle{Fill = b, Width = w, Height = h};

            // Note that the position of the rectangle isn't a
            // field of the rectangle object. You have to give
            // it such property with the SetValue method.

            r.SetValue(Canvas.LeftProperty, x);
            r.SetValue(Canvas.TopProperty, y);

            // Now add the new rectangle as another child of
            // the canvas.

            canvas.Children.Add(r);
        }
    }
}