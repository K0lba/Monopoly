using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{
    internal class API
    {
        List<Player> players= new List<Player>();
        Tile[] tiles;
        


        public  API(List<Player> player, Tile[] tile)
        {
            this.players=player;
            this.tiles=tile;
            

        }
        public void movePlayer(Player player, int move)
        {
            player.SetBoardPos(player.GetBoardPos()+move);
            int maxTile = tiles.Length;
            if(player.GetBoardPos() >= maxTile)
            {
                player.SetBoardPos(player.GetBoardPos()-maxTile);
            }

        }
        public void birthdayCash(Player player, int amountperplayer)
        {
            int count = 0;
            for(int i=0; i<players.Count; i++)
            {
                players[i].LoseCash(amountperplayer);
                count++;

            }
            player.GainCash(amountperplayer * count);

        }
        public int  rollDice(Player player, Die dice)
        {
            dice.roll();
            int roll = dice.GetTotal();
            if (dice.GetDoubleCount() == dice.GetJailRoll())
            {
                sendToJail(player);
                dice.SetDoubleCount(0);
            }
            return roll;
        }
        public void sendToJail(Player player)
        {
            player.SetJailTurns(3);
            for(int i=0; i<tiles.Length; i++)
            {
                if (tiles[i] is Jail)
                {
                    player.SetBoardPos(tiles[i].GetBoardPos());
                    break;
                }
            }
        }
        public void getOutOfJail(Player player)
        {
            player.SetJailTurns(0);
        }
        public bool tryOutOfJail(Player player, Die dice)
        {
            dice.roll();
            int roll = dice.GetTotal();
            if (dice.GetDoubleCount() == 1)
            {
                dice.SetDoubleCount(0);
                return true;
            }

            return false;
        }
        public void buyProperty(Player player, Property property)
        {
            player.LoseCash(property.GetBuyValue());
            player.AddNewProperty(property);
            property.SetOwner(player);
        }
        public void payRent(Player player, Player player2, Property property, int multiplier=1)
        {
            player.LoseCash(multiplier * property.GetRent());
            player2.GainCash(multiplier * property.GetRent());
            property.AddEarnings(multiplier * property.GetRent());
        }
        public void deductCash(Player player, double amount)
        {
            player.LoseCash(amount);
        }
        public void giveCash(Player player, double ammount)
        {
            player.GainCash(ammount);
        }
        /*public void improveProperty(Player player, Property prop)
        {
            if (board.GetAvailableHouses() > 0)
            {
                prop.SetNumHouses(prop.GetNumHouses() + 1);
                player.LoseCash(prop.GetHouseCost());
                prop.DeductEarnings(prop.GetHouseCost());
                board.DecAvailableHouses();
            }
        }*/
        /*public void sellHouses(Player player, Property prop)
        {
            prop.SetNumHouses(prop.GetNumHouses() - 1);
            player.GainCash(prop.GetHouseCost() * board.GetHousesSellPercent());
            prop.AddEarnings(prop.GetHouseCost() * board.GetHousesSellPercent());
        }*/
        
        public bool isBankrupt(Player player, int toPay = 0)
        {

            bool bankrupt = true;
            List<Property> ownedPropertys = player.GetOwenedPropertys();
		    if (ownedPropertys!=null) 
            {
                foreach(var property in ownedPropertys)
                {
                    if (property.GetIsMorgaged() == false)
                    {
                        bankrupt = false; 

                    }
					

                }
				
            }
			
		    if(player.GetCash() - toPay >= 0) { bankrupt = false; }


            return bankrupt;
        }
        public bool checkEvenBuild(string group, Player player) 
        {
            List<Property> owned = player.GetOwenedPropertys();
            List<Property> groupProperties = new List<Property>();
		    foreach(var property in owned)
            {
                if (property.GetGroup() == group)
                {
                    groupProperties.Add(property);
                }	
            }

            int compariter = groupProperties[0].GetNumHouses();

            bool even = false;
		    foreach(var property in groupProperties)
            {
                if ((property.GetNumHouses() - 1)== compariter || (property.GetNumHouses() + 1) == compariter){even = true; }

                else { even = false; }
            }
            return even;
        }
        public bool checkIfEvenBuild(Property buildProperty, Player player, bool buying = true)
        {
            List<Property> owned = player.GetOwenedPropertys();
            List<Property> groupProperties = new List<Property>();

            string group = buildProperty.GetGroup();
		
		    foreach(var property in owned)
            {
                if (property.GetGroup() == group)
                {
                    groupProperties.Add(property);
                }
				
            }
            int compariter = 0;
            if (buying) {  compariter = buildProperty.GetNumHouses() + 1; }

            else {  compariter = buildProperty.GetNumHouses() - 1; }

            bool even = false;
		
		    foreach(var property in groupProperties)
            {
                if ((property.GetNumHouses() - 1) == compariter || (property.GetNumHouses() + 1) == compariter) { even = true; }
				

                else { even = false; }

            }
			

            return even;

        }
        public int GetNumHouses(Player player, string group = "all")
        {
            int num = 0;
            List<Property> propertys = player.GetOwenedPropertys();
            if(group == "all")
            {
                foreach(var property in propertys)
                {
                    num+= property.GetNumHouses();
                }
            }
            else
            {
                foreach (var property in propertys)
                {
                    if (property.GetGroup() == group)
                    {
                        num += property.GetNumHouses();

                    }
                    
                }

            }
            return num;
        }
        #region after
        /*public void makeRepairs(Player player, int perHouse, int perHotel)
        {
            List<Property> propertys = player.GetOwenedPropertys();
            foreach(var property in propertys)
            {
                if (property.GetType() == "standard")
                {
                    player.LoseCash(perHotel);
                }
                else { player.LoseCash(perHouse*property.GetNumHouses());}
            }
        }
        public double getHighestRent(Player player)
        {
            double highest = 0;
            foreach(Property tile in tiles)
            {
                
               
                if(! (tile.GetType() == "utility"))
                {
                    if(!(tile.GetOwner() == player))
                    {
                        if (tile.GetRent() > highest)
                        {
                            highest =tile.GetRent();
                        }
                    }
                }
            }
            return highest;

        }*/
        /*public bool hasMonopoly(Player player)
        {
            List<Property> owned = player.GetOwenedPropertys();
            Dictionary<string,int > group = new Dictionary<string, int>();
            foreach(var property in owned)
            {
                if (property.GetType() == "standard")
                {
                    if (group.ContainsKey(property.GetGroup()))
                    {
                        group[property.GetGroup()] += 1;
                        if (group[property.GetGroup()] == property.GetInGroup())
                        {
                            return true;
                        }
                    }
                    else
                    {
                        group[property.GetGroup()] = 1;
                        if (group[property.GetGroup()] == property.GetInGroup())
                        {
                            return true;    

                        }
						
                    }
                }
            }
            return false;
        }

        public bool inMonopoly(Property property, Player player)
        {
            if (property.GetType() == "standard")
            {
                string group = property.GetGroup();
                List<Property> playerProp = player.GetOwenedPropertys();
                int count = 0;
                foreach(var prop in playerProp)
                {
                    if (prop.GetGroup() == group)
                    {
                        count++;
                    }
                }
                if(count == property.GetInGroup())
                {
                    return true;
                }
                else { return false; }
            }
            return false;
        }*/
        #endregion
        public bool HasAttr(Tile expando, string key)
        {
            return ((IDictionary<string, Object>)expando).ContainsKey(key);
        }
        public void sendTo(Player player, Property property)
        {
            player.SetBoardPos(property.GetBoardPos());
        }
        public Tile findNearest(Player player, string group)
        {
            bool found = false;
            Tile nearest = null;
            int pos = player.GetBoardPos();

            int maxTile = tiles.Length;

            while (!found)
            {
                pos++;
                if (pos == maxTile)
                {
                    pos = 0;
                }
                if(tiles[pos] is Property)
                {
                    Property property = (Property)tiles[pos];
                    if (property.GetGroup() == group)
                    {
                        found = true;

                        nearest = tiles[pos];
                    }
                }
				

            }

            return nearest;
        }
        
        public void playCard(Card card,Player player)
        {
            string cardAction = card.GetActions();
            string action;
            action = cardAction.Split('(')[0];
            int[] vs = new int[3];
            if (cardAction.Split('(')[1].Split(')')[0].Split(',').Length > 1)
            {
                vs[0]=Convert.ToInt32(cardAction.Split('(')[1].Split(')')[0].Split(',')[1]);
            } 
            if (cardAction.Split('(')[1].Split(')')[0].Split(',').Length > 2)
            {
                vs[1] = Convert.ToInt32(cardAction.Split('(')[1].Split(')')[0].Split(',')[2]);
            }
                


            switch (action)
            {
                    case "movePlayer":
                        movePlayer(player, vs[0]);
                        break;
                    case "birthdayCash":
                        birthdayCash(player, vs[0]);
                        break;
                    case "sendToJail":
                        sendToJail(player);
                        break;
                    case "getOutOfJail":
                        getOutOfJail(player);
                        break;
                    case "deductCash":
                        deductCash(player, vs[0]);
                        break;
                    case "giveCash":
                        giveCash(player, vs[0]);
                        break;
                    /*case "isBankrupt":
                        isBankrupt(player, vs[0]);
                        break;
                    case "makeRepairs":
                        makeRepairs(player, vs[0], vs[1]);
                        break;
                    case "getHighestRent":
                        getHighestRent(player);
                        break;
                    case "hasMonopoly":
                        hasMonopoly(player);
                        break;*/
            }
            Console.Write(" "+card.GetCardText());
        }
        public bool inJail(Player player)
        {
            if(player.GetJailTurns() != 0)
            {
                if (player.HasGetOutOfJail())
                {
                    player.UseGetOutOfJail();
                    return false;
                }
                player.SetJailTurns(player.GetJailTurns()-1);
                return true;
            }
            return false;
        }
        public void Move(Player player, int moves,  Tile[] tiles)
        {
            if (inJail(player))
            {
                Console.Write(player.Name + " is in jail");
                return;
            }
            int pos = player.GetBoardPos();
            while (moves != 0)
            {
                if ((pos + 1) == tiles.Length)
                {
                    pos = 0;
                    Console.WriteLine(player.Name+" has passed Go and take 200");
                    player.GainCash(200);
                }
                else
                {
                    pos++ ;
                }
                moves--;
            }
            player.SetBoardPos(pos);
            Console.Write( player.Name + " становится на позицию " + tiles[pos].GetName());
            
        }
    }
       
}