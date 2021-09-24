using DefinexCase.Business.Models.Models.Product;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefinexCase.WebApp.Models.Product
{
    public class ProductModelProfile : Profile
    {
        public ProductModelProfile()
        {
            CreateMap<ProductDTOModel, ProductModel>();
            CreateMap<ProductModel, ProductDTOModel>();
        }
    }
}
