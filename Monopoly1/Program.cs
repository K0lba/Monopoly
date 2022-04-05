using MONOPOLY;
using System.IO;

class Game
{
    List<Player> players = new List<Player>();
    public static Die die = new Die();
    int numPlayers = 2;
    public static API api;

    public static Tile[] tiles;
    public static Card[] Cards;
    private int Y = 4;


    public Game(string[] names)
    {
        tiles = Init();
        Cards = CardInit();


        api = new API(this.GetPlayers(), this.GetTiles());
        void CreateGame(int num)
        {
            for (int i = 0; i < num; i++)
            {
                Player player1 = new Player(i,names[i]);
                players.Add(player1);
            }
        }
        CreateGame(numPlayers);

    }
    private Tile[] Init()
    {
        Tile[] tiles = { new Start("Go", 0),
            new Property("Old Kent Road", 1, 60, new int[] { 2, 10, 30, 90, 250 }, 50, "brown"),
            new Chance("Community Chest",2),
            new Property("Whitechapel",  3, 60, new int[] { 4, 20, 60, 180, 450 }, 50, "brown"),
            new Property("Kings Cross Station", 4, 200, new int[] { 25, 2 }, 0, "station"),
            new Property("The Angel Islington", 5, 100, new int[] { 6, 30, 90, 270, 550 }, 50, "lblue"),
            new Chance("Chance", 6),
            new Property("Euston Road", 7, 100, new int[] { 6, 30, 90, 270, 550 }, 50, "lblue"),
            new Property("Pentonville Road", 8, 120, new int[] { 8, 40, 100, 300, 600 }, 50, "lblue"),
            new Jail("Jail", 9),
            new Property("Pall Mall", 10, 140, new int[] { 10, 50, 150, 450, 750 }, 100, "pink"),
            new Property("Electric Company", 11, 150, new int[] { 4, 10 }, 0, "utility"),
            new Property("Whitehall",  12, 140, new int[] { 10, 50, 150, 450, 750 }, 100, "pink"),
            new Property("Northumberland", 13, 160, new int[] { 12, 60, 180, 500, 900 }, 100, "pink"),
            new Property("Marylebone Station", 14, 200, new int[] { 25, 2 }, 0, "station"),
            new Property("Bow Street", 15, 180, new int[] { 14, 70, 200, 550, 950 }, 100, "orange"),
            new Property("Marlborough Street", 16, 180, new int[] { 14, 70, 200, 550, 950 }, 100, "orange"),
            new Property("Vine Street", 17, 200, new int[] { 16, 80, 220, 600, 1000 }, 100, "orange"),
            new Start("Free Parking",18),
            new Property("Strand", 19, 220, new int[] { 18, 90, 250, 700, 1050 }, 150, "red"),
            new Property("Fleet Street", 20, 220, new int[] { 18, 90, 250, 700, 1050 }, 150, "red"),
            new Property("Trafalgar Square", 21, 240, new int[] { 20, 100, 300, 750, 1100 }, 150, "red"),
            new Property("Fenchurch St Station", 22, 200, new int[] { 25, 2 }, 0, "station"),
            new Property("Water Works",  23, 150, new int[] { 4, 10 }, 0, "utility"),
            new GoToJail("Go To Jail",24),
            new Chance("Community Chest", 25),
            new Property("Liverpool Street Station",26,200,new int[] { 25, 2 },0,"station"),
            new Chance("Chance",  27),
        };

        return tiles;
    }
    private Card[] CardInit()
    {
       Card[] Deck = {

                new Card( "GoToJail", "go to jail", "sendToJail(player)"),
                new Card("GoBackThreeSpaces", "move three cells back", "movePlayer(player,-3)"),
                 new Card("MakeRepairs", "make general repairs on all of his houses", "makeRepairs(player,25,100)"),
                new Card("PayForSchool", "pay school fees of 150", "deductCash(player,150)"),
                new Card("DrunkInCharge", "get fine in the amount of 20 for drunkenness", "deductCash(player,20)"),
                new Card("SpeedingFine", "get fine in the amount of 15 for speeding", "deductCash(player,15)"),
                new Card("BuildingLoan", "receive 150 for a loan matures", "giveCash(player,150)"),
                new Card("WonCompetition", "win a crossword competition and collect 100", "giveCash(player,150)"),
                new Card("GetOutOfJail", "dug a tunnel and escaped jail", "getOutOfJailFree()"),

                 new Card("PayHospitalLose", "pay hospital fees, lose 100", "deductCash(player,100)"),
                new Card("WayPedest", "did not give way to a pedestrian, fine 50", "deductCash(player,50)"),
                new Card("BankError", "bank error in your favour, gain 200", "giveCash(player,200)"),
                new Card("Inherit", "inherit 100", "giveCash(player,100)"),
                new Card("Sale", "from sale of stock  get 50", "giveCash(player,50"),
                 new Card("Birthday", "have birthday, collect 10 from each player", "birthdayCash(player,10)"),
                new Card("Demonstration", "spent the night in jail and got a fine", "deductCash(player,10)"),
        };
        return Deck;

    }
    private bool inJail(Player player)
    {
        if (player.jailTurns != 0)
        {
            if (player.HasGetOutOfJail())
            {
                player.UseGetOutOfJail();
                return false;
            }
            player.SetJailTurns(player.jailTurns - 1);
            return true;
        }
        return false;
    }
    private void Move(Player player, int moves, Tile[] tiles)
    {
        if (inJail(player))
        {
            Console.Write(player.Name + " is in jail");
            return;
        }
        int pos = player.BoarPos;
        while (moves != 0)
        {
            if ((pos + 1) == tiles.Length)
            {
                pos = 0;
                Console.WriteLine(player.Name + " has passed Go and take 200");
                player.GainCash(200);
            }
            else
            {
                pos++;
            }
            moves--;
        }
        player.SetBoardPos(pos);
        Console.Write(player.Name + " становится на позицию " + tiles[pos].Name);

    }


