using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{
    public class Property : Tile
    {
        public int level = 0;
        public int BuyValue;
        public int[] RentValue;
        public int Rent;
        public int NumHouses=0;
        public int HouseCost;
        public bool IsMorgaged=false;
        public string Group;
        public Player owner;
        public double earning = 0;
       
        public Tile tile;

        public Property(string name, int boardpos, int buyValue,int[] rentValue, int houseCost, string group ):base(name,boardpos)
        {
            BuyValue = buyValue;
            RentValue=rentValue;
            HouseCost = houseCost;
            Group = group;  
            Rent = RentValue[0];

        }
        public void SetOwner(Player player) { owner = player; }
        public override void TakeTurn(Player currentPlayer)
        {
            if (Game.CanUpgradeProperty(this, currentPlayer))
            {
                Game.UpgradeProperty(this);
            }
            if (this.owner == null)
            {
                if (currentPlayer.Balance > this.BuyValue)
                {
                    currentPlayer.LoseCash(this.BuyValue);
                    currentPlayer.AddNewProperty(this);
                    this.SetOwner(currentPlayer);
                    
                    Console.Write(" купил " + this.Name);
                    Game.CanUpgradeMonopoly(currentPlayer,this.Group);
                }
                else
                {
                    Console.Write(" Недостаточно средств ");
                   
                }
            }
            else
            {
                if (owner != currentPlayer)
                {
                    if (currentPlayer.Balance > Rent)
                    {
                        currentPlayer.LoseCash(Rent);
                        Console.Write(" player had payed the rent ");
                    }
                    else
                    {
                        currentPlayer.IsBankrupt = true;
                    }


                }
            }
        }
    }
    class Start:Tile
    {
        public Start(string name, int boardpos) : base(name,boardpos)
        {

        }
        public override void TakeTurn(Player currentPlayer)
        {
                
        }
    }
    public class Chance : Tile
    {
        private string Name = "";
        public Chance(string name,int pos):base(name,pos)
        {
            Name = name;
        }
        public override void TakeTurn(Player currentPlayer)
        {
            if(Game.Cards.Length!=0)
                   Game.api.playCard(TakeFront(), currentPlayer);
            
        }
        public Card TakeFront()
        {
            var card = Game.Cards[0];
            for(int i=0; i<Game.Cards.Length-1;i++)
            {
                Game.Cards[i] = Game.Cards[i + 1];
            }
            return card;
        }
    }
    public class Station : Property
    {
        public Station(string name, int boardpos, int buyValue, int[] rentValue, int houseCost, string group) : base(name, boardpos, buyValue, rentValue, houseCost, group)
        {
        }

        public override void TakeTurn(Player currentPlayer)
        {
            
        }
    }
    public class GoToJail : Tile
    {
        public GoToJail(string name, int pos) : base(name, pos)
        {

        }
        public override void TakeTurn(Player currentPlayer)
        {
            Game.api.sendToJail(currentPlayer);
        }
    }
    public class Jail : Tile
    {
        public Jail(string name,int pos):base (name,pos)
        {
               
        }
        public override void TakeTurn(Player currentPlayer)
        {
        }
    }
}
