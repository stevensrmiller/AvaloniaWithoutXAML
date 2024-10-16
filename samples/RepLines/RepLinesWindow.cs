using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Layout;
using Avalonia.Media;

internal class RepLinesWindow
{
    public Window win;

    private Canvas canvas;
    public RepLinesWindow(float height, float width)
    {
        win = new Window
        {
            Title = "StarterWindow v0.1",
            Height = height,
            Width = width,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        canvas = new Canvas
        {
            Background = Brushes.Black,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };

        win.Content = canvas;

        win.Resized += (s, a) => OnResized();

        win.Show();
    }

    public void OnResized()
    {
        // We'll keep all the hard numbers here in one place.
<<<<<<< Updated upstream
        int div = 2;
        int numLines = 200 / div;
        float dxs = 1.7f * div;
        float dys = 2.1f * div;
        float dxe = -4.7f * div;
        float dye = -2.9f * div;
=======

        int numLines = 200;
        float dxs = 3.7f;
        float dys = 3.1f;
        float dxe = -4.7f;
        float dye = -3.9f;
>>>>>>> Stashed changes

        canvas.Children.Clear();

        // Let's compute the initial coordinates of the line's
        // start and end points from the size of the window.

        float xStart = (float)win.Width / 4;
        float yStart = (float)win.Height / 2;

        float xEnd = 3 * (float)win.Width / 4;
        float yEnd = (float)win.Height / 2;

        // Now we'll draw the line over and over. That's the
        // "repetition" part.

        for (int i = 0; i < numLines; ++i)
        {
            // We will chage the color a bit for each line.
            // This is some of the "variation."
            // Note that the value stored in "blue" will vary
            // from zero to one, no matter what value is in
            // "numLines."

            float blue = (float)i / (numLines -1);
            float green = 1 - blue;

            // Draw the line.

            Line line = new Line
            {
                StartPoint = new Point(xStart, yStart),
                EndPoint = new Point(xEnd, yEnd),
                StrokeThickness = 2.8,
                Stroke = new SolidColorBrush(RealColor.Color(.24f, green, blue)),
            };

            canvas.Children.Add(line);

            // Now we'll change the start and end points of
            // the line a bit. This is somre more "variation."
            
            xStart += dxs;
            yStart += dys;

            xEnd += dxe;
            yEnd += dye;

            dxs = dxs - .04f;
            dys = dys - .02f;

            dxe = dxe + .04f;
            dye = dye + .034f;
        }
    }
}