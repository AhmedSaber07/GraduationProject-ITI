using E_Commerce.Domain.DTOs.productDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.CategoryDto
{
    public class getCategorywithProducts
    {
        public string nameAr { get; set; }
        public string nameEn { get; set; }
        public List<GetProductDto> Products { get; set; }
    }
}
