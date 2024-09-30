using Avalonia;
using Avalonia.Controls;

class Project
{
    const int height = 720;
    const int width = 1280;
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

        app.Run(new RepLinesWindow(height, width).win);
    }
}