using Avalonia.Controls;

internal class MultiWindow
{
    // We'll build a simple UI with two labels to show the
    // counts and a button to open a new window.

    Label openNow = new Label {FontSize = 36,};
    Label openEver = new Label {FontSize = 36,};
    Button button = new Button {FontSize = 36, Content = "Open", };
    Wrangler wrangler;
    public MultiWindow(Wrangler wrangler)
    {
        // Register with the Wrangler so we get notified of
        // any changes. But DON'T tell the wranger we have
        // been opened ourselves. Let the code that created us
        // do that AFTER we have been created, so any instruction
        // reordering doesn't allow for the event to try to call
        // our handler before the UI has been fully constructed.

        wrangler.CountChanged += UpdateCounts;

        // Call the method to open new window when the button is
        // clicked.

        button.Click += (s, e) => Open();

        var win = new Window
        {
            Height = 240,
            Width = 320,
        };

        var stack = new StackPanel();

        stack.Children.Add(openNow);
        stack.Children.Add(openEver);
        stack.Children.Add(button);

        // When the window closes, make sure it notifies us so
        // we can, in turn, inform the Wrangler.

        win.Closing += (s, e) => OnClose();
        win.Content = stack;

        this.wrangler = wrangler;

        win.Show();
    }

    // The Wragler will call this for each window when the
    // counts change, so they can all update their own display.
    void UpdateCounts(int ever, int now)
    {
        openNow.Content = $"Total opened: {ever}";
        openEver.Content = $"Open now: {now}";
    }

    // Avalonia will call this when our window closes. We will
    // update the Wrangler so it knows.
    void OnClose()
    {
        wrangler.Closed();
    }

    // Whatever code created us should, after the constructor
    // returns, call this to update the Wrangler.
    void Open()
    {
        new MultiWindow(wrangler);
        wrangler.Opened();
    }
}