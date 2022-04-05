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
        public int GetBuyValue() { return this.BuyValue;}
        public double GetRent()
        {
            return Rent;
        }
        public Player GetOwner()
        {
            return owner;
        }

        public int GetHouseCost() { return HouseCost; }
        public string GetGroup()
        {
            return Group;
        }

        public void SetOwner(Player player) { owner = player; }
  
        public override void TakeTurn(Player currentPlayer)
        {
            if (Game.CanUpgradeProperty(this, currentPlayer))
            {
                Game.UpgradeProperty(this);
            }
            if (this.GetOwner() == null)
            {
                if (currentPlayer.Balance > this.GetBuyValue())
                {
                    currentPlayer.LoseCash(this.GetBuyValue());
                    currentPlayer.AddNewProperty(this);
                    this.SetOwner(currentPlayer);
                    
                    Console.Write(" купил " + this.Name);
                    Game.CanUpgradeMonopoly(currentPlayer,this.GetGroup());
                }
                else
                {
                    Console.Write(" Недостаточно средств ");
                   
                }
            }
            else
            {
                if (GetOwner() != currentPlayer)
                {
                    if (currentPlayer.Balance > GetRent())
                    {
                        currentPlayer.LoseCash(GetRent());
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
        public string Name;
        public int BoardPos;

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
            if( Name == "Chance")
                if(Game.GameDeck.ChanceDeck.m_deck.Count!=0)
                    Game.api.playCard(Game.GameDeck.ChanceDeck.takeFront(), currentPlayer);
                else
                    Game.gameDeck.Init();
            else
            {
                if (Game.GameDeck.ChestDeck.m_deck.Count != 0)
                    Game.api.playCard(Game.GameDeck.ChestDeck.takeFront(), currentPlayer);
                else
                    Game.gameDeck.Init();
            }
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
