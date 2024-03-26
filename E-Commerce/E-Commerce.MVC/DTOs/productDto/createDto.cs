
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
        public virtual ICollection<ProductImageDto.CreateWithProductDto> Images { get; set; }
        public createDto()
        {
          
            Images = new List<ProductImageDto.CreateWithProductDto>();
           
        }
    }
}
