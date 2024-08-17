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

        // Using a slight variation on how we start an Avalonia app.
        // This time, we'll create window here and pass it as an
        // argument to our application object's constructor. That way,
        // the object doesn't have to expose it as a public member.

        var win = new Window();
        
        // Since we don't assign this reference to anything, why
        // doesn't the garbage collector get rid of it right
        // after we create it? Because the constructor assigns
        // references to some the object's methods as event
        // handlers to Avalonia controls it adds to the window
        // it gets as an argument. The window persists, so the
        // controls persist, so their lists of event handlers
        // persist, so the object that handles those events
        // persists.
        
        new OurApplication(win);

        win.Show();
        app.Run(win);
    }
}