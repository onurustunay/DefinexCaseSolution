using DefinexCase.Data.Entity.Entity.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefinexCase.Data.Repository.Repositories.Cart
{
    public interface ICartRepository
    {
        IEnumerable<CartEntity> GetCartInfo();
        IEnumerable<CartItemEntity> GetCartItems();

        bool UpdateCartInfo(CartEntity data);

        bool AddCartItem(CartItemEntity data);
        bool UpdateCartItem(CartItemEntity data);
        bool RemoveCartItem(int cart_id,int product_id);
        
    }
}
