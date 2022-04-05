using MONOPOLY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{
    public abstract class Tile
    {
        public string Name;
        public int BoardPos;

        public Tile()
        {

        }

        public Tile(string name, int boardpos)
        {
            Name = name;
            BoardPos = boardpos;

        }

        public abstract void TakeTurn(Player currentPlayer);

    }
    abstract class PropertyDecorator : Property
    {
        protected Property property;
        public PropertyDecorator(string Name, int BoardPos, int BuyValue, int[] RentValue, int HouseCost, string Group) :base(Name, BoardPos, BuyValue, RentValue, HouseCost, Group)
        {
            this.property=property;
        }
        
    }

    class Monopoly : PropertyDecorator
    {
        public Monopoly(Property property) : base(property.Name + " in monopoly", property.BoardPos, property.BuyValue, property.RentValue, property.HouseCost, property.Group)
        {
            property.level += 1;
           
        }

        

    }
    class FirstHouse : PropertyDecorator
    {
        public FirstHouse(Property property): base(property.Name, property.BoardPos, property.BuyValue, property.RentValue, property.HouseCost, property.Group)
        {
            property.level += 1;
            property.Rent = property.RentValue[level];
            property.Name = base.Name + "lvl" + property.level;

        }

        
    }
    class SecondHouse : PropertyDecorator
    {
        public SecondHouse(Property property) : base(property.Name, property.BoardPos, property.BuyValue, property.RentValue, property.HouseCost, property.Group)
        {
            property.level += 1;
            property.Rent = property.RentValue[level];
            property.Name = base.Name + "lvl" + property.level;

        }

    }
    class ThirdHouse : PropertyDecorator
    {
        public ThirdHouse(Property property) : base(property.Name, property.BoardPos, property.BuyValue, property.RentValue, property.HouseCost, property.Group)
        {
            property.level += 1;
            property.Rent = property.RentValue[level];
            property.Name = base.Name + "lvl" + property.level;
        }
    }
    class Hotel : PropertyDecorator
    {
        public Hotel(Property property) : base(property.Name, property.BoardPos, property.BuyValue, property.RentValue, property.HouseCost, property.Group)
        {
            property.level += 1;
            property.Rent = property.RentValue[level];
            property.Name = base.Name + "hotel";
        }


    }

    

}
class PropertysInit
{
    Start Go = new Start("Go", 0);
    Property OldKentRoad = new Property("Old Kent Road", 1, 60, new int[] { 2, 10, 30, 90, 250 }, 50, "brown");
    Chance CommunityChest = new Chance("Community Chest",2);
    Property Whitechapel = new Property("Whitechapel",  3, 60, new int[] { 4, 20, 60, 180, 450 }, 50, "brown");
    Property KingsCrossStation = new Property("Kings Cross Station", 4, 200, new int[] { 25, 2 }, 0, "station");
    Property TheAngelIslington = new Property("The Angel Islington", 5, 100, new int[] { 6, 30, 90, 270, 550 }, 50, "lblue");
    Chance Chance = new Chance("Chance", 6);
    Property EustonRoad = new Property("Euston Road", 7, 100, new int[] { 6, 30, 90, 270, 550 }, 50, "lblue");
    Property PentonvilleRoad = new Property("Pentonville Road", 8, 120, new int[] { 8, 40, 100, 300, 600 }, 50, "lblue");
    Jail Jail = new Jail("Jail", 9);
    Property PallMall = new Property("Pall Mall", 10, 140, new int[] { 10, 50, 150, 450, 750 }, 100, "pink");
    Property ElectricCompany = new Property("Electric Company", 11, 150, new int[] { 4, 10 }, 0, "utility");
    Property Whitehall = new Property("Whitehall",  12, 140, new int[] { 10, 50, 150, 450, 750 }, 100, "pink");
    Property Northumberland = new Property("Northumberland", 13, 160, new int[] { 12, 60, 180, 500, 900 }, 100, "pink");
    Property MaryleboneStation = new Property("Marylebone Station", 14, 200, new int[] { 25, 2 }, 0, "station");
    Property BowStreet = new Property("Bow Street", 15, 180, new int[] { 14, 70, 200, 550, 950 }, 100, "orange");
    Property MarlboroughStreet = new Property("Marlborough Street", 16, 180, new int[] { 14, 70, 200, 550, 950 }, 100, "orange");
    Property VineStreet = new Property("Vine Street", 17, 200, new int[] { 16, 80, 220, 600, 1000 }, 100, "orange");
    Start FreeParking = new Start("Free Parking",18);
    Property Strand = new Property("Strand", 19, 220, new int[] { 18, 90, 250, 700, 1050 }, 150, "red");
    Property FleetStreet = new Property("Fleet Street", 20, 220, new int[] { 18, 90, 250, 700, 1050 }, 150, "red");
    Property TrafalgarSquare = new Property("Trafalgar Square", 21, 240, new int[] { 20, 100, 300, 750, 1100 }, 150, "red");
    Property FenchurchStStation = new Property("Fenchurch St Station", 22, 200, new int[] { 25, 2 }, 0, "station");
    Property WaterWorks = new Property("Water Works",  23, 150, new int[] { 4, 10 }, 0, "utility");
    GoToJail GoToJail = new GoToJail("Go To Jail",24);
    Chance CommunityChest2 = new Chance("Community Chest", 25);
    Property LiverpoolStreetStation = new Property("Liverpool Street Station",26,200,new int[] { 25, 2 },0,"station");
    Chance Chance2 = new Chance("Chance",  27);

