using System;
using Avalonia.Threading;

internal class Animator
{
    private DispatcherTimer timer;
    public Animator(Action action, int milliseconds)
    {
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(milliseconds);
        timer.Tick += (s, a) => action();
    }

    public void Run()
    {
        timer.Start();
    }

    public void Stop()
    {
        timer.Stop();
    }
}