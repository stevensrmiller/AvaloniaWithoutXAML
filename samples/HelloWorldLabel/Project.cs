// We'll need the layout namespace for this.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

class Project
{
    public static void Main(string[] args)
    {
        // This is the one-line version of how most Avalonia apps start.

        AppBuilder.Configure<Application>()
                  .UsePlatformDetect()
                  .Start(AppMain, args);
    }

    public static void AppMain(Application app, string[] args)
    {
        app.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
        app.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;

        // Create a bare-bones window.

        var win = new Window();

        // Create a label. Note the alignment settings.

        var label = new Label
        {
            Content = "Hello World",
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        // Make the label be the window's content. Note that a window
        // can only contain one thing, but that's enough here.

        win.Content = label;

        // Make the window visible.

        win.Show();

        // Start the application running with the window you created.

        app.Run(win);
    }
}