    public Tile[] Init()
    {
        Tile[] tiles = new Tile[28];

        tiles[((Tile)Whitechapel).BoardPos] =Whitechapel;
        tiles[((Tile)Go).BoardPos] = Go;
        tiles[((Tile)Chance).BoardPos] = Chance;
        tiles[((Tile)OldKentRoad).BoardPos] = OldKentRoad;
        tiles[((Tile)Chance2).BoardPos] = Chance2;
        tiles[((Tile)CommunityChest2).BoardPos] = CommunityChest2;
        tiles[((Tile)FreeParking).BoardPos] = FreeParking;
        tiles[((Tile)GoToJail).BoardPos] = GoToJail;
        tiles[((Tile)Jail).BoardPos] = Jail;
        tiles[((Tile)CommunityChest).BoardPos] = CommunityChest;
        tiles[((Tile)LiverpoolStreetStation).BoardPos] = LiverpoolStreetStation;
        tiles[((Tile)WaterWorks).BoardPos] = WaterWorks;
        tiles[((Tile)FenchurchStStation).BoardPos] = FenchurchStStation;
        tiles[((Tile)MaryleboneStation).BoardPos] = MaryleboneStation;
        tiles[((Tile)TheAngelIslington).BoardPos] =TheAngelIslington;
        tiles[((Tile)EustonRoad).BoardPos] =EustonRoad;
        tiles[((Tile)PentonvilleRoad).BoardPos] = PentonvilleRoad;
        tiles[((Tile)PallMall).BoardPos] =PallMall;
        tiles[((Tile)Whitehall).BoardPos] = Whitehall;
        tiles[((Tile)Northumberland).BoardPos] =Northumberland;
        tiles[((Tile)BowStreet).BoardPos] =BowStreet;
        tiles[((Tile)MarlboroughStreet).BoardPos] =MarlboroughStreet;
        tiles[((Tile)VineStreet).BoardPos] =VineStreet;
        tiles[((Tile)Strand).BoardPos] =Strand;
        tiles[((Tile)FleetStreet).BoardPos] = FleetStreet;
        tiles[((Tile)TrafalgarSquare).BoardPos] =TrafalgarSquare;
        tiles[((Tile)KingsCrossStation).BoardPos] =KingsCrossStation;
        tiles[((Tile)ElectricCompany).BoardPos] = ElectricCompany;
        return tiles;
    }
}
