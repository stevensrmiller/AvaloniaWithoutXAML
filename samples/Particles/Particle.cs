using System;
using Avalonia.Controls;
using Avalonia.Remote.Protocol.Input;
using SkiaSharp;

internal class Particle
{
    const float vMin = 0.10f;
    const float vMax = 1.00f;
    public float x;
    public float y;

    float dx;
    float dy;
    static Random rand = new Random();
    private int width;
    private int height;

    public Particle(int width, int height)
    {
        this.width = width;
        this.height = height;

        Randomize();        
    }

    public void Move()
    {
        x += dx;
        y += dy;

        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            Randomize();
        }
    }

    private void Randomize()
    {
        x = width * rand.NextSingle();
        y = height * rand.NextSingle();

        dx = vMin + (vMax - vMin) * rand.NextSingle();
        dy = vMin + (vMax - vMin) * rand.NextSingle();

        if (rand.NextSingle() > 0.5f)
        {
            dx = -dx;
        }

        if (rand.NextSingle() > 0.5f)
        {
            dy = -dy;
        }
    }
}