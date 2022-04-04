﻿using System;
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
        public int InGroup;
        public Tile tile;

        public Property(string name, int boardpos, int buyValue,int[] rentValue, int houseCost, string group, int ingroup ):base(name,boardpos)
        {
            BuyValue = buyValue;
            RentValue=rentValue;
            HouseCost = houseCost;
            Group = group;  
            InGroup = ingroup;
            Rent = RentValue[0];

        }

        public void SetBuyValue(int value) { this.BuyValue = value; }

        public int GetBuyValue() { return this.BuyValue;}

        public void SetRentValue(int[] value) { this.RentValue = value;}

        public int[] GetRentValue() 
        {
            return this.RentValue;
        }

        public double GetRent()
        {
            return Rent;
        }
        public void SetRent(int Rent)
        {
            this.Rent = Rent;
        }
        public void SetNumHouses(int value) { NumHouses = value; }

        public int GetNumHouses() { return this.NumHouses; }

        public void SetHouseCost(int value) { HouseCost = value; }

        public int GetHouseCost() { return HouseCost; }

        public void SetIsMorgaged(bool value) { IsMorgaged = value; }

        public bool GetIsMorgaged() { return IsMorgaged; }

        
        
        public void SetGroup(string value)
        {
            Group = value;
        }

        public string GetGroup()
        {
            return Group;
        }

        public int GetInGroup() { return InGroup; }

        public void SetOwner(Player player) { owner = player; }

        public Player GetOwner() { return owner; }

        public void AddEarnings(double ammount) { earning += ammount; }

        public void DeductEarnings(int ammount) { earning -= ammount; }
        public double GetEarnings() { return earning; }

        public override void TakeOrder(Player currentPlayer)
        {
            if (Game.CanUpgradeProperty(this, currentPlayer))
            {
                Game.UpgradeProperty(this);
            }
            if (this.GetOwner() == null)
            {
                if (currentPlayer.GetCash() > this.GetBuyValue())
                {
                    currentPlayer.LoseCash(this.GetBuyValue());
                    currentPlayer.AddNewProperty(this);
                    this.SetOwner(currentPlayer);
                    
                    Console.Write(" купил " + this.GetName());
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
                    if (currentPlayer.GetCash() > GetRent())
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
    class OtherTile:Tile
    {
        public string Name;
        public int BoardPos;

        public OtherTile()
        {

        }

        public OtherTile(string name, int boardpos) : base(name,boardpos)
        {

        }


        public void SetName(string name) { Name = name; }

        public new string GetName() { return Name; }

        public void SetBoardPos(int pos) { BoardPos = pos; }
        public int GetBoardPos() { return BoardPos; }

        public override void TakeOrder(Player currentPlayer)
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
        public override void TakeOrder(Player currentPlayer)
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
    public class Station : Tile
    {
        public override void TakeOrder(Player currentPlayer)
        {
            
        }
    }
    public class GoToJail : Tile
    {
        public GoToJail(string name, int pos) : base(name, pos)
        {

        }
        public override void TakeOrder(Player currentPlayer)
        {
            Game.api.sendToJail(currentPlayer);
        }
    }
    public class Jail : Tile
    {
        public Jail(string name,int pos):base (name,pos)
        {
               
        }
        public override void TakeOrder(Player currentPlayer)
        {
        }
    }
}
