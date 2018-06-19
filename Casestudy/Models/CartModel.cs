using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
namespace Casestudy.Models
{
    public class CartModel
    {
        private AppDbContext _db;
        public CartModel(AppDbContext ctx)
        {
            _db = ctx;
        }

        public List<Cart> GetAll()
        {
            return _db.Cart.ToList(); 
        }
        public String AddCart(Dictionary<string, object> items, string user)
        {
            String backOrder = "";
            string orderState = "Order not added , try again";
            int cartId = -1;
            using (_db)
            {
                // we need a transaction as multiple entities involved
                using (var _trans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        Cart cart = new Cart();
                        cart.UserId = user;
                        cart.CartDate = System.DateTime.Now;
                        cart.CartAmount = 0;
                        // calculate the totals and then add the cart row to the table
                        foreach (var key in items.Keys)
                        {
                            ProductViewModel item = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            if (item.Qty > 0)
                            {
                                cart.CartAmount += item.MSRP * item.Qty;
                            }
                        }
                        _db.Cart.Add(cart);
                        _db.SaveChanges();
                        // then add each item to the cartitems table
                        foreach (var key in items.Keys)
                        {
                            ProductViewModel productViewModel = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(items[key]));
                            ProductModel productModel = new ProductModel(_db);
                            Product product = productModel.GetById(productViewModel.Id);
                            if (productViewModel.Qty > 0)
                            {
                                CartLineItem cItem = new CartLineItem();
                                cItem.QtyOrder = productViewModel.Qty;
                                cItem.ProductId = product.Id;
                                cItem.CartId = cart.Id;
                                cItem.SellingPrice = cItem.QtyOrder * product.MSRP;
                                if (product.QtyOnHand < cItem.QtyOrder)
                                {
                                    cItem.QtySold = product.QtyOnHand;
                                    cItem.QtyBackOrdered = cItem.QtyOrder - product.QtyOnHand;
                                    product.QtyOnHand = 0;
                                    product.QtyOnBackOrders += cItem.QtyBackOrdered;
                                    backOrder = " some items were backOrdered";
                                }
                                else
                                {
                                    cItem.QtySold = cItem.QtyOrder;
                                    product.QtyOnHand -= cItem.QtySold;
                                    product.QtyOnBackOrders = 0;
                                   
                                }
                                _db.Products.Update(product);
                                _db.CartLineItems.Add(cItem);
                                _db.SaveChanges();
                            }
                        }
                        // test trans by uncommenting out these 3 lines
                        //int x = 1;
                        //int y = 0;
                        //x = x / y;
                        _trans.Commit();
                        cartId = cart.Id;


                      return orderState = "Order" + cart.Id + " Created "+ backOrder;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _trans.Rollback();
                        return "";
                    }
                }
            }
            
        }
        public List<CartViewModel> GetCartDetails(int tid, string uid)
        {
            List<CartViewModel> allDetails = new List<CartViewModel>();
            // LINQ way of doing INNER JOINS
            var results = from t in _db.Set<Cart>()
                          join ti in _db.Set<CartLineItem>() on t.Id equals ti.CartId
                          join mi in _db.Set<Product>() on ti.ProductId equals mi.Id
                          where (t.UserId == uid && t.Id == tid)
                          select new CartViewModel
                          {
                             // CartId = mi.Id,
                              UserId = uid,
                              MSRP = mi.MSRP,
                              QtyBackOrder = mi.QtyOnBackOrders,
                              QtySold = ti.QtySold,
                             // QtyOnHand = ti.q,
                              ProductName = mi.ProductName,
                              OrderAmount = t.CartAmount, 
                             
                              Description = mi.Description,
                              Qty = ti.QtyOrder,
                             DateCreated = t.CartDate.ToString("yyyy/MM/dd - hh:mm tt")
                          };
            allDetails = results.ToList<CartViewModel>();
            return allDetails;
        }
    }
}
