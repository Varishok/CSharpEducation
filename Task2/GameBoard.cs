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
    /// Цвет ячеек поля.
    /// </summary>
    private ConsoleColor[] cellColor;

    /// <summary>
    /// Изначальный цвет ячейки.
    /// </summary>
    private const ConsoleColor defaultColor = ConsoleColor.White;

    /// <summary>
    /// Инициализая игры.
    /// </summary>
    public GameBoard()
    {
      this.boardCell = new char[]{ '1', '2', '3', '4', '5', '6', '7', '8', '9' };
      this.cellColor = new ConsoleColor[] {
        defaultColor, defaultColor, defaultColor, 
        defaultColor, defaultColor, defaultColor,
        defaultColor, defaultColor, defaultColor
      };
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
        Console.Write("| ");
        for (int j = 0; j < 3; j++)
        {
          this.DrawCell(3 * i + j);
          Console.Write(" | ");
        }
        Console.WriteLine();
        Console.WriteLine("|   |   |   |");
        Console.WriteLine("-------------");
      }
    }

    /// <summary>
    /// Отрисовка ячейки с цветом.
    /// </summary>
    /// <param name="cellIndex">Индекс ячейки.</param>
    private void DrawCell(int cellIndex)
    {
      Console.ForegroundColor = this.cellColor[cellIndex];
      Console.Write(this.boardCell[cellIndex]);
      Console.ResetColor();
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
        this.cellColor[cell - 1] = ConsoleColor.Red;
      }
      else
      {
        this.boardCell[cell - 1] = 'O';
        this.cellColor[cell - 1] = ConsoleColor.Green;
      }
      this.isFirst = !this.isFirst;
      return true;
    }

    /// <summary>
    /// Проверка выигрыша.
    /// </summary>
    /// <returns>0 - победитель неопределен. 1 - победили крестики. -1 - победили нолики.</returns>
    public int CheckWinner()
    {
      string winningCombination = "";
      if (this.boardCell[0].Equals(this.boardCell[1]) && this.boardCell[0].Equals(this.boardCell[2]))
      {
        winningCombination += "012";
      }
      if (this.boardCell[3].Equals(this.boardCell[4]) && this.boardCell[3].Equals(this.boardCell[5]))
      {
        winningCombination += "345";
      }
      if (this.boardCell[6].Equals(this.boardCell[7]) && this.boardCell[6].Equals(this.boardCell[8]))
      {
        winningCombination += "678";
      }
      if (this.boardCell[0].Equals(this.boardCell[3]) && this.boardCell[0].Equals(this.boardCell[6]))
      {
        winningCombination += "036";
      }
      if (this.boardCell[1].Equals(this.boardCell[4]) && this.boardCell[1].Equals(this.boardCell[7]))
      {
        winningCombination += "147";
      }
      if (this.boardCell[2].Equals(this.boardCell[5]) && this.boardCell[2].Equals(this.boardCell[8]))
      {
        winningCombination += "258";
      }
      if (this.boardCell[0].Equals(this.boardCell[4]) && this.boardCell[0].Equals(this.boardCell[8]))
      {
        winningCombination += "048";
      }
      if (this.boardCell[2].Equals(this.boardCell[4]) && this.boardCell[2].Equals(this.boardCell[6]))
      {
        winningCombination += "246";
      }
      if (!winningCombination.Equals(""))
      {
        int cellIndex = 0;
        foreach (var cell in winningCombination) 
        {
          cellIndex = cell - '0';
          this.cellColor[cellIndex] = ConsoleColor.Yellow;
        }
        if (this.boardCell[cellIndex].Equals('X'))
        {
          return 1;
        }
        else
        {
          return -1;
        }
      }
      return 0;
    }

    /// <summary>
    /// Проверка ничьи.
    /// </summary>
    /// <returns>true - ничья. false - ничьи нет.</returns>
    public bool IsDraw()
    {
      if(!this.boardCell[0].Equals('O') && !this.boardCell[1].Equals('O') && !this.boardCell[2].Equals('O')) //012 - X или пусто
      {
        return false;
      }
      if (!this.boardCell[0].Equals('O') && !this.boardCell[3].Equals('O') && !this.boardCell[6].Equals('O')) //036 - X или пусто
      {
        return false;
      }
      if (!this.boardCell[0].Equals('O') && !this.boardCell[4].Equals('O') && !this.boardCell[8].Equals('O')) //048 - X или пусто
      {
        return false;
      }
      if (!this.boardCell[1].Equals('O') && !this.boardCell[4].Equals('O') && !this.boardCell[7].Equals('O')) //147 - X или пусто
      {
        return false;
      }
      if (!this.boardCell[2].Equals('O') && !this.boardCell[4].Equals('O') && !this.boardCell[6].Equals('O')) //246 - X или пусто
      {
        return false;
      }
      if (!this.boardCell[2].Equals('O') && !this.boardCell[5].Equals('O') && !this.boardCell[8].Equals('O')) //258 - X или пусто
      {
        return false;
      }
      if (!this.boardCell[3].Equals('O') && !this.boardCell[4].Equals('O') && !this.boardCell[5].Equals('O')) //345 - X или пусто
      {
        return false;
      }
      if (!this.boardCell[6].Equals('O') && !this.boardCell[7].Equals('O') && !this.boardCell[8].Equals('O')) //678 - X или пусто
      {
        return false;
      }

      if (!this.boardCell[0].Equals('X') && !this.boardCell[1].Equals('X') && !this.boardCell[2].Equals('X')) //012 - O или пусто
      {
        return false;
      }
      if (!this.boardCell[0].Equals('X') && !this.boardCell[3].Equals('X') && !this.boardCell[6].Equals('X')) //036 - O или пусто
      {
        return false;
      }
      if (!this.boardCell[0].Equals('X') && !this.boardCell[4].Equals('X') && !this.boardCell[8].Equals('X')) //048 - O или пусто
      {
        return false;
      }
      if (!this.boardCell[1].Equals('X') && !this.boardCell[4].Equals('X') && !this.boardCell[7].Equals('X')) //147 - O или пусто
      {
        return false;
      }
      if (!this.boardCell[2].Equals('X') && !this.boardCell[4].Equals('X') && !this.boardCell[6].Equals('X')) //246 - O или пусто
      {
        return false;
      }
      if (!this.boardCell[2].Equals('X') && !this.boardCell[5].Equals('X') && !this.boardCell[8].Equals('X')) //258 - O или пусто
      {
        return false;
      }
      if (!this.boardCell[3].Equals('X') && !this.boardCell[4].Equals('X') && !this.boardCell[5].Equals('X')) //345 - O или пусто
      {
        return false;
      }
      if (!this.boardCell[6].Equals('X') && !this.boardCell[7].Equals('X') && !this.boardCell[8].Equals('X')) //678 - O или пусто
      {
        return false;
      }

      return true;
    }
  }
}
