using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Layout;
using Avalonia;
using System;

internal class ChexWindow
{
    const float boxWidth = 160;
    const float boxHeight = 90;
    const int screenWidth = 1280;
    const int screenHeight = 720;
    public Window win;

    private Canvas canvas;
    private Box box;
    public ChexWindow()
    {
        MakeDisplay();
        MakeBox();
    }

    private void OnPointerPressedBox()
    {
        Console.WriteLine("I need to be replaced with code!");
        // Make the box move and turn here.
    }
    private void MakeDisplay()
    {
        win = new Window
        {
            Title = "ChexWindow v0.1",
            Height = screenHeight,
            Width = screenWidth,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            CanResize = false,
        };

        canvas = new Canvas
        {
            Background = Brushes.Black,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };
        win.Content = canvas;

        win.PointerPressed += (s, a) => OnPointerPressedBox();

        win.Show();
    }

    /// <summary>
    /// 
    /// </summary>
    private void MakeBox()
    {
        // You can make a solid color rectangle by creating objects of the
        // "Box" class. The constructor takes these arguments:
        //
        // width:   the horizontal size of the box in pixels
        // height:  the vertical size of the box in piexels
        // center:  a Vector you create with "new," that sets
        //          the coordinate inside the window of the
        //          the center of the box.
        // color:   use "RealColor.Color(r, g, b)" here, where
        //          r, g, and b are each a float from 0 to 1.

        box = new Box(
            boxWidth, boxHeight,
            new Vector(screenWidth / 2, screenHeight / 2),
            RealColor.Color(0, .8f, .4f));

        // Make the box visible in your canvas by adding its
        // "Polygon" public member. (This is an Avalonia Polygon).

        canvas.Children.Add(box.Polygon);

        // If you keep a reference to your box in a variable of
        // type Box, you can use tha reference to call your box's
        // Move and Rotate methods. The argument Move is a Vector
        // that tells the box how many pixels to move horizontally
        // how many pixels to move verticall. The argument to
        // Rotate is a float that tells the box how many radians
        // to rotate around its own center.
    }
}