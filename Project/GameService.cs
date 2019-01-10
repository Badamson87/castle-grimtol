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

    Player newPlayer = new Player("Stranger");
    public string choice = "";


    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }


    public void Setup()
    {
      //first, create all local variable (rooms and items) needed for gameplay
      Room platform = new Room("Platform", "description for the platform");
      Room main = new Room("main", "Description for the main room");
      Room storage = new Room("storage", "description for the storage room");
      Room bridge = new Room("bridge", "description for the bridge");
      Room vault = new Room("vault", "description for the vault");
      Room exit = new Room("exit", "description for the exit");
      Room train = new Room("train", "you decided the life of a mercanary is to much for you. You board the train in search of something safer as you ponder your life choices.");
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


      // create items
      Item bomb = new Item("bomb", "description for the bomb");
      //   Item full = new Item("full", "description for boxs full of parts");
      Item crate = new Item("crate", "description for empty box");
      Item key = new Item("key", "description for the key card");
      Item flask = new Item("flask", "description for the flask");
      Item sword = new Item("sword", "Its still pretty sharp even if it is busted.");

      //second, add items to rooms and player
      platform.Items.Add(bomb);
      //   main.Items.Add(full);
      main.Items.Add(crate);
      main.Items.Add(key);
      storage.Items.Add(flask);
      CurrentPlayer.Inventory.Add(sword);



      CurrentRoom = platform;
    }


    public void GetUserInput()
    {
      string input = Console.ReadLine().ToLower();
      string[] action = input.Split(" ");
      if (action[0] == "go" && action.Length > 1)
      {
        Go(action[1]);
      }
      else if (action[0] == "use" && action.Length > 1)
      {
        UseItem(action[1]);
      }
      else if (action[0] == "take" && action.Length > 1)
      {
        TakeItem(action[1]);
      }
      else if (action[0] == "search" && action.Length > 1)
      {
        Search(action[1]);
      }
      else if (action[0] == "look")
      {
        Look();
      }
      else if (action[0] == "inventory")
      {
        Inventory();
      }
      else if (action[0] == "quit")
      {
        Quit();
      }
      else if (action[0] == "reset")
      {
        Reset();
      }
      else if (action[0] == "help")
      {
        Help();
      }
      else
      {
        Console.WriteLine("I am not so sure about that");
      }
    }





    public void Go(string direction)
    {
      Console.Clear();
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = CurrentRoom.Exits[direction];
        Console.WriteLine(CurrentRoom.Description);
      }
      else
      {
        Console.WriteLine("There is not an exit in that direction");
      }
    }

    public void Help()
    {
      Console.WriteLine("Avliable Commands are: go/take/use/look/search/quit/reset");
    }

    public void Inventory()
    {
      Console.Clear();
      Console.WriteLine("You check your pack and find");
      foreach (Item item in CurrentPlayer.Inventory)
      {
        Console.WriteLine(item.Name + " " + item.Description);
      }
    }

    public void Look()
    {
      Console.Clear();
      Console.WriteLine(CurrentRoom.Name);
      Console.WriteLine(CurrentRoom.Items);
      Console.WriteLine(CurrentRoom.Description);
    }

    public void Quit()
    {
      while (true)
      {
        Console.WriteLine("Are you sure you want to quit? y/n:");
        choice = Console.ReadLine();
        if (choice == "y")
        {
          playing = false;
          return;
        }
        else if (choice == "n")
        {
          Console.WriteLine(CurrentRoom.Description);
        }
      }
    }

    public void Reset()
    {
      while (true)
      {
        Console.WriteLine("Are you sure you want to reset?  y/n:");
        choice = Console.ReadLine();
        if (choice == "y")
        {
          Console.Clear();
          StartGame(newPlayer);
          return;
        }
        else if (choice == "n")
        {
          Console.Clear();
          Console.WriteLine(CurrentRoom.Description);
          return;
        }
      }
    }

    public void Again()
    {
      while (true)
      {
        Console.WriteLine("Your adventure comes to an end");
        Console.WriteLine("play again y/n : ");
        choice = Console.ReadLine();
        if (choice == "y")
        {
          Console.Clear();
          StartGame(newPlayer);
          return;
        }
        else if (choice == "n")
        {
          Quit();
          return;
        }
      }
    }

    public void Search(string itemName)
    {
      itemName.ToLower();
      Item searchedItem = CurrentRoom.Items.Find(i => i.Name == itemName);
      if (searchedItem != null)
      {
        Console.WriteLine(searchedItem.Description);
      }
    }
    public void StartGame(Player player)
    {
      CurrentPlayer = player;
      CurrentPlayer.Inventory = new List<Item>();
      Setup();
      Console.WriteLine("you find yourself on a platform approached by a one armed man. He reaches towards you holding a bomb in his hand");

      while (playing)
      {
        if (CurrentRoom.Name == "church")
        {
          Console.WriteLine("Description for winning");
          win = true;
          break;
        }
        else if (CurrentRoom.Name == "train")
        {
          Again();
        }
        else
        {
          Console.WriteLine("What do you want to do");
          GetUserInput();
        }


      }
    }

    public void TakeItem(string itemName)
    {
      itemName.ToLower();
      Item foundItem = CurrentRoom.Items.Find(i => i.Name == itemName);
      if (foundItem != null)
      {
        Console.WriteLine($"Adding the {itemName} to your pack");
        CurrentPlayer.Inventory.Add(foundItem);
        CurrentRoom.Items.Remove(foundItem);
      }
      else
      {
        Console.WriteLine("You cant find that item");
      }

    }

    public void UseItem(string itemName)
    {

      Item usedItem = CurrentPlayer.Inventory.Find(i => i.Name == itemName);
      if (usedItem == null)
      {
        Console.WriteLine("You dont have that");
      }


      if (itemName == "bomb")
      {

        if (CurrentRoom.Name != "vault")
        {
          Console.WriteLine("You decide to take a quick peak at the bomb, while doing so you bump a wire and it explodes");
          Again();
          return;
        }
        else
        {
          Console.WriteLine("You carefully place the bomb against the vault door");
          return;
        }
      }
      else if (itemName == "sword")
      {
        if (CurrentRoom.Name != "bridge")
        {
          Console.WriteLine("You swing your sword through the air");
        }
        else
        {
          // attackfunction for the sword
        }
      }
      else if (itemName == "crate")
      {
        CurrentPlayer.Inventory.Remove(usedItem);
        CurrentRoom.Items.Add(usedItem);
        Console.WriteLine("You Place the crate in the room");
      }


    }


  }
}