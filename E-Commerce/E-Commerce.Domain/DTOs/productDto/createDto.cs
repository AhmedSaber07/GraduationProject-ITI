using E_Commerce.Domain.DTOs.CategoryDto;
using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.productDto
{
    public class createDto
    {
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

        public virtual ICollection<OrderItemDto.CreateDto>? OrderItems { get; set; }
        public virtual ICollection<ProductImageDto.CreateWithProductDto> Images { get; set; }
        public virtual ICollection<ReviewDto.CreateDto>? Reviews { get; set; }
        public createDto()
        {
            OrderItems = new List<OrderItemDto.CreateDto>();
            Images = new List<ProductImageDto.CreateWithProductDto>();
            Reviews = new List<ReviewDto.CreateDto>();
        }
    }
}
