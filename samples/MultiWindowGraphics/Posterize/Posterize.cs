using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.FileProviders;

internal class Posterize
{
    const int Red = 2;
    const int Grn = 1;
    const int Blu = 0;

    public Window win;

    WriteableImage wi;
    public Posterize()
    {
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

        Image img;

        using (var reader = embeddedProvider.GetFileInfo("OldFriend.png").CreateReadStream())
        {

            img = new Image()
            {
                Source = new Bitmap(reader),
                Stretch = Stretch.None,
            };
            img.PointerPressed += Process;
        }

        win = new Window
        {
            Title = "Posterize v4.0",
            Height = img.Source.Size.Height,
            Width = img.Source.Size.Width,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        win.Content = img;
        win.Show();

        wi = new WriteableImage(img);
    }

    // You can use the WriteableImage object to see the values in the pixels
    // of your picture, and to change them. You can also read the height and
    // the width of the picture.
    //
    // wi.height and wi.width give you the dimensions.
    //
    // WriteableImage also has a three dimensional array that lets you access
    // the colors in each pixel. Use this format:
    //
    //     wi[row, column color]
    //
    // row and column pick the pixel you want operate on.
    // color is one of 0, 1, or 2, to access the blue, green, or red byte
    // at the pixel. Remember, you are working with bytes here, which are
    // whole numbers starting at zero and going to (and including) 255.

    void Process(object s, RoutedEventArgs e)
    {
        for (int r = 0; r < wi.height; ++r)
        {
            for (int c = 0; c < wi.width; ++c)
            {
                int sum = wi[r, c, Red] + wi[r, c, Grn] + wi[r, c, Blu];

                if (sum < 192)
                {
                    wi[r, c, Red] = 0;
                    wi[r, c, Grn] = 0;
                    wi[r, c, Blu] = 0;
                }
                else if (sum < 383)
                {
                    wi[r, c, Red] = 255;
                    wi[r, c, Grn] = 128;
                    wi[r, c, Blu] = 0;
                }
                else if (sum < 575)
                {
                    wi[r, c, Red] = 0;
                    wi[r, c, Grn] = 255;
                    wi[r, c, Blu] = 128;
                }
                else
                {
                    wi[r, c, Red] = 128;
                    wi[r, c, Grn] = 0;
                    wi[r, c, Blu] = 255;
                }

                // int levels = 2;
                // float levelDiv = 255f / levels;
                // float scale = 255f / (levels - 1);

                // int rLevel = (int)(wi[r, c, Red ] / levelDiv);
                // wi[r, c, Red] = (byte)(0.5f + scale * rLevel);

                // int gLevel = (int)(wi[r, c, Grn ] / levelDiv);
                // wi[r, c, Grn] = (byte)(0.5f + scale * gLevel);

                // int bLevel = (int)(wi[r, c, Blu ] / levelDiv);
                // wi[r, c, Blu] = (byte)(0.5f + scale * bLevel);
            }
        }

        wi.Update();
    }
}