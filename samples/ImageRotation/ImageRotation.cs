using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.FileProviders;

internal class ImageRotation
{
    public Window win;

    private Image img;
    MatrixTransform mt;

    // This matrix will encode our incremental rotation.
    Matrix dm = Matrix.CreateRotation(.01 * 2 * Math.PI);
    public ImageRotation()
    {
        win = new Window
        {
            Title = "ImageRotation v0.1",
            Height = 720,
            Width = 1280,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        Canvas canvas = new Canvas
        {
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Background = Brushes.SlateGray,
        };

        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

        using (var reader = embeddedProvider.GetFileInfo("OldFriend.png").CreateReadStream())
        {
            // Open a bitmap image.

            Bitmap bitmap = new Bitmap(reader);

            img = new Image()
            {
                Source = bitmap,
                Stretch = Stretch.None,
            };

            img.SetValue(Canvas.LeftProperty, (1280 - bitmap.Size.Width) / 2);
            img.SetValue(Canvas.TopProperty, (720 - bitmap.Size.Height) / 2);

            // Set origin to be the center of the image so it rotates around that.

            img.RenderTransformOrigin =
                new Avalonia.RelativePoint(
                    bitmap.Size.Width / 2,
                    bitmap.Size.Height / 2,
                    Avalonia.RelativeUnit.Absolute);

            // Initialize the render transform to identity.

            mt = new MatrixTransform();
            mt.Matrix = Matrix.Identity;
            img.RenderTransform = mt;

            canvas.Children.Add(img);
        }

        win.Content = canvas;

        win.Show();

        // We'll use a lambda here to avoid needing to create an
        // event handler wlth two complicated arguments we aren't
        // going to use.

        win.PointerPressed += (s, a) => Animate.Run(Turn, 33, 100);
    }

    // The Animate.Run method will post a call to this method
    // on a regular basis. It will add a little more rotation
    // to the render transform's matrix by matrix multiplication.
    void Turn()
    {
        mt.Matrix = mt.Matrix * dm;
    }
}