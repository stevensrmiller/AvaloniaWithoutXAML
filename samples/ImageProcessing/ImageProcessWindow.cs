using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;

internal class ImageProcessWindow
{
    public Window win;
    public ImageProcessWindow()
    {
        win = new Window
        {
            Title = "ImageProcessWindow v2.0",
            Height = 640,
            Width = 640,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        // We might be in a development context, or we might be deployed.
        // In a development context, our parent directory is probably either
        // "Release" or "Debug." Set the path to the image accordingly.

        string fileName = "OldFriend.png";

        string parentPath = Path.GetDirectoryName(Environment.CurrentDirectory);

        if (parentPath != null)
        {
            string parentDir = Path.GetFileName(parentPath);

            if (parentDir != null && (parentDir == "Debug" || parentDir == "Release"))
            {
                fileName = $"../../../{fileName}";
            }
        }

        // Open a bitmap image.

        var img = new Image()
        {
            Source = new Bitmap(fileName),
            Stretch = Stretch.None,
        };

        // Every time the mouse is pressed or released on our image,
        // invert the color data.

        img.PointerPressed += Invert;
        img.PointerReleased += Invert;

        // Our image will be the only content.

        win.Content = img;
        win.Show();
    }

    // This bit manipulates the pixel data. It uses actual pointers,
    // which managed languages like C# try to avoid. To use them,
    // the method is marked "unsafe," which is a C# keyword designed
    // to make you reluctant to use such things. Indeed, since a pointer
    // can access any address allowed for your process, you can really
    // hurt yourself if you misuse one (up to, at least, corrupting the
    // runtime image of your code and, conceivably, far worse).
    //
    // However, if you have done your job right, the pointer will never
    // access, much less modify, any data it should leave alone. Here,
    // the pointer, called "bmpPtr," never accesses anything outside the
    // locked bitmap data.
    //
    // To compile unsafe code, add this to a PropertyGroup in the .csproj:
    //
    //     <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    unsafe void Invert(object s, RoutedEventArgs e)
    {
        Image img = (Image)s;

        using var memoryStream = new MemoryStream();
        
        ((Bitmap)img.Source).Save(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        var writeableBitmap = WriteableBitmap.Decode(memoryStream);
        using var lockedBitmap = writeableBitmap.Lock();

        byte* bmpPtr = (byte*)lockedBitmap.Address;
        int width = writeableBitmap.PixelSize.Width;
        int height = writeableBitmap.PixelSize.Height;

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                bmpPtr[0] = (byte)~bmpPtr[0]; // red
                bmpPtr[1] = (byte)~bmpPtr[1]; // grn
                bmpPtr[2] = (byte)~bmpPtr[2]; // blu
                bmpPtr += 4;                  // alf
            }
        }

        img.Source = writeableBitmap;
    }
}