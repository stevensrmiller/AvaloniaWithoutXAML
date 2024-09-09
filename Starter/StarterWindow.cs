using Avalonia.Controls;
using Avalonia.Media;

internal class StarterWindow
{
    public Window win;

    public StarterWindow()
    {
        win = new Window
        {
            Title = "StarterWindow v0.1",
            Height = 720,
            Width = 1280,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        win.Show();
    }
}