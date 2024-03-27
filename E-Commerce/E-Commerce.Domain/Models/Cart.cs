using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce.Domain.Models
{
    public class Cart : BaseEntity
    {
        [Required(ErrorMessage = "Product Id Required")]
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Product Id Required")]
        public Guid SessionId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }


        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}
