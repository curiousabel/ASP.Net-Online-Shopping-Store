using System.Collections.Generic;
namespace Casestudy.Models
{
    public class ProductViewModel
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string ProductName { get; set; }
        public string GraphicName { get; set; }
        public string Id { get; set; }
        public string JsonData { get; set; }
        public string Description { get; set; }
        public decimal MSRP { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
    }

}
