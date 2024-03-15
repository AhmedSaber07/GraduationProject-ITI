using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.Models
{
    public class Payment : BaseEntity
    {
        [Required(ErrorMessage = "Order Id  status is required")]
        public Guid OrderId { get; set; }

        public string Type { get; set; }

        [Required (ErrorMessage ="Payment status is required")]
        public string Status { get; set; }

        public string Details { get; set; }

        public virtual Order Order { get; set; }
    }
}
