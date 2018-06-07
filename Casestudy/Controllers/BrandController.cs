using Microsoft.AspNetCore.Mvc;
using Casestudy.Models;
using System.Collections.Generic;
using Casestudy.Utils;
using System;
namespace Casestudy.Controllers
{
    public class BrandController : Controller
    {
        AppDbContext _db;
        public BrandController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(BrandViewModel vm)
        {
            // only build the catalogue once
            if (HttpContext.Session.Get<List<Brand>>(SessionVars.Brands) == null)
            {
                // no session information so let's go to the database
                try
                {
                    BrandModel braModel = new BrandModel(_db);
                    // now load the categories
                    List<Brand> brands = braModel.GetAll();
                    HttpContext.Session.Set<List<Brand>>(SessionVars.Brands, brands);
                    vm.SetBrands(brands);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            else
            {
                // no need to go back to the database as information is already in the session
                vm.SetBrands(HttpContext.Session.Get<List<Brand>>(SessionVars.Brands));
                ProductModel ProdModel = new ProductModel(_db);
                vm.Products = ProdModel.GetAllByBrand(vm.Id);
            }
            return View(vm);
        }
        public IActionResult SelectBrand(BrandViewModel vm)
        {
            BrandModel brandModel = new BrandModel(_db);
            ProductModel productModel = new ProductModel(_db);
            List<Product> products = productModel.GetAllByBrand(vm.BrandId);
            List<ProductViewModel> vms = new List<ProductViewModel>();
            if (products.Count > 0)
            {
                foreach (Product product in products)
                {
                    ProductViewModel mvm = new ProductViewModel();
                    mvm.Qty = 0;
                    mvm.BrandId = vm.BrandId;
                    mvm.BrandName = brandModel.GetName(vm.BrandId);
                    mvm.ProductName = product.ProductName;
                    mvm.Price = product.Costprice;
                    mvm.MSRP = product.MSRP;
                    mvm.GraphicName = product.GraphicName;
                    mvm.Description = product.Description;
                    mvm.Id = product.Id;
                    vms.Add(mvm);
                }
                ProductViewModel[] myproduct = vms.ToArray();
                HttpContext.Session.Set<ProductViewModel[]>(SessionVars.Products, myproduct);
            }
            vm.SetBrands(HttpContext.Session.Get<List<Brand>>(SessionVars.Brands));
            return View("Index", vm); // need the original Index View here
        }

        [HttpPost]
        public ActionResult SelectProduct(BrandViewModel vm)
        {
            Dictionary<string, object> cart;
            if (HttpContext.Session.Get<Dictionary<string, Object>>(SessionVars.Cart) == null)
            {
                cart = new Dictionary<string, object>();
            }
            else
            {
                cart = HttpContext.Session.Get<Dictionary<string, object>>(SessionVars.Cart);
            }
            ProductViewModel[] products = HttpContext.Session.Get<ProductViewModel[]>(SessionVars.Products);
            String retMsg = "";
            foreach (ProductViewModel product in products)
            {
                if (product.Id == vm.ProductId)
                {
                    if (vm.Qty > 0) // update only selected product
                    {
                        product.Qty = vm.Qty;
                        retMsg = vm.Qty + " - product(s) Added!";
                        cart[product.Id] = product;
                    }
                    else
                    {
                        product.Qty = 0;
                        cart.Remove(product.Id);
                        retMsg = "product(s) Removed!";
                    }
                    vm.BrandId = product.BrandId;
                    break;
                }
            }
            ViewBag.AddMessage = retMsg;
            HttpContext.Session.Set<Dictionary<string, Object>>(SessionVars.Cart, cart);
            vm.SetBrands(HttpContext.Session.Get<List<Brand>>(SessionVars.Brands));
            return View("Index", vm);
        }
    }
}
