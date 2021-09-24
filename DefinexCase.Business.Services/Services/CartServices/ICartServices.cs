using DefinexCase.Business.Models.Models.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefinexCase.Business.Services.Services.CartServices
{
    public interface ICartServices
    {
        IEnumerable<CartDTOModel> GetCartInfo();
        IEnumerable<CartItemDTOModel> GetCartItems();

        bool UpdateCartInfo(CartDTOModel data);
        bool CalculateCart();

        bool AddCartItem(CartItemDTOModel data);
        bool UpdateCartItem(CartItemDTOModel data);
        bool RemoveCartItem(int cart_id, int product_id);
    }
}