    private Tile[] GetTiles()
    {
        return tiles;
    }
    private List<Player> GetPlayers()
    {
        return players;
    }
    
    private int Index(Player playerr)
    {
        int index = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == playerr)
            {
                return index;
            }
            index++;
        }
        return -1;
    }
    
    private void ClearLine(int line)
    {
         Console.MoveBufferArea(0, line, Console.BufferWidth, 1, Console.BufferWidth, line, ' ', Console.ForegroundColor, Console.BackgroundColor);
    }
    private void DrawConsole()
    {
        int topRightX = Console.WindowWidth - players[0].Name.Length-30;
        ClearLine(1);

        Console.SetCursorPosition(topRightX, 0);
        Console.WriteLine(players[0].Name);
        Console.SetCursorPosition(topRightX, 1);
        Console.WriteLine(players[0].Balance);

        Console.SetCursorPosition(0, 0);
        Console.WriteLine(players[1].Name);
        Console.SetCursorPosition(0, 1);
        Console.WriteLine(players[1].Balance);
    }
    public static void UpgradeProperty(Tile property)
    {
        var newProperty = property switch
        {
            Monopoly => new FirstHouse((Property)property),
            FirstHouse => new SecondHouse((Property)property),
            SecondHouse => new ThirdHouse((Property)property),
            ThirdHouse => new Hotel((Property)property),
            _=>property
        };
        if(newProperty is Property)
        tiles[property.BoardPos]= newProperty;
        Console.WriteLine(((Property)newProperty).owner.Name+" улучшает до "+ newProperty.Name);
        
    }
    public static bool CanUpgradeProperty(Property proper, Player currentPlayer)
    {
        var suitableProperty = currentPlayer.OwenedProperties.Where(property => property is Monopoly prop
        && prop.level >= proper.level && prop.Group == proper.Group).ToArray();
        return suitableProperty.Length ==3 && currentPlayer.Balance >= proper.HouseCost;
    }
    public static void CanUpgradeMonopoly(Player currentPlayer, string group)
    {
        List<Property> properties = new List<Property>();
        foreach(var property in currentPlayer.OwenedProperties)
        {
            if(group == property.Group)
            {
                properties.Add(property);
            }    
        }
        if (properties.Count == 3)
        {
            foreach(var prop in properties)
            {
                tiles[prop.BoardPos] = new Monopoly(prop);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(currentPlayer.Name + "улучшает" + tiles[prop.BoardPos].Name);
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }
    }

    public void RunGame()
    { 
        Player currentPlayer = players[DecideTurnOrder(players.Count, die)];
        int DecideTurnOrder(int players, Die dice)
        {
            int first = 0;
            int highestRoll = 0;
            for (int i = 0; i < players; i++)
            {
                dice.roll();
                int roll = dice.GetTotal();
                if (highestRoll < roll)
                {
                    highestRoll = roll;
                    first = i;
                }
            }
            return first;
        }
        bool isGameOver(List<Player> plyers)
        {
            int bankrupt = 0;
            for (int i = 0; i < plyers.Count; i++)
            {
                if (players[i].IsBankrupt)
                {
                    bankrupt++;
                }
            }
            if (bankrupt == players.Count - 1)
            {
                return true;
            }
            return false;
        }
        int NextPlayer(Player player)
            {
                int nplayer = Index(player) + 1;
                if (nplayer == players.Count)
                {
                    nplayer = 0;
                }
                return nplayer;
            }
            
        
        
        
       
       
        while (!(isGameOver(players)))//while the game is not over
        {
            
            DrawConsole();
            
            if (currentPlayer.IsBankrupt)//if player is bankkrupt move to next player
            {
                    int playerNum = NextPlayer(currentPlayer);
                    currentPlayer = players[playerNum];
            }
            else
            {
                Console.SetCursorPosition(0, Y);
                if (Y < Console.WindowHeight-2)
                {
                    Y ++;
                }
                else
                {
                    Console.Clear();
                    Y = 4;
                    DrawConsole();
                    Console.SetCursorPosition(0,Y);
                    
                }
                die.roll();
                Move(currentPlayer,die.GetTotal(), tiles);
                tiles[currentPlayer.BoarPos].TakeTurn(currentPlayer);

            }
            Console.WriteLine();
            Thread.Sleep(100);
            if ((die.GetDoubleCount() == 0))
            {
                currentPlayer = players[NextPlayer(currentPlayer)];
            }
            if (die.GetDoubleCount()==3)
            {
                api.sendToJail(currentPlayer);
            }
            
        }
            string WinningPlayer="";

            foreach (var player in players)
            {
                if (!player.IsBankrupt)
                    WinningPlayer = player.Name;

            }
            Console.Write("The Winner is: Player " + WinningPlayer);
            
    }

    public static void Main(string[] args)
    {
        string[] names = new string[] { "Тимур", "Андрей" };
        Game game = new Game(names);
        game.RunGame();
        
    }
}
