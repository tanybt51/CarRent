using _2.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5.TestUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //UserManager uManger = new UserManager();
            // var d= uManger.Users;
            OrderManager manager = new OrderManager();
            //bool b= manager.IsCarAvailable(98765455, new DateTime(2017, 7, 9), new DateTime(2017, 7, 10));
            var users = manager.GetAllUser();

            Console.ReadLine();

        }
    }
}
