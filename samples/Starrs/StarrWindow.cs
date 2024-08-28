using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

internal class StarrWindow
{
    public Window win;
    private int numLines = 3;
    private int numAmpMods = 1;
    private int numFreqMods = 1;
    private float magAmpMods = 0;
    private float magFreqMods = 0;
    private Canvas canvas;
    public StarrWindow()
    {
        win = new Window
        {
            Title = "StarrWindow v2.0",
            Width = 1280,
            Height = 720,
        };

        var dp = new DockPanel();

        Grid grid = new Grid
        {
            ShowGridLines = true,
            Background = Brushes.Yellow,
            RowDefinitions = RowDefinitions.Parse("*, *, *, *, *"),
            ColumnDefinitions = ColumnDefinitions.Parse("*"),
            Width = 320,
        };

        grid.SetValue(DockPanel.DockProperty, Dock.Left);
        
        AddSliderSet(grid, 0, "Num Lines", 3, 500, true, v => numLines = (int)v, "0");  
        AddSliderSet(grid, 1, "Num Bumps", 1, 8, true, v => numAmpMods = (int)v, "0");  
        AddSliderSet(grid, 2, "Num Folds", 1, 8, true, v => numFreqMods = (int)v, "0");  
        AddSliderSet(grid, 3, "Max Bump", 0, 1f, false, v => magAmpMods = (float)v, "0.000");  
        AddSliderSet(grid, 4, "Max Fold", 0, 1f, false, v => magFreqMods = (float)v, "0.000");  

        dp.Children.Add(grid);

        canvas = new Canvas{ Background = Brushes.Black, };

        dp.Children.Add(canvas);

        win.Content = dp;
    }

    void AddSliderSet(
        Grid grid, 
        int row,
        string label,
        float min,
        float max,
        bool snap,
        Action<double> useValue,
        string format
        )
    {
        Label spacer = new Label { Content = " ", FontSize = 12, };
        Label caption = new Label{ Content = label, FontSize = 24, FontFamily = "Liberation Mono", };
        Label valueLabel = new Label{FontSize = 24, FontFamily = "Libeation Mono", Width = 100, HorizontalAlignment = HorizontalAlignment.Left, HorizontalContentAlignment = HorizontalAlignment.Right, };
        Slider slider = new Slider{Value = min, Minimum = min, Maximum = max, TickFrequency = 1, IsSnapToTickEnabled = snap, };

        new StarrSliderManager(slider, useValue, valueLabel, format, this);

        StackPanel panel = new StackPanel();

        panel.SetValue(Grid.RowProperty, row);

        panel.Children.Add(spacer);
        panel.Children.Add(caption);
        panel.Children.Add(valueLabel);
        panel.Children.Add(slider);

        grid.Children.Add(panel);
    }

    public void DrawStarr()
    {
        canvas.Children.Clear();

        float xCenter = (float)canvas.Bounds.Width / 2;
        float yCenter = (float)canvas.Bounds.Height / 2;

        float size = yCenter;

        if (size > xCenter)
        {
            size = xCenter;
        }

        Point[] points = new Point[numLines];

        for (int i = 0; i < numLines; ++i)
        {
            float theta = i * 2 * (float)Math.PI / numLines;

            float fMod = magFreqMods * (float)Math.Sin(theta * numFreqMods);
            float aMod = magAmpMods * (float)Math.Sin(theta * numAmpMods);

            float length = 1 - (aMod + magAmpMods) / 2;
            float angle = theta + fMod;

            float x = size * length * (float)Math.Sin(angle);
            float y = size * length * (float)Math.Cos(angle);

            points[i] = new Point(xCenter + x, yCenter - y);
        }

        canvas.Children.Add(new Polygon {Stroke = Brushes.White, StrokeThickness = 1.5, Points = points} );

        Point centerPoint = new Point(xCenter, yCenter);

        for (int i = 0; i < numLines; ++i)
        {
            canvas.Children.Add(new Line {Stroke = Brushes.White, StrokeThickness = 1.5, StartPoint = centerPoint, EndPoint = points[i]});
        }
    }
}

internal class StarrSliderManager
{
    System.Action<double> useValue;
    private Label valueLabel;
    private string format;

    private event Action DrawGraph;

    public StarrSliderManager(Slider slider, Action<double> useValue, Label valueLabel, string format, StarrWindow s)
    {
        slider.ValueChanged += ValueChanged;
        this.useValue = useValue;
        this.valueLabel = valueLabel;
        this.format = format;
        valueLabel.Content = slider.Value.ToString(format);
        DrawGraph += s.DrawStarr;
    }

    void ValueChanged(object s, RoutedEventArgs e)
    {
        Slider slider = s as Slider;
        useValue(slider.Value);
        valueLabel.Content = slider.Value.ToString(format);
        DrawGraph();
    }
}
