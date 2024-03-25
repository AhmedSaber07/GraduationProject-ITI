
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_Commerce.MVC.DTOs.CategoryDto
{

    public class ReadCategoryDto
    {

        public Guid Id { get; set; }
        public string NameAr { get; set; }
     
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "English name can only contain letters and digits")]

        public string NameEn { get; set; }
        public Guid? ParentCategoryId { get; set; }
      
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
