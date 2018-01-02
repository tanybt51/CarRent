using _4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.UI_2.Models
{
    public class EditCarVM
    {
        public CarVM Car { get; set; }
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> Statuses { get; set; }

        public EditCarVM()
        {
            Branches = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
        }
    }
}