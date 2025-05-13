namespace Tetris
{
  public class GameGrid
  {
    // grid array - 0 is empty >0 contains block element
    private readonly int[,] grid;
    public int Rows { get; }
    public int Columns { get; }

    // Indexer to provide easy access to the array
    public int this[int r, int c]
    {
      get => grid[r, c];
      set => grid[r, c] = value;
    }

    public GameGrid(int rows, int columns)
    {
      Rows = rows;
      Columns = columns;
      grid = new int[rows, columns];
    }

    public bool IsInside(int r, int c)
    {
      // check to see if row/column is inside the grid
      return r >= 0 && r < Rows && c >= 0 && c < Columns;
    }

    public bool isEmpty(int r, int c)
    {
      // check to see if given cell contains a block element (return true if 0)
      return IsInside(r, c) && grid[r, c] == 0;
    }

    public bool IsRowFull(int r)
    {
      for (int c = 0; c < Columns; c++)
      {
        if (grid[r, c] == 0)
        {
          return false;
        }
      }

      return true;
    }

    public bool IsRowEmpty(int r)
    {
      for (int c = 0; c < Columns; c++)
      {
        if (grid[r, c] != 0)
        {
          return false;
        }

      }

      return true;
    }

    private void ClearRow(int r)
    {
      for (int c = 0; c < Columns; c++)
      {
        grid[r, c] = 0;
      }
    }

    private void MoveRowDown(int r, int numRows)
    {
      for (int c = 0; c < Columns; c++)
      {
        grid[r + numRows, c] = grid[r, c];
        grid[r, c] = 0;
      }
    }

    public int ClearFullRows()
    {
      int cleared = 0;

      for (int r = Rows-1; r >= 0; r--)
      {
        // the row is full we clear it
        if (IsRowFull(r))
        {
          ClearRow(r);
          cleared++;
        }
        // otherwise we move the (not full) row down by the current number of cleared rows
        else if (cleared > 0)
        {
          MoveRowDown(r, cleared);
        }
      }

      return cleared;
    }
  }
}

