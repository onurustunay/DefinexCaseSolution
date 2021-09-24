using AutoMapper;
using DefinexCase.Business.Models.Models.Product;
using DefinexCase.Business.Services.Services.ProductServices;
using DefinexCase.WebApp.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefinexCase.WebApp.Controllers.Admin
{
    public class AdminController : Controller
    {
        
        private readonly IProductServices _productServices;
        private ProductDTOModel _productModel;
        private IEnumerable<ProductModel> _productModelsList;
        private readonly IMapper _mapper;

        public AdminController(IMapper mapper,  IProductServices productServices)
        {
            _mapper = mapper;
            _productServices = productServices;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            var allProduct = _productServices.GetAllProducts();
            _productModelsList = _mapper.Map<IEnumerable<ProductDTOModel>, IEnumerable<ProductModel>>(allProduct, _productModelsList);
            return View(_productModelsList);
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            _productModel = new ProductDTOModel();
            _productModel.product_name = collection["product_name"];
            _productModel.product_price = Convert.ToInt32(collection["product_price"]);

            if (collection["discount"] == "false")
            {
                _productModel.discount = false;
            }
            else
            {
                _productModel.discount = true;
            }



            try
            {
                bool getUpdateResponse = _productServices.CreateProduct(_productModel);
                if (getUpdateResponse)
                {
                    TempData["CreateResponse"] = true;
                }
                else
                {
                    TempData["CreateResponse"] = false;
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["CreateResponse"] = false;

                return RedirectToAction(nameof(Index));
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            TempData["UpdateResponse"] = false;
            var getProduct = _productServices.GetProduct(id);
            _productModelsList = _mapper.Map<IEnumerable<ProductDTOModel>, IEnumerable<ProductModel>>(getProduct, _productModelsList);
            return View(_productModelsList);
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            
            _productModel = new ProductDTOModel();
            _productModel.product_id = id;
            _productModel.product_name = collection["item.product_name"];
            _productModel.product_price = Convert.ToInt32(collection["item.product_price"]);

            if (collection["item.discount"] == "false"){
                _productModel.discount = false;
            }
            else {
                _productModel.discount = true;
            }

            

            try
            {
                bool getUpdateResponse = _productServices.UpdateProduct(_productModel);
                if (getUpdateResponse)
                {
                    TempData["UpdateResponse"] = true;
                }
                else {
                    TempData["UpdateResponse"] = false;
                }

                var getProduct = _productServices.GetProduct(id);
                _productModelsList = _mapper.Map<IEnumerable<ProductDTOModel>, IEnumerable<ProductModel>>(getProduct, _productModelsList);
                return View(_productModelsList);
            }
            catch(Exception e)
            {
                TempData["UpdateResponse"] = false;
                
                return View(_productModelsList);
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var getProduct = _productServices.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e) {
                //Hata log eklenebilir.
                return RedirectToAction(nameof(Index));
            }
            
        }

        
        
    }
}
