using MONOPOLY;
using System.IO;

class Game
{
    List<Player> players = new List<Player>();
    Player player;
    public static Die die = new Die();
    board board;
    int numPlayers = 2;
    public static API api;

    public static CardInit gameDeck = new CardInit();
    public static CardInit GameDeck { get => gameDeck; }
    PropertysInit gameProperty = new PropertysInit();
    public static Tile[] tiles;
    List<Card> Cards= new List<Card>();
    Deck[] decks = new Deck[2];


    public Game(string[] names)
    {
        gameDeck.Init();
        tiles = gameProperty.Init();
        Cards.AddRange(gameDeck.ChanceDeck.m_deck);
        Cards.AddRange(gameDeck.ChestDeck.m_deck);
        decks[0] = gameDeck.ChanceDeck;
        decks[1] = gameDeck.ChestDeck;


        api = new API(this.GetPlayers(), this.GetTiles(), this.GetBoard());
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



    public Tile[] GetTiles()
    {
        return tiles;
    }
    public board GetBoard()
    {
        return board;
    }
    public Player GetPlayer(int player)
    {
        return players[player];
    }
    public List<Player> GetPlayers()
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
    public bool HasAttr(Tile expando, string key)
    {
        return ((IDictionary<string, Object>)expando).ContainsKey(key);
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
            int topRightX = Console.WindowWidth - players[0].Name.Length;
            Console.SetCursorPosition(topRightX, 0);
            Console.Write(players[0].Name);
            if (currentPlayer.GetBankrupt())//if player is bankkrupt move to next player
            {
                    int playerNum = NextPlayer(currentPlayer);
                    currentPlayer = players[playerNum];
            }
            else
            {
                die.roll();
                api.Move(currentPlayer,die.GetTotal(), tiles);
                tiles[currentPlayer.GetBoardPos()].TakeOrder(currentPlayer);
                tiles[currentPlayer.GetBoardPos()] = new BackGround(tiles[currentPlayer.GetBoardPos()]);
                Console.Write(((BackGround)tiles[currentPlayer.GetBoardPos()]).GetName());
            }
            Console.WriteLine();
            //Thread.Sleep(2000);
            Console.ReadKey();
            if ((die.GetDoubleCount() == 0))
            {
               currentPlayer = players[NextPlayer(currentPlayer)];

            }
            if (die.GetDoubleCount()==3)
            {
                api.sendToJail(currentPlayer);
            }
            }
            int WinningPlayer = 0;

            foreach (var player in players)
            {
                if (!player.GetBankrupt())
                    WinningPlayer = Index(player);

            }
            Console.Write("The Winner is: Player " + (WinningPlayer).ToString());

    }

    public static void Main(string[] args)
    {
        string[] names = new string[] { "Тимур", "Андрей" };
        Game game = new Game(names);
        game.RunGame();
        
    }
}
