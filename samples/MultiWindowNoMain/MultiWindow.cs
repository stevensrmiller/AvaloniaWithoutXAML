using Avalonia.Controls;
using Avalonia.Layout;

internal class MultiWindow
{
    public Window win;

    // A simple UI with a button to open a new window.

    Button button = new Button
    {
        FontSize = 36,
        Content = "Open",
        HorizontalAlignment = HorizontalAlignment.Center,
    };
    public MultiWindow()
    {
        button.Click += (s, e) => new MultiWindow();

        win = new Window
        {
            Title = "No Main Window Here",
            Height = 240,
            Width = 320,
            Content = button,
        };

        win.Show();
    }
}