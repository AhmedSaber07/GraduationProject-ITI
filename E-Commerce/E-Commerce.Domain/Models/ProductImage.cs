using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.Models
{
    public class ProductImage : BaseEntity
    {
        [Required]
        public Guid productId { get; set; }
        [Required]
        public string imageUrl { get; set; }
        public virtual Product Product { get; set; }
    }
}
