using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Casestudy.Models
{
    public class Cart
    {
        public Cart()
        {
            CartLineItems = new HashSet<CartLineItem>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CartDate { get; set; }
        [Column(TypeName = "money")]
        public decimal CartAmount { get; set; }
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        public virtual ICollection<CartLineItem> CartLineItems { get; set; }
    }
}