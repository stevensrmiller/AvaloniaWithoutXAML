using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

internal class GridWindow
{
    public Window win;

    public GridWindow()
    {
        win = new Window
        {
            Width = 800,
            Height = 450,
            Title = "GridWindow",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        // Grids can have any number of rows and any number of columns.
        // The size (width for column, height for a row) is one of "Auto,"
        // a number, or "[n]*," where n is an optional number.
        //
        // Sizes specified as numbers set the size to that number.
        // "Auto" sizes are wide enough to hold the widest control in
        // that row or column.
        // Whatever's left after the numbers and auto-sized parts is
        // distributed proportionally across all section with * sizes.
        // The proportion is equal to a given section's portion of the
        // sum of all sections with * sizes (note that n = 1 if the size
        // is just a star with no number).
        //
        // So, if three columns are set to sizes 5*, *, 10* respectively,
        // the total size of all three is 5 + 1 + 10 = 16. The first gets
        // 5/16th's of the space available. The second gets 1/16th. The
        // third gets 10/16th's.

        Grid grid = new Grid
        {
            ShowGridLines = true, // This is useful for desiging your layout.
            Background = Brushes.Navy,
            RowDefinitions = RowDefinitions.Parse("100, Auto, 3*, 2*"),
            ColumnDefinitions = ColumnDefinitions.Parse("Auto, *, 150, Auto"),
        };

        win.Content = grid;

        // This AddToGrid helper method keeps things clear. The arguments are the grid,
        // the row and column to put a label, the height and width of the lable, and
        // the alignment. Note that if an alignment is "Stretch," the size given for
        // that direction is ignored.

        AddToGrid(grid, 0, 0, 50, 80, VerticalAlignment.Top, HorizontalAlignment.Left);
        AddToGrid(grid, 0, 1, 50, 80, VerticalAlignment.Top, HorizontalAlignment.Center);
        AddToGrid(grid, 0, 2, 0, 80, VerticalAlignment.Stretch, HorizontalAlignment.Left);
        AddToGrid(grid, 0, 3, 50, 80, VerticalAlignment.Top, HorizontalAlignment.Left);

        AddToGrid(grid, 1, 0, 75, 80, VerticalAlignment.Top, HorizontalAlignment.Left);
        AddToGrid(grid, 1, 1, 50, 0, VerticalAlignment.Bottom, HorizontalAlignment.Stretch);
        AddToGrid(grid, 1, 2, 50, 80, VerticalAlignment.Top, HorizontalAlignment.Right);
        AddToGrid(grid, 1, 3, 30, 60, VerticalAlignment.Center, HorizontalAlignment.Center);

        AddToGrid(grid, 2, 0, 50, 80, VerticalAlignment.Center, HorizontalAlignment.Left);
        AddToGrid(grid, 2, 1, 50, 80, VerticalAlignment.Stretch, HorizontalAlignment.Center);
        AddToGrid(grid, 2, 2, 50, 80, VerticalAlignment.Top, HorizontalAlignment.Left);
        AddToGrid(grid, 2, 3, 75, 90, VerticalAlignment.Top, HorizontalAlignment.Left);

        AddToGrid(grid, 3, 0, 50, 80, VerticalAlignment.Bottom, HorizontalAlignment.Left);
        AddToGrid(grid, 3, 1, 50, 80, VerticalAlignment.Center, HorizontalAlignment.Center);
        AddToGrid(grid, 3, 2, 50, 80, VerticalAlignment.Stretch, HorizontalAlignment.Stretch);
        AddToGrid(grid, 3, 3, 50, 80, VerticalAlignment.Bottom, HorizontalAlignment.Right);
 
        win.Show();
    }

    void AddToGrid(Grid grid, int row, int col, float height, float width, VerticalAlignment vAlign, HorizontalAlignment hAlign)
    {
        var label = new Label
        {
            Background = Brushes.White,
            Foreground = Brushes.Black,
            Content = $"r:{row}, c:{col}",

            // If either alignment is a "Stretch," we need to set the size
            // in that direction to NaN. Note that this is the default value,
            // so in code that always sets a stretch, you can just leave the
            // size setting out entirely. Here, we need to test to see if
            // we want to use an actual size, or do we want the stretch.
            // If we want the stretch, we set the size to Nan.

            Height = vAlign == VerticalAlignment.Stretch ? float.NaN : height,
            Width = hAlign == HorizontalAlignment.Stretch? float.NaN : width,
            VerticalAlignment = vAlign,
            HorizontalAlignment = hAlign,
            VerticalContentAlignment = VerticalAlignment.Center,
            HorizontalContentAlignment = HorizontalAlignment.Center,
        };

        // Note that the position of a control is value assigned to that control.
        // You can change these later to move a control to other positions in the
        // grid.

        label.SetValue(Grid.RowProperty, row);
        label.SetValue(Grid.ColumnProperty, col);

        grid.Children.Add(label);
    }
}