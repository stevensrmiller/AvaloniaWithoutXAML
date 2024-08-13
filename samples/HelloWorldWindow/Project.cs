// At a minimum use these namespaces for all Avalonia apps.
// Most of the time, you'll need more than just thest two.

using Avalonia;
using Avalonia.Controls;

class Project
{
    public static void Main(string[] args)
    {
        // All Avalonia applications will start this way, so use
        // this code as it is. Note that the Configure and the
        // UsePlatformDetect methods return a reference to the
        // appBuilder object. You will often see this used to
        // collapse all of the code below into one line. Note that
        // Start method does NOT return anything.

        AppBuilder appBuilder;

        appBuilder = AppBuilder.Configure<Application>();
        appBuilder.UsePlatformDetect();
        appBuilder.Start(AppMain, args);
    }

    // Like Main, this is another static method. Avalonia doesn't
    // require that you use static methods, but this example is
    // kept short by not creating any new objects.

    public static void AppMain(Application app, string[] args)
    {
        // Avalonia has two built-in themes that manage the appearance
        // of all of its controls. We will use the "Fluent" theme. The
        // other is called "Simple," and is probably best used for
        // mobile applications. Always begin your application code by
        // adding a theme, as below.

        app.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());

        // Some themes have variants. You can choose Light, Dark, or
        // Default here.

        app.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;

        // You will need a top-level window to display your application.
        // In this example, we will create the window and, at the same
        // time, set its title text to be "Hello World." That's all we
        // are going to do, but it's enough to verify that we can build
        // and run an Avalonia app.

        var win = new Window
        {
            Title = "Hello World",
        };

        // Make the window visible.

        win.Show();

        // Start the application running with the window you created.
        
        app.Run(win);
    }
}