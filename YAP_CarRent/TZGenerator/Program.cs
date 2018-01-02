using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            TzGenerator tzGenerator = new TzGenerator();
            tzGenerator.FindTz();
            foreach (var t in tzGenerator.Tz)

            {
                Console.WriteLine(t);
            }
            Console.ReadLine();
        }
    }
}
