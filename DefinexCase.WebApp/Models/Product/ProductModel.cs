using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefinexCase.WebApp.Models.Product
{
    public class ProductModel
    {
        public int product_id { get; set; }
        
        public string product_name { get; set; }
        public int product_price { get; set; }
        public bool discount { get; set; }
    }
}
