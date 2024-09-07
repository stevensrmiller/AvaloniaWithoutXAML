using System.Reflection;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.FileProviders;

public class UndoDots
{
    public Window win; // The main display window.

    Bitmap blackSquare; // This will be the picture before a cell is clicked.
    Bitmap orangeDot; // Use this picture after a cell is clicked.

    // Construct the UndoDots "bingo" card. To make it easier to understand,
    // the major operations needed are all in individual methods. So the
    // constructor will call them, one by one.

    public UndoDots()
    {
        // Call the method to make the main Window. Save a reference to that
        // Window in our public member field called "win."

        win = MakeWindow();
 
        // Call the method to make our bingo card grid. Note that we will
        // keep a reference to the Grid object, so we can use it again in this
        // constructor method. But we won't need it to be a public member of
        // the UndoDots object.

        Grid grid = MakeGrid();

        // Retrieve the pictures we embedded as resources when we compiled
        // this project and store them as Bitmaps.

        GetBitmaps();

        // Add the 25 black squares to the Grid. This method will also
        // create a new CellHandler object for each cell in the Grid, and
        // assign a method in that CellHandler to the PointerPressed
        // event for the image in the cell that contains the black square.

        LoadImagesInto(grid);

        // Our main Window's one-and-only content item will be the Grid.
        // The grid contains everything else.

        win.Content = grid;

        // Make the Window visible.

        win.Show();
    }

    // Create our top-level Window and return a reference to the Window
    // object.

    Window MakeWindow()
    {
        Window window = new Window
        {
            Height = 600, // Six rows, 100 pixels high.
            Width = 500, // Five columns, 100 pixels wide.
            Title = "UndoDotsWithoutUndo v1.0",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        return window;
    }

    // This Grid will contain our 25 bingo card cells, and and extra
    // row for the Undo button at the bottom.

    Grid MakeGrid()
    {
        Grid grid = new Grid
        {
            // Six rows, each 100 pixels high.
            RowDefinitions = RowDefinitions.Parse("100, 100, 100, 100, 100, 100"),
            
            // Five columns, each 100 pixels wide.
            ColumnDefinitions = ColumnDefinitions.Parse("100, 100, 100, 100, 100"),
            
            // Choose a pretty color for the background.
            Background = Brushes.LightGreen,
            
            // Set this to true if you want to see the lines.
            ShowGridLines = false,
        };

        return grid;
    }

    // We embedded three pictures into this project when we compiled it, so we could
    // use them later without storing them in separate files. We will extract them
    // now and save them as Bitmaps.

    void GetBitmaps()
    {
        // To retrieve an embedded resource, we need an EmbeddedFileProvider. Thare
        // are several ways to get one. This technique will work well for us.

        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

        // Access each embedded resource as a "stream" of data. We'll use the stream to
        // create a Bitmap from each picture we embedded. A C# stream has to be closed
        // when you are done using it, or else it takes up some limited system resources
        // we would rather not use after we are done with the stream. To close it, you
        // call the stream's Dispose method. Here, the C# keyword "using" will make C#
        // call Dispose automatically at the end of the block of code (only one line long
        // in this case) in curly brackets underneath it. That way, we can't forget it.

        // Read the orange dot picture and save it as a Bitmap.

        using (var reader = embeddedProvider.GetFileInfo("OrangeDot.png").CreateReadStream())
        {
            orangeDot = new Bitmap(reader);
        }

        // Read the black square picture and save it as a Bitmap.
        
        using (var reader = embeddedProvider.GetFileInfo("BlackSquare.png").CreateReadStream())
        {
            blackSquare = new Bitmap(reader);
        }
    }

    // Fill top 25 cells of the Grid with Images that each have use the
    // picture of the black square. For each cell, create a completely
    // new CellHandler object. Each CellHandler keeps its own copy of the
    // row and column number for the Grid cell it handles. Each one gets
    // a new Image. We will assign a method in each cell's unique
    // CellHandler to handle PointerPressed events for that cell's Image.
    // That way, when we click on an Image in a cell, the CellHandler
    // object we created for that cell will run its handler method and,
    // because the CellHandler object kept a copy of the row and column
    // number for the cell it handles, it will know which cell in the Grid
    // to operate on.

    void LoadImagesInto(Grid grid)
    {
        // We will do the rows, one by one, from row 0 to row 4.

        for (int row = 0; row < 5; row++)
        {
            // For each row, we will do the columns, from column 0
            // to column 4. Since there are five rows and five
            // columns, the block of code below this "for" statement
            // will run a total of 25 times.

            for (int col = 0; col < 5; col++)
            {
                // For this row and column out of the total of 25,
                // create a new Image, and give it a reference to
                // the black square picture to use as its Source.

                Image image = new Image
                {
                    Source = blackSquare,
                };

                // Create a new CellHandler object fof this cell.
                // Tell it which row and which column it handles. Also,
                // give it a reference to the picture of the orange dot,
                // so it can change the Image in the cell it handles from
                // the black square as its source to the orange dot, when
                // the Image in that cell is clicked.

                CellHandler cellHandler = new CellHandler(row, col, image, orangeDot);

                // When the Image is clicked, it will raise a PointerPressed
                // event. Here, we make sure that event calls the OnPointerPressed
                // method for the unique CellHandler we created above, to handle
                // this cell. This is why each cell has its own CellHandler object.
                // Each of those objects only has to handle its own cell.

                image.PointerPressed += cellHandler.OnPointerPressed;

                // Set the row and column properties of the Image, so it shows
                // up in the right cell on the Gridl.

                image.SetValue(Grid.RowProperty, row);
                image.SetValue(Grid.ColumnProperty, col);

                // Finally, add the Image to the Grid's children. This is what
                // makes the Image visible.

                grid.Children.Add(image);
            }
        }
    }
}