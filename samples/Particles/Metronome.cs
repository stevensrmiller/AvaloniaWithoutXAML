using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;

internal class Metronome
{
    private int milliseconds;

    public event Action OnTick;

    public Metronome(int milliseconds)
    {
        this.milliseconds = milliseconds;

        _ = Task.Run(Tick);
    }

    private void Tick()
    {
        while (true)
        {
            Dispatcher.UIThread.Post(() => DoTick());
            Thread.Sleep(milliseconds);
        }
    }

    private void DoTick()
    {
        if (OnTick != null)
        {
            OnTick();
        }
    }
}