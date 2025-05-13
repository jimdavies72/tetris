using System;

namespace Tetris
{
  public class BlockQueue
  {
    // responsible for picking the next block inthe game
    private readonly Block[] blocks = new Block[]
    {
      new IBlock(),
      new JBlock(),
      new LBlock(),
      new OBlock(),
      new SBlock(),
      new TBlock(),
      new ZBlock(),
    };

    private readonly Random random = new Random();

    public Block NextBlock { get; private set; }

    public BlockQueue()
    {
      NextBlock = RandomBlock();
    }

    private Block RandomBlock()
    {
      return blocks[random.Next(blocks.Length)];
    }

    public Block GetAndUpdate()
    {
      Block block = NextBlock;

      // we will loop until we get a block that is not the same as the current block
      do
      {
        NextBlock = RandomBlock();
      }
      while (block.Id == NextBlock.Id);

      return block;
    }
  }
}
