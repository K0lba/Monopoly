using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{
    
    public class Card
    {
        private string CardText;
        private string action;
        private string Name;

        public Card(string name , string CardText, string action)
        {
            this.CardText = CardText;
            this.action = action;
            this.Name = name;
        }

        public string GetCardText()
        {
            return CardText;
        }

        public string GetActions() { return action; }


    }
    
}
