using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MONOPOLY
{
    public class Die
    {
        private int[] m_dices = new int[2];
        private int JailRoll = 3;
        private int DoubleCount =0;

        public Die()
        {

        }

        public void roll()
        {
            Random rnd = new Random();
            m_dices[0] = rnd.Next(1, 6);
            m_dices[1] = rnd.Next(1, 6);
            CheckDouble();

        }
        
        public void CheckDouble()
        {
            if (m_dices[0] == m_dices[1]) { DoubleCount += 1; }
            else { DoubleCount = 0; }
        }

        public int GetTotal() { return m_dices[0] + m_dices[1]; }
        

        public int GetDoubleCount()
        {
            return DoubleCount;
        }
        public int GetJailRoll()
        {
            return JailRoll;
        }

        public void SetDoubleCount(int count) { DoubleCount=count; }
    }
}
