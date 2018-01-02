using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZGenerator
{
   public class TzGenerator
    {

        public List<long> Tz { get; set; }
        public TzGenerator()
        {
            Tz = new List<long>();
        }
        public void FindTz()
        {
            for (long i = 100000000; i <= 200000000; i++)
            {
                if (IsTz(i.ToString()))
                    Tz.Add(i);

            }
            return; 


        }
        private bool IsTz(string tz)
        {
            string[] numbers = new string[9];
            int[] oneTwo = new int[9] { 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            for (int i = 0; i < 9; i++)
            {
                int temp = -1;
                if (int.TryParse(tz[i].ToString(), out temp))
                {
                    oneTwo[i] *= temp;
                    if (oneTwo[i] > 9)
                    {
                        oneTwo[i] = oneTwo[i] / 10 + oneTwo[i] % 10;

                    }
                }
                else
                    return false;

            }
            int sum = oneTwo.Sum();
            if (sum % 10 == 0)
                return true;
            return false;
        }
    }
}
