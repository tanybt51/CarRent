using _4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1.UI_2.Models
{
    public class OrderVM
    {
        public int ID { get; set; }
        public string User { get; set; }
        public int Car { get; set; }
        public string Image { get; set; }
        public string Model { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Return { get; set; }
        public string Branch { get; set; }
        public Double Price { get; set; }
        public Double? Fine { get; set; }
        public Double? Payment { get { return Price + Fine; } }






    }
}