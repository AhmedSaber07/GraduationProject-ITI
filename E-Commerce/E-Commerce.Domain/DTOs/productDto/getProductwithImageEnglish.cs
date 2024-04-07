using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.productDto
{
    public class getProductwithImageEnglish
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public decimal price { get; set; }
        public int stockQuantity { get; set; }
        public Guid categoryId { get; set; }
        public Guid brandId { get; set; }
        public decimal Rating { get; set; }
        public List<string> Images { get; set; }
        public virtual ICollection<ReviewDto.GetReviewDto>? Reviews { get; set; }
    }
}
