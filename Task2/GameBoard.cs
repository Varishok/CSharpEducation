using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
  /// <summary>
  /// Игра крестики-нолики.
  /// </summary>
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
    /// Символ первого игрока. (X)
    /// </summary>
    private const char firstPlayer = 'X';

    /// <summary>
    /// Символ второго игрока. (O)
    /// </summary>
    private const char secondPlayer = 'O';

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
      if(cellValue == firstPlayer || cellValue == secondPlayer)
      {
        return false;
      }

      if (this.isFirst)
      {
        this.boardCell[cell - 1] = firstPlayer;
        this.cellColor[cell - 1] = ConsoleColor.Red;
      }
      else
      {
        this.boardCell[cell - 1] = secondPlayer;
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
        if (this.boardCell[cellIndex].Equals(firstPlayer))
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
      if(!this.boardCell[0].Equals(secondPlayer) && !this.boardCell[1].Equals(secondPlayer) && !this.boardCell[2].Equals(secondPlayer) || //012 - X или пусто
         !this.boardCell[0].Equals(secondPlayer) && !this.boardCell[3].Equals(secondPlayer) && !this.boardCell[6].Equals(secondPlayer) || //036 - X или пусто
         !this.boardCell[0].Equals(secondPlayer) && !this.boardCell[4].Equals(secondPlayer) && !this.boardCell[8].Equals(secondPlayer) || //048 - X или пусто
         !this.boardCell[1].Equals(secondPlayer) && !this.boardCell[4].Equals(secondPlayer) && !this.boardCell[7].Equals(secondPlayer) || //147 - X или пусто
         !this.boardCell[2].Equals(secondPlayer) && !this.boardCell[4].Equals(secondPlayer) && !this.boardCell[6].Equals(secondPlayer) || //246 - X или пусто
         !this.boardCell[2].Equals(secondPlayer) && !this.boardCell[5].Equals(secondPlayer) && !this.boardCell[8].Equals(secondPlayer) || //258 - X или пусто
         !this.boardCell[3].Equals(secondPlayer) && !this.boardCell[4].Equals(secondPlayer) && !this.boardCell[5].Equals(secondPlayer) || //345 - X или пусто
         !this.boardCell[6].Equals(secondPlayer) && !this.boardCell[7].Equals(secondPlayer) && !this.boardCell[8].Equals(secondPlayer) || //678 - X или пусто
         !this.boardCell[0].Equals(firstPlayer) && !this.boardCell[1].Equals(firstPlayer) && !this.boardCell[2].Equals(firstPlayer) || //012 - O или пусто
         !this.boardCell[0].Equals(firstPlayer) && !this.boardCell[3].Equals(firstPlayer) && !this.boardCell[6].Equals(firstPlayer) || //036 - O или пусто
         !this.boardCell[0].Equals(firstPlayer) && !this.boardCell[4].Equals(firstPlayer) && !this.boardCell[8].Equals(firstPlayer) || //048 - O или пусто
         !this.boardCell[1].Equals(firstPlayer) && !this.boardCell[4].Equals(firstPlayer) && !this.boardCell[7].Equals(firstPlayer) || //147 - O или пусто
         !this.boardCell[2].Equals(firstPlayer) && !this.boardCell[4].Equals(firstPlayer) && !this.boardCell[6].Equals(firstPlayer) || //246 - O или пусто
         !this.boardCell[2].Equals(firstPlayer) && !this.boardCell[5].Equals(firstPlayer) && !this.boardCell[8].Equals(firstPlayer) || //258 - O или пусто
         !this.boardCell[3].Equals(firstPlayer) && !this.boardCell[4].Equals(firstPlayer) && !this.boardCell[5].Equals(firstPlayer) || //345 - O или пусто
         !this.boardCell[6].Equals(firstPlayer) && !this.boardCell[7].Equals(firstPlayer) && !this.boardCell[8].Equals(firstPlayer))   //678 - O или пусто
      {
        return false;
      }

      return true;
    }
  }
}
