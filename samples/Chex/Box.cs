using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

public class Box
{
    public SolidColorBrush FillColor
    {
        set
        {
            Polygon.Fill = value;
        }
    }
    public Vector Center
    {
        set
        {
            center = value;
            LoadPolygon();
        }
    }
    public float Rotation
    {
        set
        {
            rotation = value;
            LoadPolygon();
        }
    }
    public Polygon Polygon;
    private float halfWidth;
    private float halfHeight;
    private Vector center;
    private float rotation;

    private Point[] initPoints;
    public Box(float width, float height, Vector? center = null, Color? fillColor = null, float rotation = 0)
    {
        halfWidth = width / 2;
        halfHeight = height / 2;

        this.rotation = rotation;

        initPoints =
        [
            new Point(-halfWidth, -halfHeight),
            new Point( halfWidth, -halfHeight),
            new Point( halfWidth,  halfHeight),
            new Point(-halfWidth,  halfHeight),
        ];

        if (center is null)
        {
            this.center = Vector.Zero;
        }
        else
        {
            this.center = (Vector)center;
        }

        this.rotation = rotation;

        Polygon = new Polygon
        {
            StrokeThickness = 1.5,
            Stroke = Brushes.White,
            Fill = fillColor == null ? Brushes.Red : new SolidColorBrush((Color)fillColor),
        };

        LoadPolygon();
    }

    public void Rotate(float rotation)
    {
        this.rotation += rotation;

        LoadPolygon();
    }

    public void Move(Vector movement)
    {
        center += movement;

        LoadPolygon();
    }

    void LoadPolygon()
    {
        Matrix rotate = Matrix.CreateRotation(rotation);
        
        Point[] polyPoints =
        [
            initPoints[0].Transform(rotate) + center,
            initPoints[1].Transform(rotate) + center,
            initPoints[2].Transform(rotate) + center,
            initPoints[3].Transform(rotate) + center,
        ];

        Polygon.Points = polyPoints;
    }
}