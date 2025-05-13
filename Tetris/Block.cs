using System.Collections.Generic;

namespace Tetris
{
  // we will use this abstract class to create a sub class for each specific block type
  public abstract class Block
  {
    // an array that holds the positions of the block in each of its 4 rotational states
    protected abstract Position[][] Tiles { get; }
    protected abstract Position StartOffset { get; }
    // block type identifier (e.g. line ---- = 0, L = 1 etc...)
    public abstract int Id { get; }

    private int rotationState;
    private Position offset;

    public Block()
    {
      //set the starting position
      offset = new Position(StartOffset.Row, StartOffset.Column);
    }

    public IEnumerable<Position> TilePositions()
    {
      foreach (Position p in Tiles[rotationState])
      {
        yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
      }
    }

    public void RotateCW()
    {
      // rotate the block 90deg Clockwise
      rotationState = (rotationState + 1) % Tiles.Length;
    }

    public void RotateCCW()
    {
      // rotate the block 90deg Anti-clockwise
      if (rotationState == 0)
      {
        rotationState = Tiles.Length - 1;
      }
      else
      {
        rotationState--;
      }
    }

    public void Move(int rows, int columns)
    {
      offset.Row += rows;
      offset.Column += columns;
    }

    public void Reset()
    {
      rotationState = 0;
      offset.Row = StartOffset.Row;
      offset.Column = StartOffset.Column;
    }
  }
}
