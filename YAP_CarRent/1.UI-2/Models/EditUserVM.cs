using _4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1.UI_2.Models
{
    public class EditUserVM
    {
        public User User { get; set; }
       // public string Roles1 { get; set; }
        public List<Role> Roles2{ get; set; }
    }
}