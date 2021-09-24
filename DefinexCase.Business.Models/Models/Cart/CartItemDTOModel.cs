using System;
using System.Collections.Generic;
using System.Text;

namespace DefinexCase.Business.Models.Models.Cart
{
    public class CartItemDTOModel
    {
        public int cart_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public double total_price { get; set; }
        public double unit_price { get; set; }
    }
}
