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
            player.SetBoardPos(player.BoarPos + move);
            int maxTile = tiles.Length;
            if(player.BoarPos >= maxTile)
            {
                player.SetBoardPos(player.BoarPos - maxTile);
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
        public void sendToJail(Player player)
        {
            player.SetJailTurns(3);
            for(int i=0; i<tiles.Length; i++)
            {
                if (tiles[i] is Jail)
                {
                    player.SetBoardPos(tiles[i].BoardPos);
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
            player.LoseCash(property.BuyValue);
            player.AddNewProperty(property);
            property.SetOwner(player);
        }
        
        public void deductCash(Player player, double amount)
        {
            player.LoseCash(amount);
        }
        public void giveCash(Player player, double ammount)
        {
            player.GainCash(ammount);
        }
        
        
        public bool HasAttr(Tile expando, string key)
        {
            return ((IDictionary<string, Object>)expando).ContainsKey(key);
        }
        public void sendTo(Player player, Property property)
        {
            player.SetBoardPos(property.BoardPos);
        }
        public Tile findNearest(Player player, string group)
        {
            bool found = false;
            Tile nearest = null;
            int pos = player.BoarPos;

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
                    if (property.Group == group)
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
                    
            }
            Console.Write(" "+card.GetCardText());
        }
        
        
    }
       
}