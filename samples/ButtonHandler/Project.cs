using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
class Project
{
    // We're using statics again, but only to avoid creating an object
    // that we don't really need for this example.

    static int count;
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

        var win = new Window
        {
            Title = "Button Handler",
            Height = 512,
            Width = 512,
        };

        var stack = new StackPanel();

        var button = new Button
        {
            Content = "Press Me!",
            FontSize = 24,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        // Add a reference to the handler method for the Click event.
        // Note that the method can be private.

        button.Click += DoClick;

        label = new Label
        {
            Content = "I will count button clicks.",
        };

        // Assemble the controls into the hierarchy: the stack contains
        // the label and the button; the window contains the stack.

        stack.Children.Add(label);
        stack.Children.Add(button);
        win.Content = stack;

        win.Show();
        app.Run(win);
    }

    // When the button is clicked, Avalonia will call this method.
    static void DoClick(object sender, RoutedEventArgs args)
    {
        // Just increment the counter.

        count = count + 1;

        // And update the label's contents.

        label.Content = $"Clicks so far: {count}";
    }
}