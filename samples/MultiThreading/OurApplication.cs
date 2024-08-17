using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;

internal class OurApplication
{
    Window win;
    Label lblTime;
    Button button;
    int count;
    public OurApplication(Window win)
    {
        // Initialize our window.

        this.win = win;

        win.Title = "MultiThreading";
        win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        win.Height = 360;
        win.Width = 640;

        lblTime = new Label
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 48,
        };

        button = new Button
        {
            Content = "0",
            FontSize = 48,
            Height = 90,
            Width = 160,
            HorizontalAlignment = HorizontalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
        };

        // The event handler for button clicks will run on the
        // UI thread, as they normally do.

        button.Click += (s, e) => Increment();

        var stack = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Center,
        };

        stack.Children.Add(lblTime);
        stack.Children.Add(button);

        win.Content = stack;
        win.Show();

        // Start a task on another thread.

        Task.Run(() => Counter());
    }

    private void SetTime(string timeText)
    {
        lblTime.Content = timeText;
    }

    private void Shutdown()
    {
        win.Close();
    }

    private void Counter()
    {
        // Four times per second, pass a string with the current time
        // to our SetTime method, but do this by running a method on
        // UI thread.

        for (int i = 0; i < 40; ++i)
        {
            DateTime now = DateTime.Now;

            Dispatcher.UIThread.Post(() => SetTime($"{now.Hour:00}:{now.Minute:00}:{now.Second:00}"));
            Thread.Sleep(250);
        }

        // Tell the window to close but, again, run the code that does
        // so on the UI thread.

        Dispatcher.UIThread.Post(() => Shutdown());
    }

    // This will change the button's label every time the button
    // is clicked, while the clock display will continue to update
    // in response to jobs queued by the worker thread. This will
    // show how user interaction can proceed concurrently with
    // work on another thread, and both can affect the UI.

    private void Increment()
    {
        count = count + 1;

        button.Content = $"{count}";
    }
}