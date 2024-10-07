using System;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.FileProviders;

internal class Expand
{
    const int Red = 2;
    const int Grn = 1;
    const int Blu = 0;

    public Window win;

    WriteableImage oldFriend;
    WriteableImage newFriend;
    public Expand()
    {
        oldFriend = new WriteableImage("OldFriend.png");
        newFriend = new WriteableImage("OldFriend.png");

        win = new Window
        {
            Title = "Blend v1.0",
            Height = oldFriend.height,
            Width = oldFriend.width,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        win.Content = newFriend.img;
        win.PointerPressed += Process;
        win.Show();
    }


    void Process(object s, RoutedEventArgs e)
    {
        for (int r = 0; r < newFriend.height; ++r)
        {
            // Step 1, use shrinkFactor = 1:

            // float shrinkFactor = 1;


            // Step 2, try shrinkFactor = 0.5f:

            // float shrinkFactor = 0.5f;


            // Step 3, make shrinkFactor change for each row of pixels:
            
            // float shrinkFactor = 1 - (float)r / newFriend.height;

            // float shrinkFactor = 1 - (float)Math.Abs(r - newFriend.height / 2) / newFriend.height;

            // Your first challenge: make shrinkFactor start at 0.5
            // at the top, rise to 1 in the middle row, then back down
            // at the bottom to 0.5.

            // Your second challenge: look at the example images and
            // figure out what you think shrinkFactor is at the top,
            // middle, and bottom row, and add the code to compute those
            // values, changing it a bit at each row, so your final
            // result matches the examples.

            float shrinkFactor = 1 - 2 * (float)Math.Abs(r - newFriend.height / 2) / newFriend.height;

            for (int c = 0; c < newFriend.width; ++c)
            {
                // What is the current column's distanct, positive or negative,
                // from the center of the input image?

                float distance = c - oldFriend.width / 2f;

                // m is some number from one down to zero. Use that to shrink
                // the distance.

                float shrunkenDistance = shrinkFactor * distance;

                // Add the shrunken distance to the center coordinate to get the
                // coordinate of the pixel we want to copy (rounding, of course).

                int cCopy = (int)(0.5f + oldFriend.width / 2 + shrunkenDistance);
            
                for (int p = 0; p < 3; ++p)
                {
                    newFriend[r, c, p] = oldFriend[r, cCopy, p];
                }
            }
        }

        newFriend.Update();
    }
}