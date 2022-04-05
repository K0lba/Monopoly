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


        public int jailTurns = 0;


        public List<Card> numOfOutJailCards = new List<Card>();
        

        public Player(int order,string name)
        {
            RollOrder = order;
            Name = name;

        }
        
        public void AddNewProperty(Property prop)
        {
            OwenedProperties.Add(prop);
        }
        
        public void GainCash(double cash) { Balance += cash; }

        public void LoseCash(double ammount) { Balance -= ammount; }

        public void SetBoardPos(int pos)
        {
            BoarPos = pos;
        }
        public void SetJailTurns(int turns) { jailTurns = turns; }
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
