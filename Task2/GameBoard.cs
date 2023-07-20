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
    /// Ход первого игрока.
    /// </summary>
    private bool isFirst = true;

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
      Console.WriteLine();
      Console.WriteLine("-------------");
      for(int i = 0; i < 3; i ++)
      {
        Console.WriteLine("|   |   |   |");
        Console.WriteLine("| " + this.boardCell[3*i] + " | " + this.boardCell[3*i + 1] + " | " + this.boardCell[3*i + 2] + " |");
        Console.WriteLine("|   |   |   |");
        Console.WriteLine("-------------");
      }
    }

    /// <summary>
    /// Ходы игроков.
    /// </summary>
    /// <param name="cell">Заменяемое поле.</param>
    /// <returns>true - если все прошло успешно, false - в противном случае.</returns>
    public bool TurnPlayer(int cell)
    {
      if(cell < 1 || cell > 9)
      {
        return false;
      }

      var cellValue = this.boardCell[cell - 1];
      if(cellValue == 'X' || cellValue == 'O')
      {
        return false;
      }

      if (this.isFirst)
      {
        this.boardCell[cell - 1] = 'X';
      }
      else
      {
        this.boardCell[cell - 1] = 'O';
      }
      this.isFirst = !this.isFirst;
      return true;
    }
  }
}
