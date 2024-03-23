using E_Commerce.Domain.DTOs.productDto;
using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.CartDto
{
    public class GetCartDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime deletedAt { get; set; }

        public Guid ProductId { get; set; }

        public Guid sessionId { get; set; }
        public int Quantity { get; set; }


    }
}
