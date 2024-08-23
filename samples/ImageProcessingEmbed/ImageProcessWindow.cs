using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Embedding.Offscreen;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.FileProviders;

internal class ImageProcessWindow
{
    public Window win;
    public ImageProcessWindow()
    {
        win = new Window
        {
            Title = "ImageProcessWindow v4.0",
            Height = 640,
            Width = 640,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        // To avoid any complications arising from where the executable is
        // when we run it (and, therefore, any issues in finding out where
        // the image file is relative to that location), our .csproj file
        // embeds the image as a resource. We'll get it now and use it to
        // create a stream, which will be passed as an argument to Avalonia's
        // Bitmap constructor. In a production context, you might want to
        // use a filepicker dialog and pass a string to the Bitmap constructor
        // overload that will open a file instead of read a stream.

        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

        using (var reader = embeddedProvider.GetFileInfo("OldFriend.png").CreateReadStream())
        {
            // Open a bitmap image.

            var img = new Image()
            {
                Source = new Bitmap(reader),
                Stretch = Stretch.None,
            };

            // Every time the mouse is pressed or released on our image,
            // invert the color data.

            img.PointerPressed += Invert;
            img.PointerReleased += Invert;

            // Our image will be the only content.

            win.Content = img;
        }

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
                bmpPtr[0] = (byte)~bmpPtr[0]; // blu
                bmpPtr[1] = (byte)~bmpPtr[1]; // grn
                bmpPtr[2] = (byte)~bmpPtr[2]; // red
                bmpPtr += 4;                  // alf
            }
        }

        img.Source = writeableBitmap;
    }
}