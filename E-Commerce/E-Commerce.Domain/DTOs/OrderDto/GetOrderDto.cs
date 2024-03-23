using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.DTOs.OrderItemDto;

namespace E_Commerce.Domain.DTOs.OrderDto
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime deletedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public string status_ar { get; set; }
        public string status_en { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<GetOrderItemDto> OrderItems { get; set; }
    }
}
