﻿namespace Tetris
{
  public class TBlock : Block
  {
    // store the tile positions for the 4 rotation states
    private readonly Position[][] tiles = new Position[][]
    {
      new Position[] { new(0,1), new(1,0), new(1,1), new(1,2) },
      new Position[] { new(0,1), new(1,1), new(1,2), new(2,1) },
      new Position[] { new(1,0), new(1,1), new(1,2), new(2,1) },
      new Position[] { new(0,1), new(1,0), new(1,1), new(2,1) }

    };

    public override int Id => 6;
    protected override Position[][] Tiles => tiles;
    protected override Position StartOffset => new Position(0, 3);
  }
}
