using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

internal class BlendBands
{
    const int Red = 2;
    const int Grn = 1;
    const int Blu = 0;

    public Window win;

    WriteableImage rose;
    WriteableImage cat;
    public BlendBands()
    {
        rose = new WriteableImage("Rose.png");
        cat = new WriteableImage("Cat.png");

        win = new Window
        {
            Title = "BlendBands v1.0",
            Height = rose.height,
            Width = rose.width,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        win.Content = rose.img;
        win.PointerPressed += Process;
        win.Show();
    }


    void Process(object s, RoutedEventArgs e)
    {
        for (int r = 0; r < rose.height; ++r)
        {
            // Let's pick a weight for the rose, based on which fifth
            // of the picture the current row is in. At first, we will
            // set it to 5/6ths, which is the weight we'll use in the top
            // part of the picture.

            float roseWeight = 5f / 6f;
            
            // Next, we divide the current row number by the highest
            // row number (which is the height of the picture, minus
            // one). This will give us a float type of number, somewhere
            // from zero to one.

            float part = (float)r / (rose.height - 1);

            // If we're in the top fifth, our number will be from zero
            // up to 0.2, and we'll use the maximume weight for the rose.
            // We've already set roseWeight for the top, so now we have to
            // see if we're in one of the other four fifths of the picture.

            // Are we in the second fifth?

            if (part >= 0.2f && part < 0.4f)
            {
                roseWeight = 4f / 6f;
            }

            // Are we in the third fifth?

            if (part >= 0.4f && part < 0.6f)
            {
                roseWeight = 3f / 6f;
            }

            // Are we in the fourth fifth?

            if (part >= 0.6f && part < 0.8f)
            {
                roseWeight = 2f / 6f;
            }

            // Are we in the fifth fifth?

            if (part >= 0.8f)
            {
                roseWeight = 1f / 6f;
            }

            // The two weights have to add up to one, so we can use
            // the rose weight to get the cat weight.

            float catWeight = 1 - roseWeight;


            for (int c = 0; c < rose.width; ++c)
            {
                // Multiply the rose's red shade at this pixel by
                // the rose's weight for the part of the picture
                // we're in, to get the contribution of red from
                // the rose picture that will be made to the
                // final image at this pixel.

                float roseRedContribution = rose[r, c, Red] * roseWeight;

                // Do the same thing for the green and blue shades
                // for this pixel of the rose.

                float roseGrnContribution = rose[r, c, Grn] * roseWeight;
                float roseBluContribution = rose[r, c, Blu] * roseWeight;

                // Now do the same thing for the cat pixel, using
                // the cat's weight.

                float catRedContribution = cat[r, c, Red] * catWeight;
                float catGrnContribution = cat[r, c, Grn] * catWeight;
                float catBluContribution = cat[r, c, Blu] * catWeight;

                // Added the weighted shades together.

                float totalRed = roseRedContribution + catRedContribution;
                float totalGrn = roseGrnContribution + catGrnContribution;
                float totalBlu = roseBluContribution + catBluContribution;

                // Round each total to the nearest whole number, cast that
                // to a byte, and use it to set the new red, green, and
                // blue shades for this pixel. Note that we are putting the
                // totals into the pixels of the rose picture, because the
                // rose picture is already on our screen. After we put all
                // the new pixels values into the rose picture and update
                // it, our new pixels will be visible because we have used
                // them to change the pixels in the rose picture.

                rose[r, c, Red] = (byte)(0.5f + totalRed);
                rose[r, c, Grn] = (byte)(0.5f + totalGrn);
                rose[r, c, Blu] = (byte)(0.5f + totalBlu);
            }
        }

        rose.Update();
    }

    void ProcessRound(object s, RoutedEventArgs e)
    {
        const float fadeWidth = 100;
        const float blendRadius = 220;

        for (int r = 0; r < rose.height; ++r)
        {
            for (int c = 0; c < rose.width; ++c)
            {
                float rGap = r - rose.height / 2f;
                float cGap = c - rose.width / 2f;

                float pixelGap = (float)Math.Sqrt(rGap * rGap + cGap * cGap);

                float roseWeight = 1;

                if (pixelGap < blendRadius - fadeWidth)
                {
                    roseWeight = 0;
                }
                else if (pixelGap < blendRadius)
                {
                    roseWeight = 1 - (blendRadius - pixelGap) / fadeWidth;
                }

                float catWeight = 1 - roseWeight;

                for (int p = 0; p < 3; ++p)
                {
                    rose[r, c, p] = (byte)(0.5f + roseWeight * rose[r, c, p] + catWeight * cat[r, c, p]);
                }
            }
        }

        rose.Update();
    }
}