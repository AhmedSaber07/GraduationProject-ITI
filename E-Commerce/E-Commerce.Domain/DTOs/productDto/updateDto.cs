using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.productDto
{
    public class updateDto
    {

        public DateTime updatedAt { get; set; }
        public string nameAr { get; set; }
        public string nameEn { get; set; }
        public string descriptionAr { get; set; }
        public string colorAr { get; set; }
        public string colorEn { get; set; }
        public string descriptionEn { get; set; }
        public decimal price { get; set; }
        public int stockQuantity { get; set; }
        public Guid categoryId { get; set; }
        public Guid brandId { get; set; }

        // Navigation properties

        public virtual ICollection<OrderItemDto.UpdateDto> OrderItems { get; set; }
        public virtual ICollection<ProductImageDto.UpdateDto> Images { get; set; }
        public virtual ICollection<ReviewDto.UpdateDto> Reviews { get; set; }

        public updateDto()
        {
            OrderItems = new List<OrderItemDto.UpdateDto>();
            Images = new List<ProductImageDto.UpdateDto>();
            Reviews = new List<ReviewDto.UpdateDto>();
        }
    }
}
