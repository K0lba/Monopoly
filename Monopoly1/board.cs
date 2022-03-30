using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{
    abstract class board
    {
        public int Width = 11;
        public  int Height = 11;
        public bool EvenBuid = true;
        public int StartCash;
        public double MorgagePercent = 0.5;
        public double MorgageInterest = 1.1;
        public int AvailableHouses = 32;
        public int AvailableHotel = 12;
        public double HouseSellPercent =1.0;
        public int bail = 50;

        public void SetBail(int bail) { this.bail = bail; }
        public int GetBail() { return bail; }

        public void SetEvanBuild(bool evan) { EvenBuid = evan; }
        
        public bool GetEvanBuild() { return EvenBuid; }
        public void SetDimentions(int Width,int Height)
        {
            this.Width = Width; this.Height = Height;
        }

        public List<int> GetDimentions() 
        {
            List<int> list = new List<int>();
            list.Add(Width);
            list.Add(Height);
            return list; 
        }
        public void SetStartCash(int cash) { StartCash = cash; }
        public int GetStartCash() { return StartCash; }

        public void SetMorgagePercent(Double morgage) { MorgagePercent = morgage; }
        public Double GetMorgagePercent() { return MorgagePercent; }

        public void SetMorgageInterest(Double morgageInterest) { MorgageInterest = morgageInterest; }

        public Double GetMorgageInterest() { return MorgageInterest; }

        public void SetAvailableHouses(int AvailableHouses) { AvailableHouses = AvailableHouses;}
        public int GetAvailableHouses() { return AvailableHouses; }

        public void SetHousesPercent(Double houses) { HouseSellPercent = houses; }

        public double GetHousesSellPercent() { return HouseSellPercent; }

        public void IncAvailableHouses() { AvailableHouses += 1; }
        public void DecAvailableHouses() { AvailableHouses-= 1; }

        public void SetAvailableHotels(int AvailableHotels) { this.AvailableHotel = AvailableHotels; }
        public int GetAvailableHotels() { return AvailableHotel; }

        public void IncAvailableHotels() { AvailableHotel += 1; }
        public void DecAvailableHotels() { AvailableHotel -= 1; }



    }
}
