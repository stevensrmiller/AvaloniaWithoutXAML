using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

public class SimpleWindow
{
    const float winHeight = 300;
    public Window win;

    public SimpleWindow()
    {
        win = new Window
        {
            Height = winHeight,
            Width = 1280,
            Background = Brushes.Orange,
            Title = "My Toy Program Window",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        Line line = new Line
        {
            StrokeThickness = 5f,
            Stroke = Brushes.White,
            StartPoint = new Avalonia.Point(0, winHeight / 2),
            EndPoint = new Avalonia.Point(300, 100),
        };

        win.Content = line;

        win.Show();
    }
}