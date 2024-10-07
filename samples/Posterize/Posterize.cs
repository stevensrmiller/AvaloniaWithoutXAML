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
                // #1 Turn each individual color channel full on or full off,
                // based on whether or not that color is less than 128 in
                // the original image.
                //
                // if (wi[r, c, Red] < 128)
                // {
                //     wi[r, c, Red] = 0;
                // }
                // else
                // {
                //     wi[r, c, Red] = 255;
                // }
                //
                // if (wi[r, c, Grn] < 128)
                // {
                //     wi[r, c, Grn] = 0;
                // }
                // else
                // {
                //     wi[r, c, Grn] = 255;
                // }
                //
                // if (wi[r, c, Blu] < 128)
                // {
                //     wi[r, c, Blu] = 0;
                // }
                // else
                // {
                //     wi[r, c, Blu] = 255;
                // }


                // // #2 Three levels for each color. This actually uses code to divide
                // // output into as many levels as you choose. You set the number of
                // // levels with the numLevels variable. The code does the rest.
                // //
                // // First, pick the number of levels you wants.
                // //
                // int numLevels = 3;
                // //
                // // The highest possible value a color can have is 255. As the lowest
                // // is zero, the size of the output range is 256. Dividing that by the
                // // number of levels gives us the size of each range of input values
                // // that will be converted to the same output level.
                // //
                // float levelRangeSize = 256f / numLevels;
                // //
                // // The lowest output level is zero, no matter how many output levels
                // // we have chosen. We need to know the step from one level to the
                // // next. That will be the maximum output level, which is always 255,
                // // divided by the number of steps up from 0 it will take to reach
                // // that output level. That number is one less than the number of
                // // levels, because the first output level is zero. So, if we have
                // // three output levels, they will be 0, 127.5, and 255. Three output
                // // levels starting at 0 will need two steps of 127.5 to reach 255.
                // // Of course, our actual output level will be rounded to the nearest
                // // whole number, but we'll do that last.
                // //
                // float outputStepSize = 255f / (numLevels - 1);
                // //
                // // To know which output level we want for a given input level, we
                // // divide the input level by the size of the range of the input
                // // values that will be converted to the same output level. That
                // // will give us a number somewhere on the between L and L.99... where
                // // L is the whole number of the input range to convert to a single
                // // output value. We only want L, so we cast that number to an integer.
                // //
                // int rLevel = (int)(wi[r, c, Red ] / levelRangeSize);
                // //
                // // Finally, we multiply that whole level number by the output step
                // // size and round that value to the nearest byte, to get the actual
                // // shade for that level.
                // //
                // wi[r, c, Red] = (byte)(0.5f + outputStepSize * rLevel);
                // //
                // // And do this again for green.
                // //
                // int gLevel = (int)(wi[r, c, Grn ] / levelRangeSize);
                // wi[r, c, Grn] = (byte)(0.5f + outputStepSize * gLevel);
                // //
                // // And do this again for blue.
                // //
                // int bLevel = (int)(wi[r, c, Blu ] / levelRangeSize);
                // wi[r, c, Blu] = (byte)(0.5f + outputStepSize * bLevel);


                // #3 Average the shades into one level of gray.

                // float average = (wi[r, c, Red] + wi[r, c, Grn] + wi[r, c, Blu]) / 3f;
                // byte grayShade = (byte)(average + 0.5f);
                
                // wi[r, c, Red] = grayShade;
                // wi[r, c, Grn] = grayShade;
                // wi[r, c, Blu] = grayShade;
                

                // #4 Use the luminosity weight average formula to get a gray shade.

                // float average = 0.3f * wi[r, c, Red] + 0.59f * wi[r, c, Grn] + 0.11f * wi[r, c, Blu];
                // byte grayShade = (byte)(average + 0.5f);
                
                // wi[r, c, Red] = grayShade;
                // wi[r, c, Grn] = grayShade;
                // wi[r, c, Blu] = grayShade;


                // #5 Make a negative image of the output from #4. Note that we
                // have to cast "255 - grayShade" to be a byte, because the narrowest
                // type C# uses to do any math that only uses whole numbers is type
                // int.

                // float average = 0.3f * wi[r, c, Red] + 0.59f * wi[r, c, Grn] + 0.11f * wi[r, c, Blu];
                // byte grayShade = (byte)(average + 0.5f);
                
                // wi[r, c, Red] = (byte)(255 - grayShade);
                // wi[r, c, Grn] = (byte)(255 - grayShade);
                // wi[r, c, Blu] = (byte)(255 - grayShade);


                // #6 Make a color negative.

                // wi[r, c, Red] = (byte)(255 - wi[r, c, Red]);
                // wi[r, c, Grn] = (byte)(255 - wi[r, c, Grn]);
                // wi[r, c, Blu] = (byte)(255 - wi[r, c, Blu]);


                // #7 Make just one color negative, leave the other two alone.

                // wi[r, c, Red] = (byte)(255 - wi[r, c, Red]);
                

                // #8 Copy red into blue, blue into green, and green into red.

                // byte saveBlueForLater = wi[r, c, Blu];
                // wi[r, c, Blu] = wi[r, c, Red];
                // wi[r, c, Red] = wi[r, c, Grn];
                // wi[r, c, Grn] = saveBlueForLater;


                // #9 Set the pixel full white or full black, depending on which
                // of several brightness levels it is at, alternating between
                // black and white for adjacent levels.

                // int numLevels = 4;

                // float levelRangeSize = 256f / numLevels;

                // float average = (wi[r, c, Red] + wi[r, c, Grn] + wi[r, c, Blu]) / 3f;

                // int level = (int)(average / levelRangeSize);

                // int remainder = level - 2 * (level / 2);

                // if (remainder == 1)
                // {
                //     wi[r, c, Red] = 255;
                //     wi[r, c, Grn] = 255;
                //     wi[r, c, Blu] = 255;
                // }
                // else
                // {
                //     wi[r, c, Red] = 0;
                //     wi[r, c, Grn] = 0;
                //     wi[r, c, Blu] = 0;
                // }


                // #10 Flip the image horizonally.
                //
                // Note that this one uses a third loop to copy each color
                // byte. This works because, the color constants, Blu, Grn,
                // and Grn, are just the integers, 0, 1, and 2.
                //
                // NOTE: For this one, be SURE to change the column loop
                // to this line of code:
                //
                //             for (int c = 0; c < wi.width / 2; ++c)
                //
                // That's because you only want to work your away across half
                // of the image. If you do the whole image, you will copy each
                // pixel twice, and the picture will end up the same as it was
                // before you copied anything.
                //
                // for (int p = 0; p < 3; ++p)
                // {
                //     byte buff = wi[r, c, p];
                //     wi[r, c, p] = wi[r, (wi.width - 1) - c, p];
                //     wi[r, (wi.width - 1) - c, p] = buff;
                // }


                // #11 A BONUS! This one uses some math functions to turn the brightness
                // at each pixel into three different shades of red, green, and blue.

                // float average = 0.3f * wi[r, c, Red] + 0.59f * wi[r, c, Grn] + 0.11f * wi[r, c, Blu];
                // byte grayShade = (byte)(average + 0.5f);

                // wi[r, c, Red] = (byte)(0.5f + grayShade * grayShade / 255f);
                // wi[r, c, Grn] = (byte)(0.5f + 65f + 190f * ((grayShade - 127.5f) * (grayShade - 127.5f) / (127.5f * 127.5f)));
                // wi[r, c, Blu] = (byte)(0.5f + (255 - grayShade) * (255 - grayShade) / 255f);
            }
        }

        wi.Update();
    }
}