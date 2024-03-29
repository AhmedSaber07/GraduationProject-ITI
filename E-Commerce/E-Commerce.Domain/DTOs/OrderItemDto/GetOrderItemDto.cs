using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.OrderItemDto
{
    public class GetOrderItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotalPrice { get; set; }
        public Guid OrderId { get; set; }
    }
}
