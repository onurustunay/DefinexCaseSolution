using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefinexCase.WebApp.Models.Cart
{
    public class CartModel
    {
        public int id { get; set; }
        public string status { get; set; }
        public double total_price { get; set; }
        public int discount_type { get; set; }
        public double discount_price { get; set; }
        public DateTime created_date { get; set; }
    }
}
