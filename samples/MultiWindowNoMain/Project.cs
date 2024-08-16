using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Themes.Fluent;

class Project
{
    public static void Main(string[] args)
    {
        // This time, we'll use an Avalonia lifetime object to shut down the
        // application only after the last open window closes.

        var lifetime = new ClassicDesktopStyleApplicationLifetime
        {
            Args = args,
            ShutdownMode = ShutdownMode.OnLastWindowClose
        };

        AppBuilder.Configure<Application>()
            .UsePlatformDetect()
            .AfterSetup(b => b.Instance?.Styles.Add(new FluentTheme()))
            .SetupWithLifetime(lifetime);


        lifetime.MainWindow = new MultiWindow().win;

        lifetime.Start(args);
    }
}