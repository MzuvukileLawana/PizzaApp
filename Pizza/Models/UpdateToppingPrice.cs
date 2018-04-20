using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza.Models
{
    public class UpdateToppingPrice
    {
        public string topping { get; set; }
        public int price { get; set; }
    }
}