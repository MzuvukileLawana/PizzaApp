using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizza.Models
{
    public class UpdatePizzaPriceModel
    {
        public string pizzaName { get; set; }
        public int price { get; set; }
    }
}