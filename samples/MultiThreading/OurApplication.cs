using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;

internal class OurApplication
{
    Window win;
    Label lblTime;
    public OurApplication(Window win)
    {
        // Initialize our window.

        this.win = win;

        win.Title = "MultiThreading";
        win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        win.Height = 360;
        win.Width = 640;
        win.Background = Brushes.Navy;

        lblTime = new Label
        {
            Content = "Unused",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Foreground = Brushes.Yellow,
            FontSize = 48,
        };

        win.Content = lblTime;
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
}