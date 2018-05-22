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
            if (HttpContext.Session.Get<List<Brand>>("brands") == null)
            {
                // no session information so let's go to the database
                try
                {
                    BrandModel braModel = new BrandModel(_db);
                    // now load the categories
                    List<Brand> brands = braModel.GetAll();
                    HttpContext.Session.Set<List<Brand>>("brands", brands);
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
                vm.SetBrands(HttpContext.Session.Get<List<Brand>>("brands"));
                ProductModel ProdModel = new ProductModel(_db);
                vm.Products = ProdModel.GetAllByBrand(vm.Id);
            }
            return View(vm);
        }
    }
}
