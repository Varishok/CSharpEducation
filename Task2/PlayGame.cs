namespace Task2
{
  internal class PlayGame
  {
    static void Main(string[] args)
    {
      int cell;
      
      char newGame = 'д';
      while (newGame.Equals('д')) 
      { 
        var gameBoard = new GameBoard();
        Console.Write("Новая игра - КрестикиНолики.");
        gameBoard.DrawBoard();
        int turn = 0;
        int gameStatus = 0;
        while (turn < 9)
        {
          Console.Write("Введите номер ячейки для хода: ");
          cell = Console.ReadKey().KeyChar - '0';
          if (gameBoard.TurnPlayer(cell))
          {
            gameBoard.DrawBoard();
            turn++;
            gameStatus = gameBoard.CheckWinner();
            if(gameStatus != 0) 
            { 
              if(gameStatus == 1)
              {
                Console.WriteLine("Победили крестики.");
              }
              else
              {
                Console.WriteLine("Победили нолики");
              }
              gameBoard.DrawBoard();
              break;
            }
            if (gameBoard.IsDraw())
            {
              break;
            }
          }
          else
          {
            Console.WriteLine(" - неверный ход.");
          }
        }
        if (gameStatus == 0)
        {
          Console.WriteLine("Ничья.");
        }
        Console.Write("Игра завершена, хотите сыграть еще? (д/н) ");
        newGame = Console.ReadKey().KeyChar;
        Console.WriteLine();
      }
      Console.WriteLine("Хорошего дня!");
    }
  }
}