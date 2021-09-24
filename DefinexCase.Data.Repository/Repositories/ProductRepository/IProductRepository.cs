using System;
using System.Collections.Generic;
using System.Text;
using DefinexCase.Data.Entity.Entity.Product;

namespace DefinexCase.Data.Repository.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        IEnumerable<ProductEntity> GetAllProducts();
        IEnumerable<ProductEntity> GetProduct(int productId);
        bool UpdateProduct(ProductEntity data);
        bool CreateProduct(ProductEntity data);
        bool DeleteProduct(int productId);
    }
}
