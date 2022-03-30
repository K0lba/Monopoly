using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{
    public class Player
    {

        public List<Property> OwenedProperties = new List<Property>();
        public string Name;
        public int RollOrder;
        
        public bool inJail = false;
        
        public bool IsBankrupt = false;
        
        public double Balance = 1000;
        public int BoarPos = 0;

        public string Strategy;

        public int jailTurns = 0;


        public List<Card> numOfOutJailCards = new List<Card>();
        

        public Player(int order, string Start,string Name)
        {
            RollOrder = order;
            Strategy = Start;
            this.Name = Name;

        }
        public string GetName()
        {
            return this.Name;
        }


        public bool GetBankrupt() { return IsBankrupt; }
        public void SetBankrupt(bool bankrupt) { IsBankrupt = bankrupt; }
        public int GetRollOrder()
        {
            return RollOrder;
        }
        public void SetRollOrder(int value)
        {
            RollOrder = value;
        }
        public void AddNewProperty(Property prop)
        {
            OwenedProperties.Add(prop);
        }

        public void RemoveProperty(Property prop)
        {
            OwenedProperties.Remove(prop);
        }

        public List<Property> GetOwenedPropertys() 
        { 
            return OwenedProperties;
        }

        public void SetOwnedPropertys(List<Property> prop) { OwenedProperties = prop; }
        
        public double GetCash() { return Balance; }
        public void GainCash(double cash) { Balance += cash; }

        public void LoseCash(double ammount) { Balance -= ammount; }
        public void SetCash(int cash) { Balance = cash; }

        public void SetBoardPos(int pos)
        {
            BoarPos = pos;
        }
        public int GetBoardPos() { return BoarPos; }

        public void SetStrategy(string strat)
        {
            Strategy = strat;
        }
        public string GetStrategy() { return Strategy; }
        public void SetJailTurns(int turns) { jailTurns = turns; }
        public int GetJailTurns() { return jailTurns; }

        public void decJailTurns() { jailTurns -= 1; }
        public void AddGetOutOfJail(Card card)
        {
            numOfOutJailCards.Add(card);
        }
        public bool HasGetOutOfJail()
        {
            if(numOfOutJailCards.Count != 0) { return true; }
            return false;
        }
        public Card UseGetOutOfJail()
        {
            jailTurns = 0;
            Card card = numOfOutJailCards[0];
            numOfOutJailCards.Remove(card);
            return card;
        }

    }
}
