using Avalonia;
using Avalonia.Controls;

// This is our standard boilerplate code for creating
// and starting an Avalonia application using C#.

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

        // Create a new UndoDots object.

        UndoDots undoDots = new UndoDots();

        // Use the UndoDots object's Avalonia Window to run the
        // Avalonia application.
        
        app.Run(undoDots.win);
    }
}