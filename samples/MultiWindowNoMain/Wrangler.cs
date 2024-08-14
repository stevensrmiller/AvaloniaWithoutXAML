// We'll use one Wrangler object to track how many windows
// have been opened, and how many are open now. This is not
// a true singleton, as it does not prevent creation of 
// multiple objects of its class. But we'll only use one.

using System;
using System.Threading;
using Avalonia;

internal class Wrangler
{
    // Avalonia will watch this object's token to know
    // when the application should be terminated.

    public CancellationTokenSource Cts => cts;
    
    // Use this event to notify all windows when any
    // of them opens a new one or any of them closes.

    public event Action <int, int> CountChanged;
    CancellationTokenSource cts;
    int openNow;
    int openEver;

    public Wrangler()
    {
        // Create the one token source we'll need,
        // and save it.

        cts = new CancellationTokenSource();
    }

    // After new window opens, call this so the counts
    // all go up and every open window is notified. Do
    // this OUTSIDE your constructor, to be sure reordering
    // doesn't attempt to call an event handler in a new
    // window that has not been fully constructed.

    public void Opened()
    {
        openEver += 1;
        openNow += 1;

        // If every open window has registered with the
        // CountChanged event, it can't be null now. But
        // ?.Invoke is always good practice.

        CountChanged?.Invoke(openEver, openNow);
    }

    // When a window closes, it will call this to adjust
    // the counts and notify all windows. If the count goes
    // to zero, we signal the cancellation token so Avalonia
    // will terminate the application.
    
    public void Closed()
    {
        openNow -= 1;

        if (openNow == 0)
        {
            cts.Cancel();
        }
        else
        {
            CountChanged?.Invoke(openEver, openNow);
        }
    }
}