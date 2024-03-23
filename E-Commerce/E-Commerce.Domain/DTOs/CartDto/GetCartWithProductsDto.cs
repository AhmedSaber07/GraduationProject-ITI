using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.DTOs.productDto;

namespace E_Commerce.Domain.DTOs.CartDto
{
    public class GetCartWithProductsDto
    {
        public Guid Id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int Quantity { get; set; }
        public Guid SessionId { get; set; }

        public virtual ICollection<GetProductDto> Products { get; set; }

        public GetCartWithProductsDto()
        {
            Products = new List<GetProductDto>();
        }


    }
}
