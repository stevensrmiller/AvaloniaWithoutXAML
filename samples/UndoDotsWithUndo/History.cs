using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

// To undo changes, we will need exactly one History object
// that can save whatever information it needs to undo them.
// This object will maintain its own model of the grid, and
// call the CellHandler object that handles the cell at a
// particular row and column in the Grid, to undo changes that
// were made to the Image in the cell at the row and column.

public class History
{
    // The UndoDots constructor will assign a copy of a reference
    // to the black square picture here. We'll use this to ask
    // the right CellHanderl object to change the Image in the
    // cell it handles to black square when we want to undo a
    // change that's been made to the Image in that cell.

    public Bitmap blackSquare;

    // We'll create a two-dimensional array to model the bingo
    // card. We index the element with two numbers instead of
    // just one. The first number will be the same as a row
    // number in our Grid. The second number will be the same
    // as a column number in our Grid. Each element in our
    // model will contain a reference to the CellHandler object
    // that handles the cell in the Grid at that row and column.

    public CellHandler[,] cellHandlers = new CellHandler[5, 5];
    
    // With only 25 squares, there will be, at most, 25 changes
    // we might have to undo. To undo them, we will need to keep
    // a record, or a kind of "diary," that lists the row and
    // the column of each cell that is changed, in the order in
    // which the changes are made.

    public int[] rowHistory = new int[25];
    public int[] colHistory = new int[25];

    // We will count changes as they are made. This count will
    // also let us index the rowHistory and colHistory arrays,
    // so we can add new entries to our "diary" when things
    // change. We will subtract one from this count every time
    // we are asked to undo a change. We will use the new count
    // value obtained after subtracting one to read the entry
    // in the "diary" (our two arrays) that will tell us which
    // cell needs to be undone.

    public int count = 0;

    // The CellHandler at a given cell will call this method,
    // telling us the row and column that it is changing. We will
    // save that row and column in our "diary" (our two arrays),
    // and add one to the running count of changes.
    public void Add(int row, int col)
    {
        // Save the row and column where the change is being made.

        rowHistory[count] = row;
        colHistory[count] = col;

        // Increase the count of changes made.

        count = count + 1;
    }

    // When the Undo button is clicked, it calls this method to undo
    // the most recent change made that has not yet been undone.
    public void Undo(object sender, RoutedEventArgs e)
    {
        // If the count is zero, there are no changes to undo.

        if (count > 0)
        {
            // Back the count down by one, and use that to index
            // the "diary" (our two arrays).

            count = count - 1;

            // Get the row and the column of the cell to undo out
            // of our "diary" (our two arrays).

            int rowToUndo = rowHistory[count];
            int colToUndo = colHistory[count];

            // Get a referernce to the CellHandler that handles the
            // cell in the Grid we want to undo. Those references are
            // all in our model of the Grid, so we just read the element
            // out of our two-dimensional array by using the cell's row
            // number and the cell's column number.

            CellHandler handlerToDoTheUndoing = cellHandlers[rowToUndo, colToUndo];

            // Now that we have a reference to the CellHandler that
            // handles the cell in the Grid we want to undo, just
            // call that CellHandler's SetSourceToBitmap method, and
            // give it a referene to the black square Bitmap. The
            // method will change the Image's Source back to the
            // black square, undoing the previous change to the
            // orange dot.
            
            handlerToDoTheUndoing.SetSourceToBitmap(blackSquare);
        }
    }
}