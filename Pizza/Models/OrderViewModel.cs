using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza.Models
{
    public class OrderViewModel
    {
        public string pizzaName { get; set; }
        public string size { get; set; }
        public string topping { get; set; }
        public string drink { get; set; }
        public string delivery { get; set; }
        public string address { get; set; }       
        public string number { get; set; }
    }
    
}