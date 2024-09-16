
using Avalonia.Media;

public static class RealColor
{
    public static Color Color(float r, float g, float b)
    {
        return new Color(255, (byte)(255 * r + .5f), (byte)(255 * g + .5f), (byte)(255 * b + .5f));
    }
}