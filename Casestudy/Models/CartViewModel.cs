using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Casestudy.Models
{
    public class CartViewModel
    {
        public string CartId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string ProductId{ get; set; }
        public decimal OrderAmount { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public decimal MSRP { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public int QtySold { get; set; }
        public int QtyBackOrder { get; set; }
        public string DateCreated { get; set; }
        public int QtyOnHand { get; set; }
        

    }
}