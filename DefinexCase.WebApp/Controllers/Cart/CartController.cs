using AutoMapper;
using DefinexCase.Business.Models.Models.Product;
using DefinexCase.Business.Models.Models.Cart;
using DefinexCase.Business.Services.Services.ProductServices;
using DefinexCase.Business.Services.Services.CartServices;
using DefinexCase.WebApp.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefinexCase.WebApp.Models.Cart;

namespace DefinexCase.WebApp.Controllers.Cart
{
    public class CartController : Controller
    {

        
        private readonly IProductServices _productServices;
        private readonly ICartServices _cartServices;
        private ProductDTOModel _productModel;
        private CartItemDTOModel _cartItemDTOModel;
        private IEnumerable<ProductModel> _productModelsList;
        private IEnumerable<CartItemModel> _cartModelsList;
        private readonly IMapper _mapper;

        public CartController(IMapper mapper, IProductServices productServices,ICartServices cartServices)
        {
            _mapper = mapper;
            _productServices = productServices;
            _cartServices = cartServices;
        }

        // GET: CartController
        public ActionResult Index()
        {
            bool res = _cartServices.CalculateCart();
            var allProduct = _cartServices.GetCartInfo();
            foreach (var item in allProduct) {

                TempData["total_price"] = item.total_price;
                TempData["discount_type"] = item.discount_type;
                TempData["discount_price"] = item.discount_price;
                TempData["created_date"] = item.created_date;
                TempData["status"] = item.status;
                TempData["payment_total"] = item.total_price- item.discount_price;
            }

            var cartItems = _cartServices.GetCartItems();
            _cartModelsList = _mapper.Map<IEnumerable<CartItemDTOModel>, IEnumerable<CartItemModel>>(cartItems, _cartModelsList);
            
            return View(_cartModelsList);
        }

        [HttpPost]
        public ActionResult Add(int count,int productId)
        {
            try
            {
                _cartItemDTOModel = new CartItemDTOModel();

                var getProduct = _productServices.GetProduct(productId);
                var cartItems = _cartServices.GetCartItems();
                var ifExsist=false;
                var cartProductCount=0;
                foreach (var item in cartItems) {
                    if (item.product_id == productId) {
                        cartProductCount = item.quantity;
                        ifExsist = true;
                    }
                }

                foreach (var item in getProduct)
                {
                    
                    _cartItemDTOModel.cart_id = 1;
                    _cartItemDTOModel.product_id = productId;
                    _cartItemDTOModel.product_name = item.product_name;
                    _cartItemDTOModel.quantity = cartProductCount+count;
                    _cartItemDTOModel.unit_price = item.product_price;
                    _cartItemDTOModel.total_price = item.product_price * (cartProductCount+count);
                }

                if (ifExsist)
                {
                    var addCartItem = _cartServices.UpdateCartItem(_cartItemDTOModel);
                }
                else
                {
                    var addCartItem = _cartServices.AddCartItem(_cartItemDTOModel);
                }
                //bool res = _cartServices.CalculateCart();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            bool response = _cartServices.RemoveCartItem(1, id);
            //bool res = _cartServices.CalculateCart();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
