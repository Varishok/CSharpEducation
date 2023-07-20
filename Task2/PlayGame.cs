namespace Task2
{
  internal class PlayGame
  {
    static void Main(string[] args)
    {
      int cell;
      var gameBoard = new GameBoard();
      while (true)
      {
        cell = Console.ReadKey().KeyChar - '0';
        if(gameBoard.TurnPlayer(cell))
        {
          gameBoard.DrawBoard();
        } 
        else
        {
          Console.WriteLine(" - неверный ход.");
        }
      }
    }
  }
}