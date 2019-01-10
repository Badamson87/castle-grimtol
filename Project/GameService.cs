using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public bool win = false;
    public bool playing = true;


    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    public void GetUserInput()
    {
      string input = Console.ReadLine().ToLower();
      string action = input.Split(" ");


    }

    public void Go(string direction)
    {

    }

    public void Help()
    {

    }

    public void Inventory()
    {

    }

    public void Look()
    {

    }

    public void Quit()
    {

    }

    public void Reset()
    {

    }

    public void Setup()
    {
      //first, create all local variable (rooms and items) needed for gameplay
      Room platform = new Room("Platform", "description");
      Room main = new Room("main", "Description");
      Room north = new Room("north", "description");
      Room bridge = new Room("bridge", "description");
      Room vault = new Room("vault", "des");
      Room exit = new Room("exit", "description");
      Room train = new Room("train", "des");
      Room church = new Room("church", "description");

      // add exits to room

      platform.Exits.Add("train", train);
      platform.Exits.Add("main", main);
      main.Exits.Add("north", north);
      main.Exits.Add("exit", exit);
      main.Exits.Add("platform", platform);
      north.Exits.Add("bridge", bridge);
      north.Exits.Add("main", main);
      bridge.Exits.Add("vault", vault);
      bridge.Exits.Add("north", north);
      bridge.Exits.Add("church", church);// add from when monster
      vault.Exits.Add("bridge", bridge);



      //second, add items to rooms and player




      CurrentRoom = platform;
    }

    public void StartGame(Player player)
    {
      CurrentPlayer = player;
      CurrentPlayer.Inventory = new List<Item>();
      Setup();
      Console.WriteLine("intro to the story");

      while (playing)
      {
        if (CurrentRoom.Name == "church")
        {
          Console.WriteLine("Description for winning");
          win = true;
          break;
        }
        Console.WriteLine("accept bomb");

      }




    }

    public void TakeItem(string itemName)
    {

    }

    public void UseItem(string itemName)
    {

    }


  }
}