// The Fluent theme sets button backgrounds to be partially
// transparent. This works well for the various reactions
// buttons have to the mouse cursor when the window the
// buttons are in has its own background that matches the
// button's theme (white, for example, in the Fluent Light
// them variant). It doesn't work so well when the window
// background is a color or an image. We can solve that by
// changing the button's style.
//
// There may be better ways to do this, but the technique here
// could be centralized into a method call that also creates
// your buttons, so it's not impractical. It depends on using
// what Avalonia calls "pseudoclasses." These are actually
// states your button could be in, such as when it is pressed
// or when the mouse cursor is floating over the button but
// not pressing it.
//
// This example uses two buttons in a stack on a window with
// a solid color background. The top button is left with the
// style applied by the Fluent theme. The bottom bottom uses
// a style modified by the code you see here. Consider using
// different colors for the button style and the window's
// background, to find out what works best for you.

using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;

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

        // Note the use of WindowStartupLocation here.
        
        var win = new Window
        {
            Title = "Button Styles",
            Background = Brushes.Orange,
            Height = 24*9,
            Width = 24*16,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        var stack = new StackPanel();

        // Note the use of a few more appearance parameters here.

        var topButton = new Button
        {
            FontSize = 36,
            Content = "Normal",
            Width = 200,
            HorizontalAlignment = HorizontalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            Margin = Thickness.Parse("20"),
        };

        // Our styled button will be opaque at all times. It will have a
        // light gray background when the cursor is not over it. We can
        // set that here when the button is created. But to set the colors
        // for how it reacts when the cursor is over it, we'll use some
        // more code after it has been created.

        var botButton = new Button
        {
            FontSize = 36,
            Content = "Styled",
            Width = 200,
            HorizontalAlignment = HorizontalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            Margin = Thickness.Parse("20"),
            Background = Brushes.LightGray,
        };

        // Here's where we add some style changes so the bottom button
        // is opaque when the mouse cursor is over it.

        // Make the button's backgound a pure white when the cursor floats
        // over it. Note the use of the ":pointerover" pseudoclass here.

        // We also have the button's foreground color react by changing.
        // Whether or not this is a good idea is up to you. The code is
        // here to show you that you can change other properties of the
        // button if you want to.

        botButton.Styles.Add(
            new Style(x => x.OfType<Button>().Class(":pointerover").Template().Name("PART_ContentPresenter")) 
            {
                Setters = 
                {
                    new Setter(Button.BackgroundProperty, Brushes.White),
                    new Setter(Button.ForegroundProperty, Brushes.Red),
                }
            });

        // Set the button's background to a less bright gray when it is
        // pressed. Again, this makes the background opaque.

        botButton.Styles.Add(
            new Style(x => x.OfType<Button>().Class(":pressed").Template().Name("PART_ContentPresenter"))
            {
                Setters = 
                {
                    new Setter(Button.BackgroundProperty, Brushes.Gray),
                    new Setter(Button.ForegroundProperty, Brushes.Yellow),
                }
            });

        // As a treat, let's also add a tooltip to this button, which will
        // appear when you float the mouse cursor over it for a while.

        botButton.SetValue(ToolTip.TipProperty, "Look at me!\nI have style.");

        stack.Children.Add(topButton);
        stack.Children.Add(botButton);
        win.Content=stack;

        win.Show();
        app.Run(win);
    }
}