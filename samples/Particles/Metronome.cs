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

    private async void Tick()
    {
        while (true)
        {
            long ticksIn = DateTime.Now.Ticks;

            await Dispatcher.UIThread.InvokeAsync(() => DoTick());

            long ticksOut = DateTime.Now.Ticks;

            // How long did the update take?

            int millisUsed = (int)(ticksOut - ticksIn) / 10000;

            // What's that leave?

            int timeLeft = milliseconds - millisUsed;

            // Wait until the remaining time has elapsed (if any).

            // if (timeLeft < 3)
            // Console.WriteLine($"{timeLeft}, {ticksOut}, {ticksIn}");
            if (timeLeft > 0)
            {
                Thread.Sleep(timeLeft);
            }
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