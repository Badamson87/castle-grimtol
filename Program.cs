using System;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Clear();
      GameService gameService = new GameService();
      Console.WriteLine("intro to the story");

      //make an instance of a player
      Player player = new Player("Stranger");

      //then pass the player to the gameService constructor when you create your instance of a GameService
      //
      gameService.StartGame(player);



    }
  }
}
