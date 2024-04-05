﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.OrderDto
{
    public class getOrdersWithoutItemsEnglish
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public string status { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
