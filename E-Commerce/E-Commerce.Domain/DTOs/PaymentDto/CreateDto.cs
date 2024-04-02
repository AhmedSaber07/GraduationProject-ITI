using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.PaymentDto
{
    public class CreateDto
    {
        public string transactionId { get; set; }
        public string PaymentType { get; set; }
        public decimal OrderPrice { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
