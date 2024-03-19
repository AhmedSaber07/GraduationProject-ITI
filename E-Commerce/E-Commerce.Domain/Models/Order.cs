using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace E_Commerce.Domain.Models
{
    public class Order : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }  
        public string status_ar { get; set; }
        public string status_en { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public virtual Payment Payment { get; set; }
      
        public virtual MyUser User { get; set; }
        public virtual ICollection<orderItem> OrderItems { get; set; }
    }
}
