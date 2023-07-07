using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
  internal class GameBoard
  {
    /// <summary>
    /// Значения ячеек игрового поля.
    /// </summary>
    private char[] boardCell;

    /// <summary>
    /// Инициализая игры.
    /// </summary>
    public GameBoard()
    {
      this.boardCell = new char[]{ '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    }

    /// <summary>
    /// Отрисовка игрового поля в консоли.
    /// </summary>
    public void DrawBoard()
    {
      Console.WriteLine("-------------");
      for(int i = 0; i < 3; i ++)
      {
        Console.WriteLine("|   |   |   |");
        Console.WriteLine("| " + this.boardCell[3*i] + " | " + this.boardCell[3*i + 1] + " | " + this.boardCell[3*i + 2] + " |");
        Console.WriteLine("|   |   |   |");
        Console.WriteLine("-------------");
      }
    }
  }
}
