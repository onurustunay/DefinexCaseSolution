using System;
using System.Collections.Generic;
using System.Text;

namespace DefinexCase.Business.Models.Models.Cart
{
    public class CartDTOModel
    {
        public int id { get; set; }
        public string status { get; set; }
        public double total_price { get; set; }
        public int discount_type { get; set; }
        public double discount_price { get; set; }
        public DateTime created_date { get; set; }
    }
}
