using System;
using System.Collections.Generic;
using System.Text;

namespace DefinexCase.Business.Models.Models.Product
{
    public class ProductDTOModel
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public double product_price { get; set; }
        public bool discount { get; set; }
    }
}
