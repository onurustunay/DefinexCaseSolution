using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DefinexCase.Data.Entity.Entity.Product;

namespace DefinexCase.Business.Models.Models.Product
{
    public class ProductModelMaps : Profile
    {
        public ProductModelMaps() {
            CreateMap<ProductDTOModel, ProductEntity>();
            CreateMap<ProductEntity, ProductDTOModel>();
        } 
    }
}
