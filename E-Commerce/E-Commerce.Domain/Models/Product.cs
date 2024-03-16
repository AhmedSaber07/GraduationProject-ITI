﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Models
{
    public class Product : BaseEntity
    {

        [Required(ErrorMessage = "Arabic name is required.")]
        [MaxLength(100, ErrorMessage = "Arabic name cannot exceed 100 characters.")]
        public string nameAr { get; set; }

        [Required(ErrorMessage = "English name is required.")]
        [MaxLength(100, ErrorMessage = "English name cannot exceed 100 characters.")]
        public string nameEn { get; set; }

        [Required(ErrorMessage = "Arabic description is required.")]
        public string descriptionAr { get; set; }
        [Required(ErrorMessage = "Arabic color is required.")]
        public string colorAr { get; set; }
        [Required(ErrorMessage = "English color is required.")]
        public string colorEn { get; set; }

        [Required(ErrorMessage = "English description is required.")]
        public string descriptionEn { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number.")]
        public int stockQuantity { get; set; }
 
        public Guid categoryId { get; set; }

        public Guid brandId { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<orderItem>? OrderItems { get; set; }
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
