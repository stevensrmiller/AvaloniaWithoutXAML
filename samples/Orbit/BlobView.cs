using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

internal class BlobView
{
    private BlobModel blobModel;
    private Canvas canvas;
    private Ellipse ellipse;
    public BlobView(Canvas canvas, BlobModel blobModel)
    {
        this.canvas = canvas;
        this.blobModel = blobModel;
        ellipse = new Ellipse
        {
            Height = blobModel.size,
            Width = blobModel.size,
            Fill = blobModel.brush,
        };

        SetBlobPosition();

        canvas.Children.Add(ellipse);

        blobModel.PositionChanged += SetBlobPosition;
    }

    public void SetBlobPosition()
    {
        float cx = (float)canvas.Width / 2;
        float cy = (float)canvas.Height / 2;

        float s = cx < cy ? cx : cy;

        float x = cx + s * blobModel.X - blobModel.size / 2;
        float y = cy + s * blobModel.Y - blobModel.size / 2;

        ellipse.SetValue(Canvas.LeftProperty, x);
        ellipse.SetValue(Canvas.BottomProperty, y);
    }
}