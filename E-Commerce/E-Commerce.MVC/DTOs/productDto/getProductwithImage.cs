
namespace E_Commerce.MVC.DTOs.productDto
{
    public class getProductwithImage
    {
        public Guid id { get; set; }
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
        public decimal rating { get; set; }
        public List<string> images { get; set; }
        public virtual ICollection<ReviewDto.GetReviewDto>? reviews { get; set; }
        public getProductwithImage()
        {
            images = new List<string>();
            reviews=new List<ReviewDto.GetReviewDto>();
        }

    }
}
