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
      string[] action = input.Split(" ");
      if (action[0] == "go" || action.Length > 1)
      {
        Go(action[1]);
      }


    }

    public void Go(string direction)
    {
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = CurrentRoom.Exits[direction];
        Console.WriteLine(CurrentRoom.Description);
        GetUserInput();
      }
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
      Room platform = new Room("Platform", "description for the platform");
      Room main = new Room("main", "Description for the main room");
      Room storage = new Room("storage", "description for the storage room");
      Room bridge = new Room("bridge", "description for the bridge");
      Room vault = new Room("vault", "description for the vault");
      Room exit = new Room("exit", "description for the exit");
      Room train = new Room("train", "desciption for the train");
      Room church = new Room("church", "description for the church");

      // add exits to room

      platform.Exits.Add("south", train);
      platform.Exits.Add("east", main);
      main.Exits.Add("north", storage);
      main.Exits.Add("south", exit);
      main.Exits.Add("west", platform);
      storage.Exits.Add("east", bridge);
      storage.Exits.Add("south", main);
      bridge.Exits.Add("east", vault);
      bridge.Exits.Add("west", storage);
      //   bridge.Exits.Add("church", church);// add from when monster
      vault.Exits.Add("west", bridge);



      //second, add items to rooms and player




      CurrentRoom = platform;
    }

    public void StartGame(Player player)
    {
      CurrentPlayer = player;
      CurrentPlayer.Inventory = new List<Item>();
      Setup();
      Console.WriteLine("If find yourself on a platform approached by a one armed man he beckons you to follow him and then turns and runs away");

      while (playing)
      {
        if (CurrentRoom.Name == "church")
        {
          Console.WriteLine("Description for winning");
          win = true;
          break;
        }
        Console.WriteLine("What do you want to do");
        GetUserInput();

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