// We can have as many active window as we want. Each one can
// be created along with a new object.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Embedding.Offscreen;
using Avalonia.Layout;

class Project
{
    public static void Main(string[] args)
    {
        AppBuilder.Configure<Application>()
                  .UsePlatformDetect()
                  .Start(AppMain, args);
    }

    public static void AppMain(Application app, string[] args)
    {
        app.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
        app.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;

        var win = new Window
        {
            Title = "MultiWindowGraphcs",
            Height = 24*9,
            Width = 24*16,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        var button = new Button
        {
            Content = "Start",
            FontSize = 36,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
        };

        // Click the button to create a new object of the GraphicsWindow
        // class. That's not an Avalonia class. It's part of this sample.
        // Its constructor will create an Avalonia window and call that
        // window's Show method. It will also assign references to one
        // of its methods to events in the Canvas control that will be
        // the content of that window. So, even though the new object
        // isn't assigned to a variable, it still persists after being
        // created with "new" in the code below, because references to
        // it are held by the events.
        //
        // Note the use of a lambda here to simplify the button handler.

        button.Click += (s, e) => new GraphicsWindow();

        win.Content = button;
        win.Show();
        app.Run(win);
    }
}