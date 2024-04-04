using E_Commerce.Domain.DTOs.productDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.CategoryDto
{
    public class getCategorywithProductsArabic
    {
        public string name { get; set; }
        public List<getProductwithImageArabic> Products { get; set; }
    }
}
