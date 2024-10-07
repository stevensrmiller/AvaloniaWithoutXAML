using System;
using System.IO;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Microsoft.Extensions.FileProviders;
public class WriteableImage
{
    public int height;
    public int width;
    public Image img;
    WriteableBitmap wb;
    byte[] buffer;
    
    public WriteableImage(int width, int height)
    {
        this.height = height;
        this.width = width;

        wb = new WriteableBitmap(
            new PixelSize(width, height),
            new Vector(96, 96),
            PixelFormat.Bgra8888,
            AlphaFormat.Unpremul);

        img = new Image()
        {
            Source = wb,
            Stretch = Stretch.None,
        };

        LoadBuffer(img);
    }
    public WriteableImage(string name)
    {
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

        Image img;

        using (var reader = embeddedProvider.GetFileInfo(name).CreateReadStream())
        {

            img = new Image()
            {
                Source = new Bitmap(reader),
                Stretch = Stretch.None,
            };

            LoadBuffer(img);
        }
    }
    public WriteableImage(Image img)
    {
        LoadBuffer(img);
    }

	public byte this[int r, int c, int p]
	{
		get
		{
			return buffer[4 * (r * width + c) + p];
		}

        set
        {
            buffer[4 * (r * width + c) + p] = (byte)value;
        }
	}
    unsafe void LoadBuffer(Image img)
    {
        this.img = img;
        using var memoryStream = new MemoryStream();
        
        ((Bitmap)img.Source).Save(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);
        wb = WriteableBitmap.Decode(memoryStream);

        img.Source = wb;

        using var lockedBitmap = wb.Lock();

        byte* bmpPtr = (byte*)lockedBitmap.Address;
        width = wb.PixelSize.Width;
        height = wb.PixelSize.Height;        

        buffer = new byte[(int)(height * width * 4)];

        for (int i = 0; i < buffer.Length; ++i)
        {
            buffer[i] = *bmpPtr++;
        }
        
        img.InvalidateVisual();
    }

    unsafe public void Update()
    {
        using var lockedBitmap = wb.Lock();

        byte* bmpPtr = (byte*)lockedBitmap.Address;

        for (int i = 0; i < buffer.Length; ++i)
        {
            bmpPtr[i] = buffer[i];
        }

        img.InvalidateVisual();
    }
}

