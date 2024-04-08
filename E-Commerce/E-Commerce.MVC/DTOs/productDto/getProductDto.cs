
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.MVC.DTOs.productDto
{
    public class GetProductDto
    {
        public Guid id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime deletedAt { get; set; }
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
        public virtual ICollection<ProductImageDto.GetProductImageDto> images { get; set; }
        public virtual ICollection<ReviewDto.GetReviewDto> reviews { get; set; }
        public GetProductDto()
        {
            images = new List<ProductImageDto.GetProductImageDto>();
            reviews = new List<ReviewDto.GetReviewDto>();
        }

    }
}
