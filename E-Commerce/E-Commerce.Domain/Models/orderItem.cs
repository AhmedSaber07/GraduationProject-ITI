using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce.Domain.Models
{
    public class orderItem : BaseEntity
    {
        [Required(ErrorMessage = "ProductId is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public virtual Product Product { get; set; }

      
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
