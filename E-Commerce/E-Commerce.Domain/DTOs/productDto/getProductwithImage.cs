using E_Commerce.Domain.DTOs.ProductImageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.productDto
{
    public class getProductwithImage
    {
        public Guid Id { get; set; }
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
        public decimal Rating { get; set; }
        public List<string> Images { get; set; }
        public virtual ICollection<ReviewDto.GetReviewDto>? Reviews { get; set; }

    }
}
