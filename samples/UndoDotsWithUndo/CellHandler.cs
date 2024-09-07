using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

// The OnPointerPressed method adds information to the
// History object, so the History object can undo what
// the OnPointerPressed method did, if the History object
// gets asked to do that at some future time.

public class CellHandler
{
    private int myRow;
    private int myCol;
    private Image myImage;
    private Bitmap myOrangeDot;

    // Keep a copy of a reference to the one-and-only
    // History object, so this CellHandler can add information
    // to that History object, when when this CellHandler
    // changes something we might want to undo in the future.

    private History myHistory;

    public CellHandler(int row, int col, Image image, Bitmap orangeDot, History history)
    {
        myRow = row;
        myCol = col;
        myImage = image;
        myOrangeDot = orangeDot;
        myHistory = history;
    }

    public void OnPointerPressed(object sender, RoutedEventArgs e)
    {
        if (myImage.Source == myOrangeDot)
        {
            return;
        }

        // We are going to change the Image in the cell that this
        // CellHander handles from a black square to an orange dot.
        // Tell the History object which row and column numbers
        // this cell is in, so it can undo the change at some future
        // time, if it is asked to do so.

        myHistory.Add(myRow, myCol);

        myImage.Source = myOrangeDot;
    }

    // When it is asked to undo a change in the cell that this
    // CellHandler handles, the History object will call this
    // CellHandler object's SetSourceToBitmap method, passing
    // a reference to the black square picture.
    public void SetSourceToBitmap(Bitmap bitmap)
    {
        // Set the Source of the Image in the cell that this
        // CellHandler handles to the picture passed in the
        // bitmap argument (which will be the black square).
        
        myImage.Source = bitmap;
    }
}