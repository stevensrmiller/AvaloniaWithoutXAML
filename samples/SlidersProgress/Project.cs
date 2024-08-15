using Avalonia;
using Avalonia.Controls;

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

    // We'll create a new SlidersProgressWindow, but remember that it isn't
    // a subclass of Window. So, to set the lifetime of this app to the
    // actual Window lifetime, SlidersProgressWindow exposes its Window as
    // a public member field.
    
        SlidersProgressWindow spWin = new SlidersProgressWindow();

        app.Run(spWin.win);
    }
}