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

        // This time, we'll let a new class do all the work of creating
        // and populating the window. Note that we are still calling a
        // static method here. This is a use of the "factory" pattern to
        // create the new window. The method will still have to return a
        // reference to the window so the Application object can run it.

	    var win = LabelStack.Create();

        win.Show();
        app.Run(win);
    }
}