﻿using MONOPOLY;
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
                Player player1 = new Player(i, "risky",names[i]);
                players.Add(player1);
            }
        }



        CreateGame(numPlayers);

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
    private bool HasAttr(Tile expando, string key)
    {
        return ((IDictionary<string, Object>)expando).ContainsKey(key);
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
        Console.WriteLine(players[0].GetName());
        Console.SetCursorPosition(topRightX, 1);
        Console.WriteLine(players[0].GetCash());

        Console.SetCursorPosition(0, 0);
        Console.WriteLine(players[1].GetName());
        Console.SetCursorPosition(0, 1);
        Console.WriteLine(players[1].GetCash());
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
        tiles[property.GetBoardPos()]= newProperty;
        if (newProperty is not Station) Console.WriteLine(((Property)newProperty).owner.GetName()+" улучшает до "+ newProperty.GetName());

    }
    public static bool CanUpgradeProperty(Property proper, Player currentPlayer)
    {
        var suitableProperty = currentPlayer.GetOwenedPropertys().Where(property => property is Property prop
        && prop.level >= proper.level && prop.Group == proper.Group).ToArray();
        return suitableProperty.Length ==3 && currentPlayer.GetCash() >= proper.GetHouseCost();
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
                if (players[i].GetBankrupt())
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
            
        Tile currentTile = tiles[currentPlayer.GetBoardPos()];
        
        
        /*bool CanAfford(Player player, double cost)
            {
                if (cost + api.getHighestRent(player) < player.GetCash())
                {
                    return true;
                }
                return false;
            }*/
        /*List<dynamic> isBuyingHouses(Player player)
            {
                List<dynamic> list = new List<dynamic>();
                List<Property> playersProperties = player.GetOwenedPropertys();
                int lowestCost = 9999;

                Property cheapestHouseProperty = null;

                foreach (var property in playersProperties)
                {
                    if (api.inMonopoly(property, player))
                    {
                        if (property.GetHouseCost() < lowestCost && property.GetNumHouses() < 5 && api.checkIfEvenBuild(property, player) && board.GetAvailableHouses() > 0)
                        {

                            lowestCost = property.GetHouseCost();

                            cheapestHouseProperty = property;

                        }
                    }
                }
                if (CanAfford(player, lowestCost) && !(cheapestHouseProperty == null))
                {
                    list.Add(true);
                    list.Add(cheapestHouseProperty);
                    return list;
                }


                else
                {
                    list.Add(false);
                    return list;
                }


            }*/
        /*void sellForCash(Player player)
            {
                List<Property> properties = player.GetOwenedPropertys();
                int lowestCost = 9999;
                Property selling = null;
                bool sellImprovments = false;
                foreach (var property in properties)
                {
                    if ((property.GetBuyValue() < lowestCost) && property is Utility && !(property.GetIsMorgaged()))
                    {
                        lowestCost = property.GetBuyValue();
                        selling = property;
                    }
                }
                if (lowestCost == 9999)
                {
                    foreach (var property in properties)
                    {
                        if ((property.GetBuyValue() < lowestCost) && (!(api.inMonopoly(property, player))) && !(property.GetIsMorgaged()))
                        {
                            lowestCost = property.GetBuyValue();
                            selling = property;
                        }
                    }
                }
                if (lowestCost == 9999)
                {
                    foreach (var property in properties)
                    {
                        if ((property.GetBuyValue() < lowestCost) && (!(property.GetIsMorgaged())) && (api.GetNumHouses(player, property.GetGroup()) == 0))
                        {
                            lowestCost = property.GetBuyValue();
                            selling = property;
                        }
                    }
                }
                if (lowestCost == 9999)
                {
                    foreach (var property in properties)
                    {
                        if (HasAttr(property, "GetHouseCost"))
                        {
                            if ((property.GetHouseCost() < lowestCost) && !(property.GetIsMorgaged()) && property.GetNumHouses() > 0 && api.checkIfEvenBuild(property, player, false))
                            {
                                sellImprovments = true;
                                lowestCost = property.GetHouseCost();
                                selling = property;
                            }

                        }

                    }
                }
                if (!(selling == null))
                {
                    if (sellImprovments == true)
                    {
                        api.sellHouses(player, selling);
                        Console.Write("player " + (player.GetRollOrder()).ToString() + " sells imporvements on " + (selling.GetName()).ToString());
                    }

                    else if (sellImprovments == false)
                    {
                        api.morgageProperty(player, selling);


                        Console.Write("player " + (player.GetRollOrder()), ToString() + " morgages " + (selling.GetName()).ToString());
                    }
                }
            }*/
       
        while (!(isGameOver(players)))//while the game is not over
        {
            
            DrawConsole();
            
            if (currentPlayer.GetBankrupt())//if player is bankkrupt move to next player
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
                    DrawConsole();
                    Y = 4;
                }
                die.roll();
                api.Move(currentPlayer,die.GetTotal(), tiles);
                tiles[currentPlayer.GetBoardPos()].TakeOrder(currentPlayer);

            }
            Console.WriteLine();
            Thread.Sleep(1000);
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
                if (!player.GetBankrupt())
                    WinningPlayer = player.GetName();

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
