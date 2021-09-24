using System;
using System.Collections.Generic;
using System.Text;
using DefinexCase.Business.Models.Models.Product;

namespace DefinexCase.Business.Services.Services.ProductServices
{
    public interface IProductServices
    {
        IEnumerable<ProductDTOModel> GetAllProducts();
        IEnumerable<ProductDTOModel> GetProduct(int productId);
        bool UpdateProduct(ProductDTOModel data);
        bool CreateProduct(ProductDTOModel data);
        bool DeleteProduct(int productId);
    }
}
