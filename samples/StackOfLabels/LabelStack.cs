using Avalonia;
using Avalonia.Controls;

// We need this namespace to use the Brushes class, which contains
// convenient color definitions.

using Avalonia.Media;

internal class LabelStack
{
    // We'll use the factory pattern to create this window.

    public static Window Create()
    {

        // Note that it is simple and intuitive to set the dimensions.

        var win = new Window
        {
            Title = "StackOfLabels",
            Height = 640,
            Width = 360,
        };

        // The only content for this window will be a StackPanel. This
        // control can contain any number of other controls. All of its
        // contents will appear in a vertical layout, from top to bottom,
        // in the order in which those contents were added.

        var stack = new StackPanel();

        // Create three labels to add to the StackPanel. Note the use of
        // FontSize and color settings.

        var labelTop = new Label
        {
            Content = "Top Label",
            FontSize = 24,
            Foreground = Brushes.White,
            Background = Brushes.Red,
        };

        var labelMid = new Label
        {
            Content = "Mid Label",
            FontSize = 24,
            Foreground = Brushes.Green,
            Background = Brushes.Yellow,
        };

        var labelBot = new Label
        {
            Content = "Bot Label",
            FontSize = 24,
            Foreground = Brushes.Black,
            Background = Brushes.Cyan,
        };

        // Add the labels to the StackPanel. They will appear, top down, in
        // the order they were added.

	stack.Children.Add(labelTop);
	stack.Children.Add(labelMid);
	stack.Children.Add(labelBot);

        // Make the StackPanel the window's content. Note that it doesn't
        // matter if you add content to the stack or the window first.

        win.Content = stack;
        win.Show();

        return win;
    }
}