using System.Collections.Generic;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project.Interfaces
{
  public interface IEnemy
  {
    string EnemyName { get; set; }
    int Health { get; set; }
  }
}