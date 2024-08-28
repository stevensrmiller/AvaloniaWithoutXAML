using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class DrawSquare
{
    public Window win;

    public DrawSquare()
    {
        win = new Window
        {
            Height = 360,
            Width = 640,
            Title = "DrawSquare 1.0",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        Canvas canvas = new Canvas
        {
            Background = Brushes.Black,
        };

        Line topLine = new Line
        {
            StartPoint = new Point(230, 90),
            EndPoint = new Point(410, 90),
            Stroke = Brushes.White,
            StrokeThickness = 1.5,
        };

        Line rgtLine = new Line
        {
            StartPoint = new Point(410, 90),
            EndPoint = new Point(410, 270),
            Stroke = Brushes.White,
            StrokeThickness = 1.5,
        };

        Line botLine = new Line
        {
            StartPoint = new Point(410, 270),
            EndPoint = new Point(230, 270),
            Stroke = Brushes.White,
            StrokeThickness = 1.5,
        };

        Line lftLine = new Line
        {
            StartPoint = new Point(230, 270),
            EndPoint = new Point(230, 90),
            Stroke = Brushes.White,
            StrokeThickness = 1.5,
        };
        
        canvas.Children.Add(topLine);
        canvas.Children.Add(rgtLine);
        canvas.Children.Add(botLine);
        canvas.Children.Add(lftLine);

        win.Content = canvas;
        win.Show();
    }
}