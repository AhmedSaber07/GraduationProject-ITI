
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.MVC.DTOs.productDto
{
    public class createDto
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
        [Range(0, int.MaxValue, ErrorMessage = "sorry the quantity between  0  to 2147483647")]
        [Required(ErrorMessage = "please  enter the quantity of this product in our stock")]
        public int stockQuantity { get; set; }
        public Guid categoryId { get; set; }
        public Guid brandId { get; set; }
        public virtual List<IFormFile> FormFiles { get; set; }
        public virtual ICollection<ProductImageDto.CreateWithProductDto> Images { get; set; }
        public createDto()
        {
          
            Images = new List<ProductImageDto.CreateWithProductDto>();
            FormFiles = new List<IFormFile>();


        }
    }
}
