using _4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.UI_2.Models
{
    public class FindCarVM
    {
        public List<SelectListItem> Branches { get; set; }
        public List<SelectListItem> Manufacturers { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Gear { get; set; }
        public List<CarVM> LastSearch { get; set; }
        public FindCarVM(List<Branch> _branches,List<Manufacturer>_man,List<Group>_group,List<Gear>_gear,List<CarVM> _lastSearch=null)
        {
            Branches = _branches.Select(b=>new SelectListItem() {Text=b.Name,Value = b.ID.ToString() }).ToList() as List<SelectListItem>;
            Manufacturers = _man.Select(m => new SelectListItem() { Text = m.Name, Value = m.ID.ToString() }).ToList() as List<SelectListItem>;
            Groups = _group.Select(g => new SelectListItem() { Text = g.Name, Value = g.Level.ToString() }).ToList() as List<SelectListItem>;
            Gear = _gear.Select(g => new SelectListItem() { Text = g.Name, Value = g.ID.ToString() }).ToList() as List<SelectListItem>;
            LastSearch = _lastSearch;
        }
    }
}