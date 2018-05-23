using Microsoft.AspNetCore.Mvc;
using Casestudy.Models;
namespace Casestudy.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _db;
        public ProductController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(BrandViewModel brand)
        {
            ProductModel model = new ProductModel(_db);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.BrandName = brand.BrandName;
            viewModel.Products = model.GetAllByBrand(brand.Id);
            return View(viewModel);
        }
    }
}