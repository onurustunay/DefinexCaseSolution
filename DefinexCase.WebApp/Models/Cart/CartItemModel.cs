using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefinexCase.WebApp.Models.Cart
{
    public class CartItemModel
    {
        public int cart_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public double total_price { get; set; }
        public double unit_price { get; set; }
    }
}
