using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

internal class HarmonographWindow
{
    public Window win;
    public int stepsPerLoop = 3;
    private int numLoops = 1;
    private int ratioMajor = 1;
    private float ratioMinor = 0;
    private float shrinkRate = 0;
    private Canvas canvas;
    public HarmonographWindow()
    {
        win = new Window
        {
            Title = "HarmonographWindow v2.0",
            Width = 1280,
            Height = 720,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        FileCommand fc = new FileCommand(win);

        var dp = new DockPanel();

        var menu = new Menu
        {
            Background = Brushes.LightGray,
            Foreground = Brushes.Black,
            FontSize = 16,
            Height = 24,

            ItemsSource = new[]
            {
                new MenuItem
                {
                    Header = "File",
                    Command = fc,
                },
            },
        };

        menu.SetValue(DockPanel.DockProperty, Dock.Top);

        dp.Children.Add(menu);

        Grid grid = new Grid
        {
            ShowGridLines = true,
            Background = Brushes.Yellow,
            RowDefinitions = RowDefinitions.Parse("*, *, *, *, *"),
            ColumnDefinitions = ColumnDefinitions.Parse("*"),
            Width = 320,
        };

        grid.SetValue(DockPanel.DockProperty, Dock.Left);
        
        AddSliderSet(grid, 0, "Step/loop", 3, 180, true, v => stepsPerLoop = (int)v, "0");  
        AddSliderSet(grid, 1, "Num Loops", 1, 100, true, v => numLoops = (int)v, "0");  
        AddSliderSet(grid, 2, "Ratio Major", 1, 5, true, v => ratioMajor = (int)v, "0");  
        AddSliderSet(grid, 3, "Ratio Minor", 0, .01f, false, v => ratioMinor = (float)v, "0.000");  
        AddSliderSet(grid, 4, "Shrink Rate", 0, .01f, false, v => shrinkRate = (float)v, "0.000");  

        dp.Children.Add(grid);

        canvas = new Canvas{ Background = Brushes.DarkBlue, };

        dp.Children.Add(canvas);

        win.Content = dp;
        win.Show();
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

        new SliderManager(slider, useValue, valueLabel, format, this);

        StackPanel panel = new StackPanel();

        panel.SetValue(Grid.RowProperty, row);

        panel.Children.Add(spacer);
        panel.Children.Add(caption);
        panel.Children.Add(valueLabel);
        panel.Children.Add(slider);

        grid.Children.Add(panel);
    }

    public void DrawGraph()
    {
        canvas.Children.Clear();

        float xCenter = (float)canvas.Bounds.Width / 2;
        float yCenter = (float)canvas.Bounds.Height / 2;

        float size = yCenter;

        if (size > xCenter)
        {
            size = xCenter;
        }

        Point[] points = new Point[numLoops * stepsPerLoop + 1];

        int pointIndex = 0;

        points[pointIndex++] = new Point(xCenter, -yCenter + size);

        float ratio = ratioMajor + ratioMinor;

        float radius = 1;

        float dThetaX = 2 * (float)Math.PI / stepsPerLoop;
        float dThetaY = ratio * dThetaX;
        float dRadius = shrinkRate / stepsPerLoop;

        for (int loop = 0; loop < numLoops; ++loop)
        {
            float thetaX = loop * 2 * (float)Math.PI;
            float thetaY = ratio * thetaX;

            for (int step = 1; step <= stepsPerLoop; ++step)
            {
                thetaX += dThetaX;
                thetaY += dThetaY;
                radius -= dRadius;

                float x = radius * (float)Math.Sin(thetaX);
                float y = radius * (float)Math.Cos(thetaY);

                points[pointIndex++] = new Point(x * size + xCenter, -y * size + yCenter);
            }
        }
        
        canvas.Children.Add(new Polyline {Stroke = Brushes.White, StrokeThickness = 1.5, Points = points});
    }
}

internal class SliderManager
{
    System.Action<double> useValue;
    private Label valueLabel;
    private string format;

    private event Action DrawGraph;

    public SliderManager(Slider slider, Action<double> useValue, Label valueLabel, string format, HarmonographWindow h)
    {
        slider.ValueChanged += ValueChanged;
        this.useValue = useValue;
        this.valueLabel = valueLabel;
        this.format = format;
        valueLabel.Content = slider.Value.ToString(format);
        DrawGraph += h.DrawGraph;
    }

    void ValueChanged(object s, RoutedEventArgs e)
    {
        Slider slider = s as Slider;
        useValue(slider.Value);
        valueLabel.Content = slider.Value.ToString(format);
        DrawGraph();
    }
}
internal class FileCommand : ICommand
{
    Window win;

    public FileCommand(Window win)
    {
        this.win = win;
    }
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        Window dialog = new Window
        {
            Width = 640,
            Height = 360,
            Background = Brushes.Black,
            Content = new Label { Content = $"Coming soon...", Foreground = Brushes.Red, FontSize = 24,}
        };

        dialog.ShowDialog(win);
    }
}