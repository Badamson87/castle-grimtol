using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public bool fought = false;
    public bool win = false;
    public bool playing = true;

    public bool vaultBlown = false;

    public bool enemyAlive = false;
    int damage = 0;

    Enemy robot = new Enemy("robot", 150);

    public string choice = "";

    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    Player newPlayer = new Player("Stranger", 100);

    public void Setup()
    {

      //first, create all local variable (rooms and items) needed for gameplay
      Room platform = new Room("Platform", "A large open room with tracks running along the south wall. The only exit you see is to the east.");
      Room main = new Room("main", "You enter the main staging area for the plant taking note of an exit sign hung above the large door to the south, while doors to the north in west seem slightly less ominous. The bulk of the room is occupied by collections of crates. As you scan the room you think you catch a shimmer of light from atop the crate in the far corner.");
      Room storage = new Room("storage", "As you enter the room and instantly notice the loud buzz of the overhead lights. A single unoccupied desk sits in front of the east door, shelves on the far side of the room hold a flask.");
      Room bridge = new Room("bridge", "A suspended steel bridge stretches from east to west, The height makes you uneasy as a fall to the city below would be a poor way to die, but the obvious defense it adds to the vault is impressive.");
      Room vault = new Room("vault", "The large door behind you is the only way in or out. A giant reactor hums away as it slowly drains life from the earth.");
      Room exit = new Room("exit", "Stepping through the door peer out into the courtyard", true);
      Room train = new Room("train", "You decided the life of a mercanary is to much for you. You board the train in search of something safer as you ponder your life choices.");
      Room church = new Room("church", "church description");

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
      vault.Exits.Add("west", bridge);

      // create items
      Item bomb = new Item("bomb", "Your not to good with these things. Usually it would be a job for someone else.");
      Item crate = new Item("crate", "There are crates stacked throughout the room inside one you find a key");
      Item key = new Item("key", "Looks like a normaly key. You wonder who left it behind.");
      Item flask = new Item("flask", "Red liquid swirls inside");
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
        if (CurrentRoom.Exits[direction].LockedRoom == false)
        {
          CurrentRoom = CurrentRoom.Exits[direction];
          Console.WriteLine(CurrentRoom.Description);
        }
        else
        {
          Console.WriteLine("The door is locked.");
        }
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

    public void AttackRobot()
    {
      Random random = new Random();
      damage = random.Next(20, 30);
      if (robot.Health > 0)
      {
        Console.WriteLine($"You attack the robot for {damage} damge");
        robot.Health = robot.Health - damage;
        if (robot.Health > 0)
        {
          Attacked();
        }
        else
        {
          enemyAlive = false;
          Console.WriteLine("The robot is defeated and crumbles into a pile of parts");
        }
      }
      else
      {
        Console.WriteLine("You approach the machine to finsh it off, as you do so it explodes and send you flying off the bridge a poor way to die indead, closing your eyes you accept your fate.  You Awake to the sound of a ladys voice. It seems you crashed down through the roof of a dilapidated church coming to rest on a bed of flowers. She offer to take you back to her home and make you breakfast! You're happy to comply and soon after put your adventuring days behind you.");
        playing = false;
      }
    }
    public void Attacked()
    {
      Random random = new Random();
      damage = random.Next(20, 30);
      Console.WriteLine($"The robot attacks you for {damage} damge.");
      CurrentPlayer.Health = CurrentPlayer.Health - damage;
      if (CurrentPlayer.Health > 0)
      {
        Console.WriteLine($"Health remaing: {CurrentPlayer.Health}");
      }
      else
      {
        Console.WriteLine("You have fallen in combat");
        Again();
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
          robot.Health = 150;
          vaultBlown = false;
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
      CurrentPlayer.Health = 100;
      Setup();
      Console.WriteLine("The train pulls to a slow stop. Taking a quick moment to steal your nerves, you step out onto the platform. A large one armed man wastes no time approaching from the east. 'You want your pay merk, time to earn it.' As he extends his hand your eyes come to rest on the bomb and its rudimentary principles. 'Hurry the guard should be gone'. ");

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
        else if (CurrentRoom.Name == "exit")
        {
          if (vaultBlown == false)
          {
            Console.WriteLine("What have you gotten yourself into? You decided to get out now while you still can. As you move through the door a powerful hand grabs you from behind. 'No one skips out on me merk' As you feel the gun barrel press into your back, you know you made a mistake.");
            Again();
          }
          else
          {
            Console.WriteLine("You break through the exit and find yourself face to face with squad of armed guards. Defiently you unsheathe your sword. Shots ring through the air, instantly you're down by a hail of gunfire. What did you expect? You did bring a sword to a gun fight afterall.");
            Again();
          }
        }
        else if (CurrentRoom.Name == "bridge" && vaultBlown == true && enemyAlive == true)
        {
          Console.WriteLine("A large robot stands in the center of the bridge to block your way. It's poised to attack");
          GetUserInput();
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
      if (foundItem == null)
      {
        Console.WriteLine("You cant find that item");
        return;
      }
      if (foundItem.Name == "flask")
      {
        Item roomItem = CurrentRoom.Items.Find(i => i.Name == "crate");
        if (roomItem != null)
        {
          CurrentPlayer.Inventory.Add(foundItem);
          CurrentRoom.Items.Remove(foundItem);
          Console.WriteLine("Standing upon the crate, you got yee flask!");

        }
        else
        {
          Console.WriteLine("Why in the world can you not get yee flask?");
        }
      }

      else
      {
        Console.Clear();
        Console.WriteLine($"Adding the {itemName} to your pack");
        CurrentPlayer.Inventory.Add(foundItem);
        CurrentRoom.Items.Remove(foundItem);
      }
    }

    public void UseItem(string itemName)
    {
      itemName.ToLower();
      Item usedItem = CurrentPlayer.Inventory.Find(i => i.Name == itemName);
      if (usedItem == null)
      {
        Console.WriteLine("You dont have that");
        return;
      }
      if (itemName == "bomb")
      {

        if (CurrentRoom.Name != "vault")
        {
          Console.Clear();
          Console.WriteLine("You decide to take a quick peak at the bomb, while doing so you bump a wire and it explodes");
          Again();
          return;
        }
        else
        {
          Console.Clear();
          Console.WriteLine("You carefully place the bomb against the reactor. In the distance you hear a man yell. 'Someone has breached security sound the alarm!'");
          vaultBlown = true;
          enemyAlive = true;
          return;
        }
      }
      else if (itemName == "sword")
      {
        if (CurrentRoom.Name != "bridge")
        {
          Console.WriteLine("You swing your sword through the air but it seems unphased.");
        }
        else
        {
          if (vaultBlown == true)
          {
            AttackRobot();
          }
        }
      }
      else if (itemName == "crate")
      {
        CurrentPlayer.Inventory.Remove(usedItem);
        CurrentRoom.Items.Add(usedItem);
        Console.WriteLine($"You place the crate in the {CurrentRoom.Name} room.");
      }
      else if (itemName == "key")
      {
        if (CurrentRoom.Name == "main")
        {
          Console.WriteLine("You slide the key into the south door it's lock easily turns.");
          CurrentRoom.Exits["south"].LockedRoom = false;
        }
        else
        {
          Console.WriteLine("It doesn't seem to do anything");
        }
      }
      else if (itemName == "flask")
      {
        CurrentPlayer.Inventory.Remove(usedItem);
        Console.WriteLine("You chug down the flask and feel revitalized");
        CurrentPlayer.Health = 100;
      }
    }
  }
}