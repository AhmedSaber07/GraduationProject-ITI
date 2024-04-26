using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.CategoryDto
{
    public class CreateOrUpdateCategoryDto
    {
        public Guid id { get; set; }
        [Display(Name = "Arabic Name")]
        [Required(ErrorMessage = " Arabic  name is required.")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string nameAr { get; set; }
        [Display(Name = "English Name")]
        [Required(ErrorMessage = " English  name is required.")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string nameEn { get; set; }
        [Display(Name = "Parent Category")]
        public Guid? ParentCategoryId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
