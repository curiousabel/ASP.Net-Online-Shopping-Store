using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Casestudy.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; } // generates FK
        [Required]
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Timer { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string GraphicName { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Costprice { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal MSRP { get; set; }
        [Required]
        public int QtyOnHand { get; set; }
        [Required]
        public int QtyOnBackOrders { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }



    }
}
