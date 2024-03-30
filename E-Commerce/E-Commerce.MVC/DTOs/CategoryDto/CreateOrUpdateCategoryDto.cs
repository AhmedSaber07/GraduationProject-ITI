namespace E_Commerce.MVC.DTOs.CategoryDto
{
    public class CreateOrUpdateCategoryDto
    {
        public Guid id { get; set; }
        public string nameAr { get; set; }      
        public string nameEn { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
