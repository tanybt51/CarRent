using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1.UI_2.Models
{
    public class CarVM
    {
        [Display(Name ="לוחית זיהוי")]
        public int Serial { get; set; }
        [Display(Name ="מודל")]
        public string Model { get; set; }
        [Display(Name ="קילומטראג")]
        public int? Kilometerage { get; set; }
        [Display(Name ="תמונה")]
        public string Image { get; set; }
        public HttpPostedFileBase img { get; set; }
        [Display(Name ="סטטוס")]
        public string Status { get; set; }
        [Display(Name ="סניף")]
        public string Branch { get; set; }
        [Display(Name ="גיר")]
        public string Gear { get; set; }
        [Display(Name ="מחיר יומי")]
        public double Price { get; set; }
        [Display(Name = "קבוצה")]
        public string Group { get; set; }

        public List<SelectListItem> Rankings { get; set; }

    }
}