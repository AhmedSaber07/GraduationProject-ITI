using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Domain.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = " Arabic  name is required.")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string nameAr { get; set; }
        [Required(ErrorMessage = " English  name is required.")]
        [MaxLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string nameEn { get; set; }

        [ForeignKey("ParentCategory")]
        public Guid ParentCategoryId { get; set; }
      
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Category> Subcategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
