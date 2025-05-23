﻿using System.Windows.Navigation;

namespace Tetris
{
  public class GameState
  {
    private Block currentBlock;

    public Block CurrentBlock
    {
      get => currentBlock;
      private set
      {
        currentBlock = value;
        // reset the current start position and roatation.
        currentBlock.Reset();

        //make the block spawn at the top of the grid and not from the hidden starting pos
        for (int i = 0; i < 2; i++)
        {
          currentBlock.Move(1, 0);

          if (!BlockFits())
          {
            currentBlock.Move(-1, 0);
          }
        }
      }

    }

    public GameGrid GameGrid { get; }
    public BlockQueue BlockQueue { get; }
    public bool GameOver { get; private set; }
    public int Score { get; private set; }
    public Block HeldBlock { get; private set; }
    public bool CanHold { get; private set; }


    public GameState()
    {
      GameGrid = new GameGrid(22, 10);
      BlockQueue = new BlockQueue();
      CurrentBlock = BlockQueue.GetAndUpdate();
      CanHold = true;
    }

    private bool BlockFits()
    {
      foreach (Position p in CurrentBlock.TilePositions())
      {
        if (!GameGrid.isEmpty(p.Row, p.Column))
        {
          return false;
        }
      }

      return true;
    }

    public void HoldBlock()
    {
      if (!CanHold)
      {
        return;
      }

      if (HeldBlock == null)
      {
        HeldBlock = currentBlock;
        CurrentBlock = BlockQueue.GetAndUpdate();
      }
      else
      {
        Block tmp = CurrentBlock;
        CurrentBlock = HeldBlock;
        HeldBlock = tmp;
      }

      // stop spam holding
      CanHold = false;


    }

    public void RotateBlockCW()
    {
      CurrentBlock.RotateCW();

      if (!BlockFits())
      {
        CurrentBlock.RotateCCW();
      }
    }

    public void RotateBlockCCW()
    {
      CurrentBlock.RotateCCW();

      if (!BlockFits())
      {
        CurrentBlock.RotateCW();
      }
    }

    public void MoveBlockLeft()
    {
      CurrentBlock.Move(0, -1);

      if (!BlockFits())
      {
        CurrentBlock.Move(0, 1);
      }
    }

    public void MoveBlockRight()
    {
      CurrentBlock.Move(0, 1);

      if (!BlockFits())
      {
        CurrentBlock.Move(0, -1);
      }
    }

    private bool IsGameOver()
    {
      // if either of the 2 hidden rows at the top of the grid are not empty then game over
      return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
    }

    private void PlaceBlock()
    {
      foreach (Position p in CurrentBlock.TilePositions())
      {
        GameGrid[p.Row, p.Column] = CurrentBlock.Id;
      }

      Score += GameGrid.ClearFullRows();

      if (IsGameOver())
      {
        GameOver = true;
      }
      else
      {
        CurrentBlock = BlockQueue.GetAndUpdate();
        CanHold = true;
      }
    }

    public void MoveBlockDown()
    {
      CurrentBlock.Move(1, 0);

      if (!BlockFits())
      {
        CurrentBlock.Move(-1, 0);
        PlaceBlock();
      }
    }

    private int TileDropDistance(Position p)
    {
      int drop = 0;

      while (GameGrid.isEmpty(p.Row + drop + 1, p.Column))
      {
        drop++;
      }

      return drop;
    }

    public int BlockDropDistance()
    {
      int drop = GameGrid.Rows;

      foreach (Position p in CurrentBlock.TilePositions())
      {
        drop = System.Math.Min(drop, TileDropDistance(p));
      }

      return drop;
    }

    public void DropBlock()
    {
      CurrentBlock.Move(BlockDropDistance(), 0);
      PlaceBlock();
    }


  }
}
