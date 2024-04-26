
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.MVC.DTOs.productDto
{
    public class getProductwithImage
    {
        public Guid id { get; set; }
        [Display(Name ="Arabic Name")]
        public string nameAr { get; set; }
        [Display(Name = "English Name")]
        public string nameEn { get; set; }
        [Display(Name = "Arabic Description")]
        public string descriptionAr { get; set; }
        [Display(Name = "English Description")]
        public string descriptionEn { get; set; }
        [Display(Name = "Arabic Color")]
        public string colorAr { get; set; }
        [Display(Name = "English Color")]
        public string colorEn { get; set; }
        [Display(Name = "Price")]
        public decimal price { get; set; }
        [Display(Name = "Stock Quantity")]
        public int stockQuantity { get; set; }
        [Display(Name = "Category Name")]
        public Guid categoryId { get; set; }
        public Guid brandId { get; set; }
        public decimal rating { get; set; }
        public virtual List<IFormFile>? FormFiles { get; set; }

        public List<string>? images { get; set; }
        public virtual ICollection<ReviewDto.GetReviewDto>? reviews { get; set; }
        public getProductwithImage()
        {
            images = new List<string>();
            reviews=new List<ReviewDto.GetReviewDto>();
        }

    }
}
