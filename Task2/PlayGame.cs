namespace Task2
{
  internal class PlayGame
  {
    static void Main(string[] args)
    {
      int cell;
      char player;
      
      char newGame = 'д';
      while (newGame.Equals('д')) 
      { 
        var gameBoard = new GameBoard();
        Console.Write("Новая игра - КрестикиНолики.");

        gameBoard.DrawBoard();

        int turn = 0;
        GameBoard.GameStatus gameStatus = GameBoard.GameStatus.InProgress;
        player = GameBoard.firstPlayer;

        while (turn < 9)
        {
          Console.Write("Введите номер ячейки для хода: ");
          cell = Console.ReadKey().KeyChar - '0';

          if (gameBoard.TurnPlayer(cell, player))
          {
            gameBoard.DrawBoard();
            turn++;

            gameStatus = gameBoard.CheckWinner();
            if(gameStatus != GameBoard.GameStatus.InProgress) 
            { 
              if(gameStatus == GameBoard.GameStatus.XWin)
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

            gameStatus = gameBoard.IsDraw();
            if (gameStatus == GameBoard.GameStatus.Draw)
            {
              break;
            }

            if(player.Equals(GameBoard.firstPlayer))
            {
              player = GameBoard.secondPlayer;
            }
            else
            {
              player = GameBoard.firstPlayer;
            }
          }
          else
          {
            Console.WriteLine(" - неверный ход.");
          }
        }
        if (gameStatus == GameBoard.GameStatus.Draw)
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