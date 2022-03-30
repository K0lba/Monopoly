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


        public void SetName(string name) { Name = name; }

        public new string GetName() { return Name; }

        public void SetBoardPos(int pos) { BoardPos = pos; }
        public int GetBoardPos() { return BoardPos; }

        public abstract void TakeOrder(Player currentPlayer);

    }
    abstract class TileDecorator : Tile
    {
        protected Tile tile;
        public TileDecorator(Tile tile):base(tile.Name,tile.BoardPos)
        {
            this.tile = tile;
        }
        public virtual string GetName()
        {
            return tile.GetName();
        }
    }
    class BackGround:TileDecorator
    {
        private int CoefPrice = 2;
        private double CoefRent = 1.5;
        public int contUpgrage;
        private int level = 0;
        public BackGround(Tile tile) : base(tile)
        {

        }
        public override string GetName()
        {
            return base.Name + "lvl" + level.ToString();
        }
        public void Upgrade()
        {
            if(level == 4) { return; }
            level++;
            
        }

        public override void TakeOrder(Player currentPlayer)
        {
        }
    }

}
class PropertysInit
{
    OtherTile Go = new OtherTile("Go", 0);
    Property OldKentRoad = new Property("Old Kent Road", 1, 60, new int[] { 2, 10, 30, 90, 160, 250 }, 50, "brown", 2);
    Chest CommunityChest = new Chest("Community Chest",2);
    Property Whitechapel = new Property("Whitechapel",  3, 60, new int[] { 4, 20, 60, 180, 360, 450 }, 50, "brown", 2);
    Property KingsCrossStation = new Property("Kings Cross Station", 4, 200, new int[] { 25, 2 }, 0, "station", 0);
    Property TheAngelIslington = new Property("The Angel Islington", 5, 100, new int[] { 6, 30, 90, 270, 400, 550 }, 50, "lblue", 3);
    Chance Chance = new Chance("Chance", 6);
    Property EustonRoad = new Property("Euston Road", 7, 100, new int[] { 6, 30, 90, 270, 400, 550 }, 50, "lblue", 3);
    Property PentonvilleRoad = new Property("Pentonville Road", 8, 120, new int[] { 8, 40, 100, 300, 450, 600 }, 50, "lblue", 3);
    Jail Jail = new Jail("Jail", 9);
    Property PallMall = new Property("Pall Mall", 10, 140, new int[] { 10, 50, 150, 450, 625, 750 }, 100, "pink", 3);
    Property ElectricCompany = new Property("Electric Company", 11, 150, new int[] { 4, 10 }, 0, "utility", 0);
    Property Whitehall = new Property("Whitehall",  12, 140, new int[] { 10, 50, 150, 450, 625, 750 }, 100, "pink", 3);
    Property Northumberland = new Property("Northumberland", 13, 160, new int[] { 12, 60, 180, 500, 700, 900 }, 100, "pink", 3);
    Property MaryleboneStation = new Property("Marylebone Station", 14, 200, new int[] { 25, 2 }, 0, "station", 0);
    Property BowStreet = new Property("Bow Street", 15, 180, new int[] { 14, 70, 200, 550, 750, 950 }, 100, "orange", 3);
    Property MarlboroughStreet = new Property("Marlborough Street", 16, 180, new int[] { 14, 70, 200, 550, 750, 950 }, 100, "orange", 3);
    Property VineStreet = new Property("Vine Street", 17, 200, new int[] { 16, 80, 220, 600, 800, 1000 }, 100, "orange", 3);
    OtherTile FreeParking = new OtherTile("Free Parking",18);
    Property Strand = new Property("Strand", 19, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, 150, "red", 3);
    Property FleetStreet = new Property("Fleet Street", 20, 220, new int[] { 18, 90, 250, 700, 875, 1050 }, 150, "red", 3);
    Property TrafalgarSquare = new Property("Trafalgar Square", 21, 240, new int[] { 20, 100, 300, 750, 925, 1100 }, 150, "red", 3);
    Property FenchurchStStation = new Property("Fenchurch St Station", 22, 200, new int[] { 25, 2 }, 0, "station", 0);
    Property WaterWorks = new Property("Water Works",  23, 150, new int[] { 4, 10 }, 0, "utility", 0);
    GoToJail GoToJail = new GoToJail("Go To Jail",24);
    Chest CommunityChest2 = new Chest("Community Chest", 25);
    Property LiverpoolStreetStation = new Property("Liverpool Street Station",26,200,new int[] { 25, 2 },0,"station",0);
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
