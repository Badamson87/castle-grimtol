using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }

    public int Health { get; set; }

    public Player(string playerName, int health)
    {
      PlayerName = playerName;
      Inventory = new List<Item>();
      Health = Health;
    }
  }
}