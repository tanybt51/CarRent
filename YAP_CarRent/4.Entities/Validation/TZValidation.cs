using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class TZAttribute : ValidationAttribute
    {
        public override bool IsValid(object _tz)
        {
            string tz = _tz.ToString();
            if (tz.Length==9)
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
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return "ccccc";
        }
    }
}
