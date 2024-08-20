// Pull a Windows icon out of the system based on what is associated
// with an executable. Note that this is platform-specific application.
// You'll see lots of compiler warnings becaues of that.

using System.Drawing;
using System.Drawing.Imaging;
using Avalonia;
using Avalonia.Controls;
using aBrushes = Avalonia.Media.Brushes;
using aBitmap = Avalonia.Media.Imaging.Bitmap;

class Project
{
    public static void Main(string[] args)
    {
        AppBuilder.Configure<Application>()
                  .UsePlatformDetect()
                  .Start(AppMain, args);
    }

    public static void AppMain(Application app, string[] args)
    {
        app.Styles.Add(new Avalonia.Themes.Fluent.FluentTheme());
        app.RequestedThemeVariant = Avalonia.Styling.ThemeVariant.Light;

        var sp = new StackPanel
        {
            Background = aBrushes.Black,
        };

        // Get Windows to give you an Icon object.

        Icon icon =
            Icon.ExtractAssociatedIcon(@"C:\WINDOWS\system32\notepad.exe");

        // Now make Windwos convert your icon to a GDI+ bitmap. Note that
        // this Bitmap class is System.Drawing.Bitmap. We'll need to use
        // Avalonia.Media.Imaging.Bitmap later, so don't get confused.

        Bitmap bmp = icon.ToBitmap();

        Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

        // Now we want access to the actual bitmap data, so we can
        // copy the pixels. We ask Windows to lock the bitmap into
        // place in memory, and return an object that tells us how
        // to get at it.

        BitmapData  bitmapdata = bmp.LockBits
        (
            rect,
            ImageLockMode.ReadWrite,
            bmp.PixelFormat
        );

        // Finally, we will use Avalonia.Media.Imaging.Bitmap and
        // copy the pixels from our GDI+ bitmap.

        int height = bitmapdata.Height;
        int width = bitmapdata.Width;

        aBitmap avaloniaBitmap = new aBitmap
        (
            Avalonia.Platform.PixelFormat.Bgra8888,
            Avalonia.Platform.AlphaFormat.Unpremul,
            bitmapdata.Scan0, // IntPtr where the pixels start.
            new PixelSize(width, height),
            new Vector(96, 96), // dpi of the bitmap
            bitmapdata.Stride // bytes per row (yes, might be > width)
        );
        
        // We've copied the bitmap now, so we can unlock it and
        // let Windows get rid of it. Note that we have to ask
        // Windows to dispose of it or else it will take up system
        // resources the .NET garbage collector doesn't know about.

        bmp.UnlockBits(bitmapdata);
        bmp.Dispose();

        // Now we're just working with Avalonia again.

        var image = new Avalonia.Controls.Image
        {
            Source = avaloniaBitmap,
            Height = height,
            Width = width,
        };

        // Using a green background here to make it easy to see the
        // (somewhat surprising) fact that the Windows notebook icon
        // has a partially transparent blue cover.
        
        var win = new Window
        {
            Title = "An Icon",
            Content = image,
            Height = 180,
            Width = 320,
            Background = aBrushes.Green,
        };

        win.Show();
        app.Run(win);
    }
}