namespace Tetris
{
  // a class type that will hold the position of various game objects
  public class Position
  {
    public int Row { get; set; }
    public int Column { get; set; }

    public Position(int row, int column)
    {
      Row = row; 
      Column = column;
    }
  }
}
