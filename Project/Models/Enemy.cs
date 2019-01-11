using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Enemy : IEnemy
  {
    public string EnemyName { get; set; }
    public int Health { get; set; }

    public Enemy(string enemyName, int health)
    {
      EnemyName = enemyName;
      Health = health;
    }
  }
}