using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Platform.Storage;

class Project
{
    static Window win;
    static Label label;
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

        win = new Window
        {
            Title = "My Avalonia App",
            Height = 360,
            Width = 640,
        };

        var button = new Button
        {
            Content = "Open",
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        button.Click += (s, e) => OpenFile();

        label = new Label
        {
            Content = "File names will appear here.",
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        var sp = new StackPanel();

        sp.Children.Add(button);
        sp.Children.Add(label);

        win.Content = sp;

        win.Show();
        app.Run(win);
    }

    // Open the filepicker and let the user select a file. Note that
    // cancelation means zero files were picked. This is an async
    // method, but your calling code doesn't have to do any thread
    // or task management. Avalonia takes care of that. But if you try
    // to wait on the OpenFilePickerAsync method, your application
    // will hang, so don't do it.

    static async void OpenFile()
    {
        // You need access to an implementation of the
        // IStorageProvider interface. Fortunately, a Window
        // has one.

        Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);

        var files = await win.StorageProvider.OpenFilePickerAsync
        (
            new FilePickerOpenOptions
            {
                Title = "Pick a File, any File",
                AllowMultiple = true, // set this false to pick only one
            }
        );

        // The await operator above makes this code appear to run
        // single-threaded. When it deblocks, we're still on the
        // UI thread, so we can assign to the label here.

        label.Content = "";

        foreach(var fileName in files)
        {
            label.Content += $"{fileName.Name} ";
        }
    }
}