using E_Commerce.Domain.DTOs.OrderItemDto;
using E_Commerce.Domain.Enums;

namespace E_Commerce.Domain.DTOs.OrderDto
{
    public class GetOrderDto
    {
        public int OrderNumber { get; set; }
        public DateTime createdAt { get; set; }
        public string status_ar { get; set; }
        public string status_en { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
