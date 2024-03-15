using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.productDto
{
    public class getProductDto
    {
        public Guid Id { get; set; }
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

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<orderItem> OrderItems { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public getProductDto()
        {
            OrderItems = new List<orderItem>();
            Images = new List<ProductImage>();
            Reviews = new List<Review>();
        }
    }
}
