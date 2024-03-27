using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.Models
{
    public class Payment : BaseEntity
    {
        public string transactionId { get; set; }
        public string PaymentType { get; set; }
        public decimal OrderPrice { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public MyUser User { get; set; }
        [Required(ErrorMessage = "Order Id  status is required")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
