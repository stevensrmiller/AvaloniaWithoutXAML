using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;

public class Animate
{
    volatile static bool isRunning = false;
    public static void Run(Action doFrame, int delay, int numFrames)
    {
        if (isRunning) return;

        isRunning = true;

        _ = Task.Run(() => Frames(doFrame, delay, numFrames));
    }

    private static void Frames(Action action, int delay, int numFrames)
    {
        for (int i = 0; i < numFrames; ++i)
        {
            Dispatcher.UIThread.Post(action);
            Thread.Sleep(delay);
        }

        isRunning = false;
    }
}