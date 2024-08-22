using System.IO;
using System.Reflection;
using System.Media;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.FileProviders;

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
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
        var sp = new SoundPlayer();

        using (var reader = embeddedProvider.GetFileInfo("Whoop.wav").CreateReadStream())
        {
            var sr = new StreamReader(reader);

            sp.Stream = reader;
            sp.LoadAsync();
        }

        app.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
        app.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;

        var win = new Window
        {
            Title = "My Sound App",
            Height = 90,
            Width = 160,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        var button = new Button
        {
            Content = "Play",
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
        };

        button.Click += (s, e) => sp.Play();
 
        win.Content = button;
        win.Show();
        app.Run(win);
    }
}