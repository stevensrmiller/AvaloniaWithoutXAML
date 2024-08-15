using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

// Create a simple grid with two sliders, two progress bars, and two
// labels. The labels and progress bars will track the user's movement
// of the sliders.

internal class SlidersProgressWindow
{
    public Window win;
    ProgressBar progBarH;
    ProgressBar progBarV;
    Label labelH;
    Label labelV;
    public SlidersProgressWindow()
    {
        win = new Window
        {
            Title = "SlidersProgressWindow v3.0",
            Height = 360,
            Width = 640,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        // Note that our grid is 3x3, but the top row is twice as high
        // as the two bottom rows, and the leftmost column is twice as
        // wide as the two other columns.

        Grid grid = new Grid
        {
            ShowGridLines = true,
            Background = Brushes.Yellow,
            RowDefinitions = RowDefinitions.Parse("50*, 25*, 25*"),
            ColumnDefinitions = ColumnDefinitions.Parse("50*, 25*, 25*"),
        };

        Slider sliderH = new Slider
        {
            Minimum = -10,
            Maximum = 50,
            Value = -10,
            Margin = Thickness.Parse("10"),
            VerticalAlignment = VerticalAlignment.Center,
        };

        sliderH.SetValue(Grid.RowProperty, 2);
        sliderH.SetValue(Grid.ColumnProperty, 0);

        // We want the slider's handler called whenever it moves.

        sliderH.PointerMoved += SliderHMoved;
        
        grid.Children.Add(sliderH);

        // Sliders are horizontal by default, so we'll set this one
        // to a vertical orientation explicitly.

        Slider sliderV = new Slider
        {
            Minimum = 10,
            Maximum = 150,
            Value = 80,
            Margin = Thickness.Parse("10"),
            HorizontalAlignment = HorizontalAlignment.Center,
            Orientation = Orientation.Vertical,
        };
        
        grid.Children.Add(sliderV);

        // Our slider will span multiple rows. The row property
        // we set here sets the topmost row it will span.

        sliderV.SetValue(Grid.RowProperty, 0);
        sliderV.SetValue(Grid.ColumnProperty, 2);

        // We can make the slider span as many grid rows as we want
        // it to. Here, we'll set it to span the entire height of
        // the grid by setting the span property to 3.

        sliderV.SetValue(Grid.RowSpanProperty, 3);

        sliderV.PointerMoved += SliderVMoved;
 
        progBarH = new ProgressBar
        {
            Minimum = -10,
            Maximum = 50,
            Value = -10,
            Height = 20,
            Margin = Thickness.Parse("10"),
            VerticalAlignment = VerticalAlignment.Center,
        };

        progBarH.SetValue(Grid.RowProperty, 1);
        progBarH.SetValue(Grid.ColumnProperty, 0);
        
        grid.Children.Add(progBarH);

        // Note that we did not need to set vertical slider's
        // vertical alignment to "Stretch," but we do have to
        // do this for the progress bar, or else its height
        // will match the height of its top row.

        progBarV = new ProgressBar
        {
            Minimum = 10,
            Maximum = 150,
            Value = 80,
            Width = 20,
            Margin = Thickness.Parse("10"),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Stretch,
            Orientation = Orientation.Vertical,
        };

        grid.Children.Add(progBarV);

        progBarV.SetValue(Grid.RowProperty, 0);
        progBarV.SetValue(Grid.RowSpanProperty, 3);
        progBarV.SetValue(Grid.ColumnProperty, 1);
        
        // For our labels, we'll juse a fixed-width font,
        // so the decimal point will stay in the same
        // place as the numbers change.

        labelH = new Label
        {
            VerticalAlignment = VerticalAlignment.Bottom,
            FontSize = 24,
            FontFamily = "Liberation Mono",
            FontWeight = FontWeight.Bold,
            Width = 200,
        };

        labelH.SetValue(Grid.RowProperty, 0);
        labelH.SetValue(Grid.ColumnProperty, 0);

        grid.Children.Add(labelH);

        labelV = new Label
        {
            VerticalAlignment = VerticalAlignment.Top,
            FontSize = 24,
            FontFamily = "Liberation Mono",
            FontWeight = FontWeight.Bold,
            Width = 200,
        };

        // Note that we are putting both labels into the same
        // grid cell. This works because one label is aligned
        // at the bottom, the other is at the top, and they don't
        // overlap each other.

        labelV.SetValue(Grid.RowProperty, 0);
        labelV.SetValue(Grid.ColumnProperty, 0);

        grid.Children.Add(labelV);

        win.Content = grid;
        win.Show();
    }

    // Whenever a slider moves, we'll set its associated
    // progress bar to the same value (of course, you can
    // change their relationship mathematically, if you want
    // to). We'll also use a simple interpolated string and
    // some .NET formatting to set the label text.
    
    void SliderHMoved(object s, RoutedEventArgs e)
    {
        Slider slider = s as Slider;
        progBarH.Value = slider.Value;
        labelH.Content = $"H = {slider.Value,8:0.00}";
    }

    void SliderVMoved(object s, RoutedEventArgs e)
    {
        Slider slider = s as Slider;
        progBarV.Value = slider.Value;
        labelV.Content = $"V = {slider.Value,8:0.00}";
    }
}