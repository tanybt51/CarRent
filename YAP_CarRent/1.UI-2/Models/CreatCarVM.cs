using _4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.UI_2.Models
{
    public class CreatCarVM
    {
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> Manufacturer { get; set; }
        public List<SelectListItem> Status { get; set; }
        public List<SelectListItem> Gear { get; set; }
        public CreatCarVM()
        {
            Branches = new List<SelectListItem>();
            Manufacturer = new List<SelectListItem>();
            Status = new List<SelectListItem>();
            Gear = new List<SelectListItem>();
        }
    }
}