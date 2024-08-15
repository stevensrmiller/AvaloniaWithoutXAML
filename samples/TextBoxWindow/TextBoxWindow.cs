using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

internal class TextBoxWindow
{
    public Window win;
    Label lbl;
    public TextBoxWindow()
    {
        win = new Window
        {
            Title = "InputTextWindow v2.0",
            Height = 360,
            Width = 640,
            Background = Brushes.Orange,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        var sp = new StackPanel();

        var it = new TextBox()
        {
            FontSize = 24,
        };

        // Every time the text chanbes, we want our handler called.
        //
        // Most of the time, you wouldn't use this. You'd respond to
        // some other event, like a button press, and query the TextBox.

        it.TextChanged += TextChanged;
        
        sp.Children.Add(it);

        lbl = new Label()
        {
            Background = Brushes.Aquamarine,
            FontSize = 24,
            Content = " ",
        };

        sp.Children.Add(lbl);

        win.Content = sp;
        win.Show();
    }

    void TextChanged(object s, RoutedEventArgs e)
    {
        lbl.Content = ((TextBox)s).Text;
    } 
}