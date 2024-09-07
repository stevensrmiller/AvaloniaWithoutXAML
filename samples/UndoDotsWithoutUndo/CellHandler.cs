using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

// Objects of this class hand the PointerPressed events
// raised by the Images stored in the cells in the Grid.
// There will be one of these objects for each Image.
// Clicking on the Image will call the object's
// OnPointerPressed method, which will change that Image's
// Source to the orange dot.

public class CellHandler
{
    // Save the row and column numbers for the cell that
    // this CellHandler object handles.

    private int myRow;
    private int myCol;

    // Save a reference to the Image in the cell that this
    // CellHandler object handles, so this CellHandler can
    // can change that Image's Source.

    private Image myImage;

    // Keep a copy of a reference to the orange dot Bitmap,
    // so this CellHandler can change the Source of the
    // Image in the cell this CellHandler handles to the
    // orange dot when this cell's Image is clicked.

    private Bitmap myOrangeDot;

    // This constructor simply copies all the information that is
    // passed to it when a new CellHandler is created, so that
    // information is available when one of this CellHander's
    // methods are run at some future time.

    public CellHandler(int row, int col, Image image, Bitmap orangeDot)
    {
        myRow = row;
        myCol = col;
        myImage = image;
        myOrangeDot = orangeDot;
    }

    // When the Image in the cell that this CellHandler handles is
    // clicked, this method is called.

    public void OnPointerPressed(object sender, RoutedEventArgs e)
    {
        // If the Image in the cell that this CellHandler handles
        // already has an orange dot, there is nothing do, so just
        // return right away.

        if (myImage.Source == myOrangeDot)
        {
            return;
        }

        // Change the Source of the Image in the cell that this
        // CellHandler handles to an orange dot.

        myImage.Source = myOrangeDot;
    }
}