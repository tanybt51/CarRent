//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        public Car()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Serial { get; set; }
        public int Model { get; set; }
        public Nullable<int> Kilometerage { get; set; }
        public string ImagePath { get; set; }
        public string Image { get; set; }
        public Nullable<int> Status { get; set; }
        public int Branch { get; set; }
        public Nullable<int> Color { get; set; }
        public Nullable<int> Gear { get; set; }
    
        public virtual Branch Branch1 { get; set; }
        public virtual Color Color1 { get; set; }
        public virtual Gear Gear1 { get; set; }
        public virtual Model Model1 { get; set; }
        public virtual Status Status1 { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
