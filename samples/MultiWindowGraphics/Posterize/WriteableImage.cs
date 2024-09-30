using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Converters;
using Avalonia.Media.Imaging;
public class WriteableImage
{
    public int height;
    public int width;
    Image img;
    WriteableBitmap wb;
    byte[] buffer;
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
        //img.Source = wb;
        byte* bmpPtr = (byte*)lockedBitmap.Address;

        for (int i = 0; i < buffer.Length; ++i)
        {
            bmpPtr[i] = buffer[i];
        }

        img.InvalidateVisual();
    }
}

