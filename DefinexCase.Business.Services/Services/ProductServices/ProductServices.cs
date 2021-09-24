using AutoMapper;
using DefinexCase.Business.Models.Models.Product;
using DefinexCase.Data.Repository.Repositories.ProductRepository;
using DefinexCase.Data.Entity.Entity.Product;
using System;
using System.Collections.Generic;

using System.Text;

namespace DefinexCase.Business.Services.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private List<ProductDTOModel> _productModelsList = new List<ProductDTOModel>();
        private readonly IMapper _mapper;
        private ProductEntity _productEntity;

        public ProductServices(IProductRepository productRepository, IMapper mapper) {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDTOModel> GetAllProducts()
        {
            var allProducts = _productRepository.GetAllProducts();
            var productsResource = _mapper.Map<IEnumerable<ProductEntity>, IEnumerable<ProductDTOModel>>(allProducts, _productModelsList);

            return productsResource;
        }

        public IEnumerable<ProductDTOModel> GetProduct(int productId)
        {
            var allProducts = _productRepository.GetProduct(productId);
            var productsResource = _mapper.Map<IEnumerable<ProductEntity>, IEnumerable<ProductDTOModel>>(allProducts, _productModelsList);

            return productsResource;
        }

        public bool UpdateProduct(ProductDTOModel data)
        {
            var productsResource = _mapper.Map<ProductDTOModel, ProductEntity>(data, _productEntity);
            var response = _productRepository.UpdateProduct(productsResource);
            
            return response;
        }

        public bool CreateProduct(ProductDTOModel data)
        {
            var productsResource = _mapper.Map<ProductDTOModel, ProductEntity>(data, _productEntity);
            var response = _productRepository.CreateProduct(productsResource);

            return response;
        }

        public bool DeleteProduct(int productId)
        {
            var response = _productRepository.DeleteProduct(productId);

            return response;
        }
    }
}
