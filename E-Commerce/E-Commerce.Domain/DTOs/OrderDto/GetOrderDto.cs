using E_Commerce.Domain.DTOs.OrderItemDto;
using E_Commerce.Domain.Enums;

namespace E_Commerce.Domain.DTOs.OrderDto
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime createdAt { get; set; }
        public OrderStateAr status_ar { get; set; }
        public OrderStateEn status_en { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<getOrderItemwithprice> OrderItems { get; set; }
    }
}
