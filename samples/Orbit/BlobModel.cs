using System;
using Avalonia.Media;
using Avalonia.Media.Immutable;

internal class BlobModel
{
    public float size = 100;
    public float orbitalRadius = .5f;
    public float theta = 0;
    public float deltaTheta = .1f;
    public IImmutableSolidColorBrush brush = Brushes.Magenta;

    public float X { get => orbitalRadius * (float)Math.Cos(theta); }
    public float Y { get => orbitalRadius * (float)Math.Sin(theta); }

    public event Action PositionChanged;
    public void IncrementPosition()
    {
        theta = theta + deltaTheta;

        while (theta > 2 * Math.PI)
        {
            theta = theta - 2 * (float)Math.PI;
        }

        if (PositionChanged != null)
        {
            PositionChanged();
        }
    }
}