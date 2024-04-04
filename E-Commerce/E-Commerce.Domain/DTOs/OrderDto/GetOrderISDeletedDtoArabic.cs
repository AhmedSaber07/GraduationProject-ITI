using E_Commerce.Domain.DTOs.OrderItemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.OrderDto
{
    public class GetOrderISDeletedDtoArabic
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public string status { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<GetOrderItemDto> OrderItems { get; set; }
    }
}
