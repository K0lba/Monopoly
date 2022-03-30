using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{
    internal class Deck
    {
        private int type;
        public List<Card> m_deck = new List<Card>();


        public Deck(int type)
        {
            this.type = type;
        }
        public Card takeFront()
        {
            Card card = m_deck.FirstOrDefault();
            m_deck.Remove(card);
            return card;
        }
        public void placeBack(Card card)
        {
            m_deck.Add(card);
        }
        public void Shuffle()
        {
            Random rand = new Random();
            m_deck = new List<Card>(m_deck.OrderBy((o) =>
            {
                return (rand.Next() % m_deck.Count);
            }));
        }

    }
    public class Card
    {
        //chance: type = 1, community chest: type = 2 
        private int CardType;
        private string CardText;
        private string action;
        private string Name;

        public Card(int Cardtype,string name , string CardText, string action)
        {
            this.CardType = Cardtype;
            this.CardText = CardText;
            this.action = action;
            this.Name = name;
        }
        public string GetName()
        {
            return Name;
        }
        public int GetCardType()
        {
            return CardType;
        }

        public string GetCardText()
        {
            return CardText;
        }

        public string GetActions() { return action; }


    }
    class CardInit
    {
        public Deck Chestdeck = new Deck(1);
        public Deck Chancedeck = new Deck(2);

        public Deck ChestDeck { get { return Chestdeck; } }
        public Deck ChanceDeck { get { return Chancedeck; } }

        Card GoToJail = new Card(0, "GoToJail", "go to jail", "sendToJail(player)");
        Card GoBackThreeSpaces = new Card(1, "GoBackThreeSpaces", "move three cells back", "movePlayer(player,-3)");
        Card MakeRepairs = new Card(1, "MakeRepairs", "make general repairs on all of his houses", "makeRepairs(player,25,100)");
        Card PayForSchool = new Card(1, "PayForSchool", "pay school fees of 150", "deductCash(player,150)");
        Card DrunkInCharge = new Card(1, "DrunkInCharge", "get fine in the amount of 20 for drunkenness", "deductCash(player,20)");
        Card SpeedingFine = new Card(1, "SpeedingFine", "get fine in the amount of 15 for speeding", "deductCash(player,15)");
        Card BuildingLoan = new Card(1, "BuildingLoan", "receive 150 for a loan matures", "giveCash(player,150)");
        Card WonCompetition = new Card(1, "WonCompetition", "win a crossword competition and collect 100", "giveCash(player,150)");
        Card GetOutOfJail = new Card(0, "GetOutOfJail", "dug a tunnel and escaped jail", "getOutOfJailFree()");

        Card PayHospitalLose = new Card(2, "PayHospitalLose", "pay hospital fees, lose 100", "deductCash(player,100)");
        Card WayPedest = new Card(2, "WayPedest", "did not give way to a pedestrian, fine 50", "deductCash(player,50)");
        Card BankError = new Card(2, "BankError", "bank error in your favour, gain 200", "giveCash(player,200)");
        Card Inherit = new Card(2, "Inherit", "inherit 100", "giveCash(player,100)");
        Card Sale = new Card(2, "Sale", "from sale of stock  get 50", "giveCash(player,50");
        Card Birthday = new Card(2, "Birthday", "have birthday, collect 10 from each player", "birthdayCash(player,10)");
        Card Demonstration = new Card(2, "Demonstration", "spent the night in jail and got a fine", "deductCash(player,10)");

        public void Init()
        {
            Chancedeck.placeBack(GoToJail);
            Chancedeck.placeBack(GoBackThreeSpaces);
            Chancedeck.placeBack(MakeRepairs);
            Chancedeck.placeBack(PayForSchool);
            Chancedeck.placeBack(DrunkInCharge);
            Chancedeck.placeBack(SpeedingFine);
            Chancedeck.placeBack(BuildingLoan);
            Chancedeck.placeBack(WonCompetition);
            Chancedeck.placeBack(GetOutOfJail);
            Chancedeck.Shuffle();

            Chestdeck.placeBack(GoToJail);
            Chestdeck.placeBack(PayHospitalLose);
            Chestdeck.placeBack(WayPedest);
            Chestdeck.placeBack(BankError);
            Chestdeck.placeBack(Inherit);
            Chestdeck.placeBack(Sale);
            Chestdeck.placeBack(Birthday);
            Chestdeck.placeBack(Demonstration);
            Chestdeck.placeBack(GetOutOfJail);
            Chestdeck.Shuffle();
        }
    }
}
