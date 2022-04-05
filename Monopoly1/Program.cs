using MONOPOLY;
using System.IO;

class Game
{
    List<Player> players = new List<Player>();
    Player player;
    public static Die die = new Die();
    int numPlayers = 2;
    public static API api;

    public static CardInit gameDeck = new CardInit();
    public static CardInit GameDeck { get => gameDeck; }
    PropertysInit gameProperty = new PropertysInit();
    public static Tile[] tiles;
    List<Card> Cards= new List<Card>();
    Deck[] decks = new Deck[2];

    private int Y = 4;


    public Game(string[] names)
    {
        gameDeck.Init();
        tiles = gameProperty.Init();
        Cards.AddRange(gameDeck.ChanceDeck.m_deck);
        Cards.AddRange(gameDeck.ChestDeck.m_deck);
        decks[0] = gameDeck.ChanceDeck;
        decks[1] = gameDeck.ChestDeck;


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
    public bool inJail(Player player)
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
    
    public int Index(Player playerr)
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
        return suitableProperty.Length ==3 && currentPlayer.Balance >= proper.GetHouseCost();
    }
    public static void CanUpgradeMonopoly(Player currentPlayer, string group)
    {
        List<Property> properties = new List<Property>();
        foreach(var property in currentPlayer.OwenedProperties)
        {
            if(group == property.GetGroup())
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
