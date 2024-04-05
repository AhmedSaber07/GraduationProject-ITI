using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.OrderDto
{
    public class GetOrderDtoEnglish
    {
        public int OrderNumber { get; set; }
        public DateTime createdAt { get; set; }
        public string status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
