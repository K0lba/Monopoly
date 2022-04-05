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

