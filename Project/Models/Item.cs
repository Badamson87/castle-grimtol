using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Item : IItem
  {
    public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public string Description { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Item(string name, string description)
    {
      Name = name;
      Description = description;
    }
  }
}