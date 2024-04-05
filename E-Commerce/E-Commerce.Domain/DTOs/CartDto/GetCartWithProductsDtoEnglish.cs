using E_Commerce.Domain.DTOs.productDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.CartDto
{
    public class GetCartWithProductsDtoEnglish
    {
        public Guid Id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int Quantity { get; set; }
        public Guid SessionId { get; set; }

        public virtual ICollection<getProductDtoEnglish> Products { get; set; }

        public GetCartWithProductsDtoEnglish()
        {
            Products = new List<getProductDtoEnglish>();
        }
    }
}
