using AutoMapper;
using DefinexCase.Business.Models.Models.Cart;
using DefinexCase.Business.Services.Services.ProductServices;
using DefinexCase.Data.Entity.Entity.Cart;
using DefinexCase.Data.Repository.Repositories.Cart;
using DefinexCase.Data.Repository.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefinexCase.Business.Services.Services.CartServices
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductServices _productServices;
        private List<CartItemDTOModel> _cartItemModelList = new List<CartItemDTOModel>();
        private List<CartDTOModel> _cartModelList = new List<CartDTOModel>();
        private CartDTOModel cartDTOModel = new CartDTOModel();
        private readonly IMapper _mapper;
        private CartEntity _cartEntity;
        private CartItemEntity _cartItemEntity;

        public CartServices(ICartRepository cartRepository, IProductServices productServices, IMapper mapper)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _productServices = productServices;
        }

        public bool AddCartItem(CartItemDTOModel data)
        {

            var cartResource = _mapper.Map<CartItemDTOModel, CartItemEntity>(data, _cartItemEntity);
            var response = _cartRepository.AddCartItem(cartResource);
            

            return response;
        }

        public IEnumerable<CartDTOModel> GetCartInfo()
        {
            var allProducts = _cartRepository.GetCartInfo();
            var productsResource = _mapper.Map<IEnumerable<CartEntity>, IEnumerable<CartDTOModel>>(allProducts, _cartModelList);

            return productsResource;
        }

        public IEnumerable<CartItemDTOModel> GetCartItems()
        {
            var allProducts = _cartRepository.GetCartItems();
            var productsResource = _mapper.Map<IEnumerable<CartItemEntity>, IEnumerable<CartItemDTOModel>>(allProducts, _cartItemModelList);

            return productsResource;
        }

        public bool RemoveCartItem(int cart_id, int product_id)
        {
            var response = _cartRepository.RemoveCartItem(cart_id, product_id);

            return response;
        }

        public bool UpdateCartInfo(CartDTOModel data)
        {
            var cartResource = _mapper.Map<CartDTOModel, CartEntity>(data, _cartEntity);
            var response = _cartRepository.UpdateCartInfo(cartResource);

            return response;
        }

        public bool UpdateCartItem(CartItemDTOModel data)
        {
            var cartResource = _mapper.Map<CartItemDTOModel, CartItemEntity>(data, _cartItemEntity);
            var response = _cartRepository.UpdateCartItem(cartResource);

            return response;
        }

        public bool CalculateCart()
        {
            double totalPrice = 0;
            double totalDiscountPrice = 0;
            
            var response = GetCartItems();
            foreach (var item in response) {
                totalPrice = totalPrice+item.total_price;
            }

            double discountCase1 = DiscountCase1(response);
            double discountCase2 = DiscountCase2(response);
            double discountCase3 = DiscountCase3(response);

            if (totalDiscountPrice < discountCase1){
                totalDiscountPrice = discountCase1;
                cartDTOModel.discount_type = 1;
            }
            if (totalDiscountPrice < discountCase2) { 
                totalDiscountPrice = discountCase2;
                cartDTOModel.discount_type = 2;
            }
            if (totalDiscountPrice < discountCase3) { 
                totalDiscountPrice = discountCase3;
                cartDTOModel.discount_type = 3;
            }

            cartDTOModel.total_price = totalPrice;
            cartDTOModel.discount_price = totalDiscountPrice;


            bool responseCartInfo=UpdateCartInfo(cartDTOModel);


            return responseCartInfo;
        }
        //1000 TL ve üzeri sepet tutarında en ucuz 1 ürüne %20 indirim
        double DiscountCase1(IEnumerable<CartItemDTOModel> data)
        {

            double cartTotalPrice = 0;
            double discountTotal = 0;
            double minTotal = 0;
            CartItemDTOModel min = new CartItemDTOModel();

            foreach (var item in data)
            {
                cartTotalPrice = cartTotalPrice + item.total_price;
                if (min.unit_price == 0)
                {
                    min.unit_price = item.unit_price;
                    min.total_price = item.total_price;
                    min.quantity = item.quantity;
                    min.product_id = item.product_id;
                    min.product_name = item.product_name;
                }
                else if (min.unit_price > item.unit_price)
                {
                    min.unit_price = item.unit_price;
                    min.total_price = item.total_price;
                    min.quantity = item.quantity;
                    min.product_id = item.product_id;
                    min.product_name = item.product_name;
                }
            }
            if (cartTotalPrice >= 1000)
            {
                
                discountTotal = ((min.unit_price / 100) * 20); ;
                return discountTotal;
            }
            else
            {
                discountTotal = 0;
                return discountTotal;
            }

        }
        //Aynı üründen her 3 adede %15 indirim 
        double DiscountCase2(IEnumerable<CartItemDTOModel> data)
        {
            double cartTotalPrice = 0;
            double discountTotal = 0;
            var count = 0;
            foreach(var item in data)
            {
                
                count = item.quantity;
                while (count >= 3) {
                    
                    discountTotal = discountTotal+(((item.unit_price * 3) / 100) * 15);
                    count = count - 3;
                }
                
            }

            return discountTotal;
        }
        //1 alana 1 bedava 
        double DiscountCase3(IEnumerable<CartItemDTOModel> data)
        {
            
            double discountTotal = 0;
            bool discount = false;
            foreach (var item in data) {
                var productlist = _productServices.GetProduct(item.product_id);
                foreach (var productInfo in productlist) {
                    discount = productInfo.discount;
                }
                if (item.quantity > 2 && discount) {
                    discountTotal = discountTotal + item.unit_price * (item.quantity / 2);
                }
            }
            return discountTotal;
        }
    }
}
