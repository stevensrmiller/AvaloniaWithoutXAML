using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.Media;
using System;
using Avalonia.Controls.Shapes;
using Avalonia;

internal class ParticlesWindow
{
    const int width = 1280;
    const int height = 720;
    public Window win;
    private Canvas canvas;
    private Particle[] particles;

    public ParticlesWindow()
    {
        win = new Window
        {
            Title = "ParticlesWindow v0.2",
            Height = height,
            Width = width,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        canvas = new Canvas
        {
            Height = height,
            Width = width,
            Background = Brushes.Black,
        };

        win.Content = canvas;
        win.Show();

        // Start a task on another thread. (Assign to discard to avoid
        // CS4014 warning.)

        Metronome metronome = new Metronome(20);

        CreateParticles();

        metronome.OnTick += Update;
    }

    private void CreateParticles()
    {
        particles = new Particle[50];

        for (int i = 0; i < particles.Length; ++i)
        {
            particles[i] = new Particle(width, height);
        }
    }
    private void Update()
    {
        canvas.Children.Clear();

        foreach (var particle in particles)
        {
            particle.Move();

            canvas.Children.Add( new Line
            {
                StrokeThickness = 3,
                Stroke = Brushes.White,
                StartPoint = new Point(particle.x, particle.y),
                EndPoint = new Point(particle.x + 3, particle.y),
            });
        }

        for (int start = 0; start < particles.Length - 1; ++start)
        {
            Particle sp = particles[start];

            for (int end = start + 1; end < particles.Length; ++end)
            {
                Particle ep = particles[end];

                // Avoid taking a square root here by comparing the square of the max distance.

                float distSq = (sp.x - ep.x) * (sp.x - ep.x) + (sp.y - ep.y) * (sp.y - ep.y);

                if (distSq < 40000)
                {
                    float opacity = 1 - distSq / 40000;

                    // Correct for non-linear opacity by squaring it.

                    opacity = opacity * opacity;

                    canvas.Children.Add( new Line
                    {
                        StrokeThickness = 1.5f,
                        Stroke = new SolidColorBrush(Colors.White, opacity),
                        StartPoint = new Point(sp.x + 1.5f, sp.y),
                        EndPoint = new Point(ep.x + 1.5f, ep.y),
                    });
                }
            }
        }
    }
}