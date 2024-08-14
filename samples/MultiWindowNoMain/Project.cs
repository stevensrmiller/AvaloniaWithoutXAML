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

        // Singletons are generally evil. We will use one here, however,
        // to keep track of how many windows are open, so we can shut
        // down the application when the last one closes. This will let
        // us open and close whatever windows we want to, in any order.
        // In this context, no one window is the "main" window.

        var wrangler = new Wrangler();

        new MultiWindow(wrangler);
        wrangler.Opened();

        // Instead of passing reference to an Avalonia window to the
        // Run method (which is what makes that window the overall main
        // window of the application), we will pass a CancellationToken
        // the application will monitor.

        app.Run(wrangler.Cts.Token);
    }
}