using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using Casestudy.Utils;
namespace Casestudy.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ClearCart() // clear out current tray
        {
            HttpContext.Session.Remove("cart");
            HttpContext.Session.Set<String>("Message", "Cart Cleared");
            return Redirect("/Home");
        }
    }
}