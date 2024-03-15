using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.Models
{
    public class Brand:BaseEntity
    {
        
        [Required(ErrorMessage = " Arabic  name is required.")]
        [MaxLength(100, ErrorMessage = "Brand name cannot exceed 100 characters.")]
        public string nameAr { get; set; }
        [Required(ErrorMessage = " English  name is required.")]
        [MaxLength(100, ErrorMessage = "Brand name cannot exceed 100 characters.")]
        public string nameEn { get; set; }
        public string email { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
