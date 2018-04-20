//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pizza.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int OrderId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int CustId { get; set; }
        public string Payment_type { get; set; }
        public int DeliveryId { get; set; }
        public Nullable<int> PizzaId { get; set; }
        public Nullable<int> BaseId { get; set; }
        public Nullable<int> ToppingId { get; set; }
        public Nullable<int> DrinkId { get; set; }
    
        public virtual Base Base { get; set; }
        public virtual CoolDrink CoolDrink { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual DeliveryMethod DeliveryMethod { get; set; }
        public virtual Order Order1 { get; set; }
        public virtual Order Order2 { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual Topping Topping { get; set; }
    }
}
