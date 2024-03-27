using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Enums;

namespace E_Commerce.Domain.DTOs.OrderDto
{
    public class CreateOrUpdateDto
    {
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public OrderStateAr status_ar { get; set; }
        public OrderStateEn status_en { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<Domain.DTOs.OrderItemDto.CreateDto> OrderItems { get; set; }
        public CreateOrUpdateDto()
        {
            OrderItems = new List<Domain.DTOs.OrderItemDto.CreateDto>();
        }
    }
}
