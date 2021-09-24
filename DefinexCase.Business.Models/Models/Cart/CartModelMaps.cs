using AutoMapper;
using DefinexCase.Data.Entity.Entity.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefinexCase.Business.Models.Models.Cart
{
    public class CartModelMaps : Profile
    {
        public CartModelMaps() {
            CreateMap<CartItemDTOModel, CartItemEntity>();
            CreateMap<CartItemEntity, CartItemDTOModel>();

            CreateMap<CartDTOModel, CartEntity>();
            CreateMap<CartEntity, CartDTOModel>();
        }
    }
}
