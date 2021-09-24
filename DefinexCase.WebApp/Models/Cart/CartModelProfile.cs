using AutoMapper;
using DefinexCase.Business.Models.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefinexCase.WebApp.Models.Cart
{
    public class CartModelProfile : Profile
    {
        public CartModelProfile()
        {
            CreateMap<CartItemDTOModel, CartItemModel>();
            CreateMap<CartItemModel, CartItemDTOModel>();

            CreateMap<CartDTOModel, CartModel>();
            CreateMap<CartModel, CartDTOModel>();
        }
    }
}
