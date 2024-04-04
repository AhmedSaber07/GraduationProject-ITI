using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.productDto
{
    public class getProductDtoArabic
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public decimal price { get; set; }
        public int stockQuantity { get; set; }
        public Guid categoryId { get; set; }
        public Guid brandId { get; set; }

        // Navigation properties

        public virtual ICollection<OrderItemDto.GetOrderItemDto> OrderItems { get; set; }
        public virtual ICollection<ProductImageDto.GetProductImageDto> Images { get; set; }
        public virtual ICollection<ReviewDto.GetReviewDto> Reviews { get; set; }
        public getProductDtoArabic()
        {
            OrderItems = new List<OrderItemDto.GetOrderItemDto>();
            Images = new List<ProductImageDto.GetProductImageDto>();
            Reviews = new List<ReviewDto.GetReviewDto>();
        }
    }
}
