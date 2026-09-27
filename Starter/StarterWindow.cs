using Avalonia.Controls;
using Avalonia.Media;

internal class MyProject
{
    public Window win;

    public MyProject()
    {
        win = new Window
        {
            Title = "MyProject v0.1",
            Height = 720,
            Width = 1280,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        win.Show();
    }
}
