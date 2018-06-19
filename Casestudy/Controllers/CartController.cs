//////using Microsoft.AspNetCore.Mvc;
//////using Microsoft.AspNetCore.Http;
//////using System;
//////using Casestudy.Utils;
//////namespace Casestudy.Controllers
//////{
//////    public class CartController : Controller
//////    {
//////        public IActionResult Index()
//////        {
//////            return View();
//////        }
//////        public ActionResult ClearCart() // clear out current Cart
//////        {
//////            HttpContext.Session.Remove(SessionVars.Cart);
//////            HttpContext.Session.Set<String>(SessionVars.Cart, "Cart Cleared");
//////            return Redirect("/Home");
//////        }
//////    }
//////}

////using Microsoft.AspNetCore.Mvc;
////using Microsoft.AspNetCore.Http;
////using Casestudy.Utils;
////using Casestudy.Models;
////using System.Collections.Generic;
////using System;
////namespace Casestudy.Controllers
////{
////    public class CartController : Controller
////    {
////        AppDbContext _db;
////        public CartController(AppDbContext context)
////        {
////            _db = context;
////        }
////        public IActionResult Index()
////        {
////            return View();
////        }
////        public ActionResult ClearTray() // clear out current tray
////        {
////            HttpContext.Session.Remove(SessionVars.Cart);
////            HttpContext.Session.SetString(SessionVars.Message, "Cart Cleared");
////            return Redirect("/Home");
////        }
////        // Add the tray, pass the session variable info to the db
////        public ActionResult AddCart()
////        {
////            CartModel model = new CartModel(_db);
////            int retVal = -1;
////            string retMessage = "";
////            try
////            {
////                Dictionary<string, object> cartItems = HttpContext.Session.Get<Dictionary<string, object>>(SessionVars.Cart);
////                retVal = model.AddCart(cartItems, HttpContext.Session.GetString(SessionVars.User));
////                if (retVal > 0) // Tray Added
////                {
////                    retMessage = "Cart " + retVal + " Created!";
////                }
////                else // problem
////                {
////                    retMessage = "Cart not added, try again later";
////                }
////            }
////            catch (Exception ex) // big problem
////            {
////                retMessage = "Cart was not created, try again later! - " + ex.Message;
////            }
////            HttpContext.Session.Remove(SessionVars.Cart); // clear out current tray once persisted
////            HttpContext.Session.SetString(SessionVars.Message, retMessage);
////            return Redirect("/Home");
////        }
////    }
////}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Casestudy.Utils;
using Casestudy.Models;
using System.Collections.Generic;
using System;
namespace Casestudy.Controllers
{
    public class CartController : Controller
    {
        AppDbContext _db;
        public CartController(AppDbContext context)
        {
            _db = context;
        }

        [Route("[action]")]
        public IActionResult GetCarts()
        {
            CartModel model = new CartModel(_db);
            return Ok(model.GetAll());
        }

        [Route("[action]/{tid:int}")]
        public IActionResult GetCartDetails(int tid)
        {
            CartModel model = new CartModel(_db);
            return Ok(model.GetCartDetails(tid, HttpContext.Session.GetString(SessionVars.User)));
        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ClearCArt() // clear out current tray
        {
            HttpContext.Session.Remove(SessionVars.Cart);
            HttpContext.Session.SetString(SessionVars.Message, "Cart Cleared");
            return Redirect("/Home");
        }
        // Add the tray, pass the session variable info to the db
        public ActionResult AddCart()
        {
            CartModel model = new CartModel(_db);
            
            string retMessage = "";
            try
            {
                Dictionary<string, object> cartItems = HttpContext.Session.Get<Dictionary<string, object>>(SessionVars.Cart);
                retMessage = model.AddCart(cartItems, HttpContext.Session.GetString(SessionVars.User));
                if (retMessage  == "") // Tray Added
                {
                    retMessage = "Cart not added, try again later";
                }
              
            }
            catch (Exception ex) // big problem
            {
                retMessage = "Cart was not created, try again later! - " + ex.Message;
            }
            HttpContext.Session.Remove(SessionVars.Cart); // clear out current Cart once persisted
            HttpContext.Session.SetString(SessionVars.Message, retMessage);
            return Redirect("/Home");
        }
        public IActionResult List()
        {
            // they can't list Carts if they're not logged on
            if (HttpContext.Session.GetString(SessionVars.User) == null)
            {
                return Redirect("/Login");
            }
            return View("List");
        }



    }
}