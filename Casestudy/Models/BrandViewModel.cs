using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Casestudy.Models
{
    public class BrandViewModel
    {
        private List<Brand> _brands;
        public IEnumerable<Product> Products { get; set; }
        
        public int Qty { get; set; }
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ProductId { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<SelectListItem> GetBrands()
        {
            return _brands.Select(BrandName => new SelectListItem
            {
                Text = BrandName.Name,
                Value = Convert.ToString(BrandName.Id)
            });
        }
        public void SetBrands(List<Brand> bra)
        {
            _brands = bra;
        }
    }
}
