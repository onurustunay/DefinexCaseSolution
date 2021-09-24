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

namespace DefinexCase.WebApp.Controllers.Product
{
    public class ProductController : Controller
    {
        
        private readonly IProductServices _productServices;
        private List<ProductModel> _productModel;
        private IEnumerable<ProductModel> _productModelsList;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper,  IProductServices productServices)
        {
            _mapper = mapper;
            _productServices = productServices;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var allProduct = _productServices.GetAllProducts();
            _productModelsList = _mapper.Map<IEnumerable<ProductDTOModel>, IEnumerable<ProductModel>>(allProduct, _productModelsList);

            return View(_productModelsList);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
    }
}
