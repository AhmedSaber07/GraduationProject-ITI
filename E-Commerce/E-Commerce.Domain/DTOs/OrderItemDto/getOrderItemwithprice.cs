using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.OrderItemDto
{
    public class getOrderItemwithprice
    {
        public string productName {  get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public decimal ItemTotalPrice { get; set; }
    }
}
