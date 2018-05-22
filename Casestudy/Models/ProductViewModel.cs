using System.Collections.Generic;
namespace Casestudy.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}