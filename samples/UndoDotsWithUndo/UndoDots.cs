using System.Reflection;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.FileProviders;

public class UndoDots
{
    public Window win;

    Bitmap blackSquare;
    Bitmap orangeDot;
    Bitmap undo; // A picture for the "Undo" button.

    public UndoDots()
    {
        win = MakeWindow();
 
        Grid grid = MakeGrid();

        GetBitmaps();

        // All of the work we need to do to undo marking spots on our bingo
        // card will be handled by just one History object. We'll create it
        // now, and save a reference to it we can pass to each and every
        // CellHandler object.

        History history = new History();

        // Give a copy of the reference to the black square picture to the
        // History object, so the History object can change orange circles
        // back to black squares as part of the undo operation.

        history.blackSquare = blackSquare;

        // Now the LoadImagesInto method needs a reference to the History
        // object to pass that to the CellHandler objects it creates.

        LoadImagesInto(grid, history);

        // Now we'll add the "Undo" button, which will assign a method
        // to the PointerPressed event for the image in the cell that
        // contains it.

        AddUndoButtonTo(grid, history);

        win.Content = grid;
        win.Show();
    }

    Window MakeWindow()
    {
        Window window = new Window
        {
            Height = 600,
            Width = 500,
            Title = "UndoDotsWithUndo v1.0",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        return window;
    }

    Grid MakeGrid()
    {
        Grid grid = new Grid
        {
            RowDefinitions = RowDefinitions.Parse("100, 100, 100, 100, 100, 100"),
            ColumnDefinitions = ColumnDefinitions.Parse("100, 100, 100, 100, 100"),
            Background = Brushes.LightGreen,
            ShowGridLines = false,
        };

        return grid;
    }

    void GetBitmaps()
    {
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

        using (var reader = embeddedProvider.GetFileInfo("OrangeDot.png").CreateReadStream())
        {
            orangeDot = new Bitmap(reader);
        }

        using (var reader = embeddedProvider.GetFileInfo("BlackSquare.png").CreateReadStream())
        {
            blackSquare = new Bitmap(reader);
        }

        using (var reader = embeddedProvider.GetFileInfo("Undo.png").CreateReadStream())
        {
            undo = new Bitmap(reader);
        }
    }

    void LoadImagesInto(Grid grid, History history)
    {
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                Image image = new Image
                {
                    Source = blackSquare,
                };

                // Pass a reference the the History object so the CellHandler
                // can add information to the History object whenever it makes
                // a change what is shown in the Grid.

                CellHandler cellHandler = new CellHandler(row, col, image, orangeDot, history);

                // The History object will keep its own model of the bingo card.
                // In that model, which is two-dimensional array, it will keep
                // a reference to the CellHandler we have created for each of
                // cells. We'll assign that reference to the History model now.

                history.cellHandlers[row, col] = cellHandler;

                image.PointerPressed += cellHandler.OnPointerPressed;

                image.SetValue(Grid.RowProperty, row);
                image.SetValue(Grid.ColumnProperty, col);

                grid.Children.Add(image);
            }
        }
    }

    // Put an "Undo" button in the middle column of the bottom
    // row of the Grid.

    void AddUndoButtonTo(Grid grid, History history)
    {
        // Create a Image using the "Undo" picture as its Source.

        Image undoImage = new Image
        {
            Source = undo,
        };

        // Set the row and column properties of the Undo button
        // so it appears in the right place.

        undoImage.SetValue(Grid.RowProperty, 5);
        undoImage.SetValue(Grid.ColumnProperty, 2);

        // When the Undo button's Image is clicked, it will raise
        // a PointerPressed event. We will want this to call the
        // Undo method in the History object.

        undoImage.PointerPressed += history.Undo;

        // Add the Undo button the Grid's children, so it is visible.
        
        grid.Children.Add(undoImage);
    }
}