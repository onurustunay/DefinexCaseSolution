using System;
using System.Collections.Generic;
using System.Text;

namespace DefinexCase.Data.Entity.Entity.Cart
{
    public class CartItemEntity
    {
        public int cart_id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int quantity { get; set; }
        public int total_price { get; set; }
        public int unit_price { get; set; }
    }
}
